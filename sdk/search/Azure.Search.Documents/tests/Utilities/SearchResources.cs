﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core.Testing;
using Azure.Search.Documents.Models;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.Search;
using Microsoft.Azure.Management.Search.Models;
using NUnit.Framework;

namespace Azure.Search.Documents.Tests
{
    /// <summary>
    /// Manage the creation and deletion of live test resources.  This uses the
    /// Track 1 management libraries to create the Search Service and
    /// (temporarily) the Track 1 data plane libraries to create and populate
    /// the Hotels index.
    /// </summary>
    public partial class SearchResources : IAsyncDisposable
    {
        /// <summary>
        /// Settings used to create test resources.
        /// </summary>
        private static class Settings
        {
            /// <summary>
            /// A source of randomness for creating live test resources.  This
            /// should not be tied into test recordings because live resource
            /// creation is independent and not recorded.
            /// </summary>
            public static Random Random { get; } = new Random(Environment.TickCount);

            /// <summary>
            /// The prefix we use when creating resources - which can help make
            /// it a little easier to identify anything that leaks while
            /// debugging locally.
            /// </summary>
            public const string ResourcePrefix = "azs-net-";

            /// <summary>
            /// The name of the RP for Search Services.
            /// </summary>
            public const string ResourceProviderNamespace = "Microsoft.Search";

            /// <summary>
            /// The AD Tenant of our Service Principal.
            /// </summary>
            public static string TenantId { get; } = GetEnvVar("AZURE_TENANT_ID");

            /// <summary>
            /// The Client ID of our Service Principal.
            /// </summary>
            public static string ClientId { get; } = GetEnvVar("AZURE_CLIENT_ID");

            /// <summary>
            /// The Client Secret for our Service Principal.
            /// </summary>
            public static string ClientSecret { get; } = GetEnvVar("AZURE_CLIENT_SECRET");

            /// <summary>
            /// The Subscription ID to use when creating resources.
            /// </summary>
            public static string SubscriptionId { get; } = GetEnvVar("AZURE_SUBSCRIPTION_ID");

            /// <summary>
            /// The Resource Group to use for creating resources (so they can
            /// be easily marked for cleanup in the event of a leak).
            /// </summary>
            public static string ResourceGroup { get; } = GetEnvVar("AZURE_RESOURCE_GROUP");

            /// <summary>
            /// The location to create our test resources.
            /// </summary>
            public static string Location { get; } = GetEnvVar("AZURE_LOCATION");

            /// <summary>
            /// Get the value of an environment variable and fail the test if
            /// not found.
            /// </summary>
            /// <param name="name">
            /// The name of the environment variable.
            /// </param>
            /// <returns>The value of the environment variable.</returns>
            private static string GetEnvVar(string name)
            {
                string value = Environment.GetEnvironmentVariable(name);
                if (string.IsNullOrEmpty(value))
                {
                    Assert.Inconclusive($"Could not find environment variable {name} required to run these tests live.");
                }
                return value;
            }
        }

        /// <summary>
        /// The name of the Search service.
        /// </summary>
        public string SearchServiceName
        {
            get => TestFixture.Recording.GetVariable("SearchServiceName", _searchServiceName);
            set
            {
                TestFixture.Recording.SetVariable("SearchServiceName", value);
                _searchServiceName = value;
            }
        }
        private string _searchServiceName = null;

        /// <summary>
        /// The name of the index created for test data.
        /// </summary>
        public string IndexName
        {
            get => TestFixture.Recording.GetVariable("SearchIndexName", _indexName);
            set
            {
                TestFixture.Recording.SetVariable("SearchIndexName", value);
                _indexName = value;
            }
        }
        private string _indexName = null;

        /// <summary>
        /// The URI of the Search service.
        /// </summary>
        public Uri Endpoint => new Uri($"https://{SearchServiceName}.search.windows.net");

        /// <summary>
        /// The Primary or Admin API key to authenticate requests to the
        /// service.
        /// </summary>
        public string PrimaryApiKey { get; private set; } = "Sanitized";

        /// <summary>
        /// The Query API key to authenticate requests to the service.
        /// </summary>
        public string QueryApiKey { get; private set; } = "Sanitized";

        /// <summary>
        /// Flag indicating whether these resources need to be cleaned up.
        /// This is true for any resources that we created.
        /// </summary>
        public bool RequiresCleanup { get; private set; }

