// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.Logic.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// The parameters to generate upgraded definition.
    /// </summary>
    public partial class GenerateUpgradedDefinitionParameters
    {
        /// <summary>
        /// Initializes a new instance of the
        /// GenerateUpgradedDefinitionParameters class.
        /// </summary>
        public GenerateUpgradedDefinitionParameters()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// GenerateUpgradedDefinitionParameters class.
        /// </summary>
        /// <param name="targetSchemaVersion">The target schema
        /// version.</param>
        public GenerateUpgradedDefinitionParameters(string targetSchemaVersion = default(string))
        {
            TargetSchemaVersion = targetSchemaVersion;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets the target schema version.
        /// </summary>
        [JsonProperty(PropertyName = "targetSchemaVersion")]
        public string TargetSchemaVersion { get; set; }

    }
}