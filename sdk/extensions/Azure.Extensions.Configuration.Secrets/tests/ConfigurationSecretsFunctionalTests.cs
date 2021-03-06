﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading.Tasks;
using Azure.Core.Testing;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Azure.AspNetCore.DataProtection.Blobs.Tests
{
    public class ConfigurationSecretsFunctionalTests
    {
        private static readonly string TenantId = Environment.GetEnvironmentVariable("AZURE_TENANT_ID");
        private static readonly string ClientId = Environment.GetEnvironmentVariable("AZURE_CLIENT_ID");
        private static readonly string ClientSecret = Environment.GetEnvironmentVariable("AZURE_CLIENT_SECRET");
        private static readonly string KeyVaultUrl = Environment.GetEnvironmentVariable("AZURE_KEYVAULT_URL");

        [Test]
        [Category("Live")]
        public async Task SecretsAreLoadedFromKeyVault()
        {
            var credential = new ClientSecretCredential(TenantId, ClientId, ClientSecret);
            var vaultUri = new Uri(KeyVaultUrl);

            var client = new SecretClient(vaultUri, credential);
            await client.SetSecretAsync("TestSecret1", "1");
            await client.SetSecretAsync("TestSecret2", "2");
            await client.SetSecretAsync("Nested--TestSecret3", "3");

            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddAzureKeyVault(vaultUri, credential);

            IConfigurationRoot configuration = configurationBuilder.Build();

            Assert.AreEqual("1", configuration["TestSecret1"]);
            Assert.AreEqual("2", configuration["TestSecret2"]);
            Assert.AreEqual("3", configuration["Nested:TestSecret3"]);

            await client.SetSecretAsync("TestSecret1", "2");
            configuration.Reload();

            Assert.AreEqual("2", configuration["TestSecret1"]);
            Assert.AreEqual("2", configuration["TestSecret2"]);
            Assert.AreEqual("3", configuration["Nested:TestSecret3"]);
        }
    }
}