        /// <summary>
        /// The TestFixture with context about our current test run,
        /// recordings, instrumentation, etc.
        /// </summary>
        public SearchTestBase TestFixture { get; private set; }

        #region Create Test Resources
        /// <summary>
        /// Direct folks toward the static helpers.
        /// </summary>
        /// <param name="fixture">
        /// The TestFixture with context about our current test run,
        /// recordings, instrumentation, etc.
        /// </param>
        private SearchResources(SearchTestBase fixture) => TestFixture = fixture;

        /// <summary>
        /// Create a new Search Service resource with no indexes.
        /// </summary>
        /// <param name="fixture">
        /// The TestFixture with context about our current test run,
        /// recordings, instrumentation, etc.
        /// </param>
        /// <returns>A new TestResources context.</returns>
        public static async Task<SearchResources> CreateWithNoIndexesAsync(SearchTestBase fixture)
        {
            var resources = new SearchResources(fixture);
            await resources.CreateSearchServiceAsync();
            return resources;
        }

        /// <summary>
        /// Create a new Search Service resource with an empty Hotel index.
        /// See TestResources.Data.cs for the index schema.
        /// </summary>
        /// <param name="fixture">
        /// The TestFixture with context about our current test run,
        /// recordings, instrumentation, etc.
        /// </param>
        /// <returns>A new TestResources context.</returns>
        public static async Task<SearchResources> CreateWithEmptyHotelsIndexAsync(SearchTestBase fixture)
        {
            var resources = new SearchResources(fixture);
            await resources.CreateSearchServiceAndIndexAsync();
            return resources;
        }

        /// <summary>
        /// Create a new Search Service resource a Hotel index and sample data
        /// set.  See TestResources.Data.cs for the index schema and data set.
        /// </summary>
        /// <param name="fixture">
        /// The TestFixture with context about our current test run,
        /// recordings, instrumentation, etc.
        /// </param>
        /// <returns>A new TestResources context.</returns>
        public static async Task<SearchResources> CreateWithHotelsIndexAsync(SearchTestBase fixture)
        {
            var resources = new SearchResources(fixture);
            await resources.CreateSearchServiceIndexAndDocumentsAsync();
            return resources;
        }

        /// <summary>
        /// Get a shared Search Service resource with a Hotel index and sample
        /// data set.  See TestResources.Data.cs for the index schema and data
        /// set.  This resource is shared across the entire test run and should
        /// not be mutated.
        /// </summary>
        /// <param name="fixture">
        /// The TestFixture with context about our current test run,
        /// recordings, instrumentation, etc.
        /// </param>
        /// <returns>The shared TestResources context.</returns>
        public static async Task<SearchResources> GetSharedHotelsIndexAsync(SearchTestBase fixture)
        {
            await SharedSearchResources.EnsureInitialized(async () => await CreateWithHotelsIndexAsync(fixture));

            // Clone it for the current fixture (note that setting these values
            // will create the recording ServiceName/IndexName/etc. variables)
            return new SearchResources(fixture)
            {
                SearchServiceName = SharedSearchResources.Search.SearchServiceName,
                IndexName = SharedSearchResources.Search.IndexName,
                PrimaryApiKey = SharedSearchResources.Search.PrimaryApiKey,
                QueryApiKey = SharedSearchResources.Search.QueryApiKey
            };
        }
        #endregion Create Test Resources

        #region Get Clients
        /// <summary>
        /// Get a SearchServiceClient to use for testing.
        /// </summary>
        /// <param name="options">Optional client options.</param>
        /// <returns>A SearchServiceClient instance.</returns>
        public SearchServiceClient GetServiceClient(SearchClientOptions options = null) =>
            TestFixture.InstrumentClient(
                new SearchServiceClient(
                    Endpoint,
                    new AzureKeyCredential(PrimaryApiKey),
                    TestFixture.GetSearchClientOptions(options)));

        /// <summary>
        /// Get a SearchIndexClient to use for testing with an Admin API key.
        /// </summary>
        /// <param name="options">Optional client options.</param>
        /// <returns>A SearchIndexClient instance.</returns>
        public SearchIndexClient GetIndexClient(SearchClientOptions options = null)
        {
            Assert.IsNotNull(IndexName, "No index was created for these TestResources!");
            return TestFixture.InstrumentClient(
                GetServiceClient(options).GetSearchIndexClient(IndexName));
        }

