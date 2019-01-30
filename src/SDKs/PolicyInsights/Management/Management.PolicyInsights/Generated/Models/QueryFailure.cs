// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.PolicyInsights.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Error response.
    /// </summary>
    public partial class QueryFailure
    {
        /// <summary>
        /// Initializes a new instance of the QueryFailure class.
        /// </summary>
        public QueryFailure()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the QueryFailure class.
        /// </summary>
        /// <param name="error">Error definition.</param>
        public QueryFailure(QueryFailureError error = default(QueryFailureError))
        {
            Error = error;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets error definition.
        /// </summary>
        [JsonProperty(PropertyName = "error")]
        public QueryFailureError Error { get; set; }

    }
}