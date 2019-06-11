// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.DeploymentManager.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Defines headers for CreateOrUpdate operation.
    /// </summary>
    public partial class ServiceUnitsCreateOrUpdateHeaders
    {
        /// <summary>
        /// Initializes a new instance of the ServiceUnitsCreateOrUpdateHeaders
        /// class.
        /// </summary>
        public ServiceUnitsCreateOrUpdateHeaders()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ServiceUnitsCreateOrUpdateHeaders
        /// class.
        /// </summary>
        /// <param name="azureAsyncOperation">Contains the status URL on which
        /// clients are expected to poll the status of the operation.</param>
        public ServiceUnitsCreateOrUpdateHeaders(string azureAsyncOperation = default(string))
        {
            AzureAsyncOperation = azureAsyncOperation;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets contains the status URL on which clients are expected
        /// to poll the status of the operation.
        /// </summary>
        [JsonProperty(PropertyName = "Azure-AsyncOperation")]
        public string AzureAsyncOperation { get; set; }

    }
}