        /// <summary>
        /// Get a SearchIndexClient to use for testing with a query API key.
        /// </summary>
        /// <param name="options">Optional client options.</param>
        /// <returns>A SearchIndexClient instance.</returns>
        public SearchIndexClient GetQueryClient(SearchClientOptions options = null)
        {
            Assert.IsNotNull(IndexName, "No index was created for these TestResources!");
            return TestFixture.InstrumentClient(
                new SearchIndexClient(
                    Endpoint,
                    IndexName,
                    new AzureKeyCredential(QueryApiKey),
                    TestFixture.GetSearchClientOptions(options)));
        }
        #endregion Get Clients

        #region Search Service Management
        /// <summary>
        /// Create a client that can be used to create and delete Search
        /// Services.
        /// </summary>
        /// <returns>
        /// A client that can be used to create and delete Search Services.
        /// </returns>
        private static SearchManagementClient GetManagementClient() =>
            new SearchManagementClient(
                new AzureCredentialsFactory().FromServicePrincipal(
                    Settings.ClientId,
                    Settings.ClientSecret,
                    Settings.TenantId,
                    AzureEnvironment.AzureGlobalCloud))
            {
                SubscriptionId = Settings.SubscriptionId
            };

        /// <summary>
        /// Automatically delete the Search Service when the resources are no
        /// longer needed.
        /// </summary>
        public async ValueTask DisposeAsync() => await DeleteSearchSeviceAsync();

        /// <summary>
        /// Delete the Search Service created as a test resource.
        /// </summary>
        private async Task DeleteSearchSeviceAsync()
        {
            // Only delete the Search Service if we own it
            if (RequiresCleanup)
            {
                SearchManagementClient client = GetManagementClient();
                await client.Services.DeleteAsync(Settings.ResourceGroup, SearchServiceName);
                RequiresCleanup = false;
            }
        }

        /// <summary>
        /// Create a new Search Service.
        /// </summary>
        /// <returns>This TestResources context.</returns>
        private async Task<SearchResources> CreateSearchServiceAsync()
        {
            // We don't create any live resources during playback
            if (TestFixture.Mode != RecordedTestMode.Playback)
            {
                SearchManagementClient client = GetManagementClient();

                // Ensuring a Search Service involves creating it, and then
                // waiting until its DNS resolves.  Since reliability is
                // paramount we retry the entire sequence several times,
                // deleting and trying to re-create the service each time.
                int maxAttempts = 10;
                for (int attempt = 0; attempt <= maxAttempts; attempt++)
                {
                    if (attempt == maxAttempts)
                    {
                        throw new InvalidOperationException("Failed to provision a Search Service in a timely manner.");
                    }

                    // Use a new random name for the Search Service every time
                    // we try to create it
                    SearchServiceName = Settings.ResourcePrefix + Settings.Random.GetName(8);

                    // Create a free Search Service
                    await client.Services.CreateOrUpdateAsync(
                        Settings.ResourceGroup,
                        SearchServiceName,
                        new SearchService
                        {
                            Location = Settings.Location,
                            Sku = new Sku { Name = SkuName.Free }
                        });

                    // In the common case, DNS propagation happens in less than
                    // 15 seconds. In the uncommon case, it can take many
                    // minutes.  Deleting and recreating the service is often
                    // faster than waiting a long time for DNS propagation.
                    if (await WaitForSearchServiceDnsAsync(Endpoint, TimeSpan.FromSeconds(15)))
                    {
                        break;
                    }

                    // If the service DNS isn't resolvable in a timely manner,
                    // delete it and try to create another one.  We need to
                    // delete it since there can be only a few free services
                    // per subscription.
                    await client.Services.DeleteAsync(Settings.ResourceGroup, SearchServiceName);
                }

                // Make sure we delete this resource when we're finished
                RequiresCleanup = true;

                // Get the primary admin key
                AdminKeyResult admin = await client.AdminKeys.GetAsync(
                    Settings.ResourceGroup,
                    SearchServiceName);
                Assert.NotNull(admin);
                PrimaryApiKey = admin.PrimaryKey;

                // Get a query key
                IEnumerable<QueryKey> queryKeys = await client.QueryKeys.ListBySearchServiceAsync(
                    Settings.ResourceGroup,
                    SearchServiceName);
                Assert.NotNull(queryKeys);
                QueryApiKey = queryKeys.First().Key;
            }

            return this;
        }

