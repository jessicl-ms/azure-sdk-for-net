// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.CognitiveServices.Language.LUIS.Authoring.Models
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Defines the Azure account information object.
    /// </summary>
    public partial class AzureAccountInfoObject
    {
        /// <summary>
        /// Initializes a new instance of the AzureAccountInfoObject class.
        /// </summary>
        public AzureAccountInfoObject()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the AzureAccountInfoObject class.
        /// </summary>
        /// <param name="azureSubscriptionId">The id for the Azure
        /// subscription.</param>
        /// <param name="resourceGroup">The Azure resource group name.</param>
        /// <param name="accountName">The Azure account name.</param>
        public AzureAccountInfoObject(string azureSubscriptionId, string resourceGroup, string accountName)
        {
            AzureSubscriptionId = azureSubscriptionId;
            ResourceGroup = resourceGroup;
            AccountName = accountName;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets the id for the Azure subscription.
        /// </summary>
        [JsonProperty(PropertyName = "azureSubscriptionId")]
        public string AzureSubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the Azure resource group name.
        /// </summary>
        [JsonProperty(PropertyName = "resourceGroup")]
        public string ResourceGroup { get; set; }

        /// <summary>
        /// Gets or sets the Azure account name.
        /// </summary>
        [JsonProperty(PropertyName = "accountName")]
        public string AccountName { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (AzureSubscriptionId == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "AzureSubscriptionId");
            }
            if (ResourceGroup == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "ResourceGroup");
            }
            if (AccountName == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "AccountName");
            }
        }
    }
}