        /// <summary>
        /// Create a new Search Service and empty Hotels Index.
        /// </summary>
        /// <returns>This TestResources context.</returns>
        private async Task<SearchResources> CreateSearchServiceAndIndexAsync()
        {
            // Create the Search Service first
            await CreateSearchServiceAsync();

            // Create the index
            if (TestFixture.Mode != RecordedTestMode.Playback)
            {
                // Generate a random Index Name
                IndexName = Settings.Random.GetName(8);

                SearchServiceClient client = new SearchServiceClient(Endpoint, new AzureKeyCredential(PrimaryApiKey));
                await client.CreateIndexAsync(GetHotelIndex(IndexName));

                // Give the index time to stabilize before running tests.
                await WaitForIndexCreationAsync();
            }

            return this;
        }

        /// <summary>
        /// Create a new Search Service, Hotels Index, and fill it with
        /// test documents.
        /// </summary>
        /// <returns>This TestResources context.</returns>
        private async Task<SearchResources> CreateSearchServiceIndexAndDocumentsAsync()
        {
            // Create the Search Service and Index first
            await CreateSearchServiceAndIndexAsync();

            // Upload the documents
            if (TestFixture.Mode != RecordedTestMode.Playback)
            {
                SearchIndexClient client = new SearchIndexClient(Endpoint, IndexName, new AzureKeyCredential(PrimaryApiKey));
                IndexDocumentsBatch<Hotel> batch = IndexDocumentsBatch.Upload(TestDocuments);
                await client.IndexDocumentsAsync(batch);

                await WaitForIndexingAsync();
            }

            return this;
        }

        /// <summary>
        /// Wait for the index to stabilize.
        /// </summary>
        /// <remarks>
        /// The default delay is 20s, but can be configured by setting the <c>AZURE_SEARCH_CREATE_DELAY</c>
        /// environment variable to the number of seconds you want to wait otherwise.
        /// </remarks>
        /// <returns>A Task to await.</returns>
        public async Task WaitForIndexCreationAsync()
        {
            TimeSpan delay = TimeSpan.FromSeconds(20);

            string value = Environment.GetEnvironmentVariable("AZURE_SEARCH_CREATE_DELAY");
            if (int.TryParse(value, out int numValue))
            {
                delay = TimeSpan.FromSeconds(numValue);
            }

            await TestFixture.DelayAsync(delay);
        }

        /// <summary>
        /// Wait for uploaded documents to be indexed.
        /// </summary>
        /// <returns>A Task to wait.</returns>
        public async Task WaitForIndexingAsync() =>
            await TestFixture.DelayAsync(TimeSpan.FromSeconds(2));

        /// <summary>
        /// Wait for the synonym map to be updated.
        /// </summary>
        /// <returns>A Task to wait.</returns>
        public async Task WaitForSynonymMapUpdateAsync() =>
            await TestFixture.DelayAsync(TimeSpan.FromSeconds(5));

        /// <summary>
        /// Wait for the Search Service to be provisioned.
        /// </summary>
        /// <returns>A Task to wait.</returns>
        public async Task WaitForServiceProvisioningAsync() =>
            await TestFixture.DelayAsync(TimeSpan.FromSeconds(10));

        /// <summary>
        /// Wait for DNS to propagate.
        /// </summary>
        /// <param name="endpoint">The URI to check for.</param>
        /// <param name="maxDelay">The maximum delay to wait</param>
        /// <returns>True if the DNS resolves, false otherwise.</returns>
        private static async Task<bool> WaitForSearchServiceDnsAsync(Uri endpoint, TimeSpan maxDelay)
        {
            // Check once a second
            TimeSpan retryDelay = TimeSpan.FromSeconds(1);
            int maxRetries = (int)(maxDelay.TotalSeconds / retryDelay.TotalSeconds);
            int retries = 0;
            while (retries < maxRetries)
            {
                try
                {
                    CancellationTokenSource cancel = new CancellationTokenSource();
                    cancel.CancelAfter(retryDelay);
                    TaskFactory factory = new TaskFactory(cancel.Token);
                    await factory.FromAsync(Dns.BeginGetHostEntry, Dns.EndGetHostEntry, endpoint.Host, null);
                    return true;
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("No such host is known"))
                    {
                        return false;
                    }
                }
                await Task.Delay(retryDelay);
                retries++;
            }

            return false;
        }
        #endregion Search Service Management
    }
}
