// 
// Copyright (c) Microsoft and contributors.  All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// 
// See the License for the specific language governing permissions and
// limitations under the License.
// 

// Warning: This code was generated by a tool.
// 
// Changes to this file may cause incorrect behavior and will be lost if the
// code is regenerated.

using System;
using System.Linq;

namespace Microsoft.Azure.Management.Sql.Models
{
    /// <summary>
    /// Update Azure Sql Database recommended index properties.
    /// </summary>
    public partial class RecommendedIndexUpdateProperties
    {
        private string _state;
        
        /// <summary>
        /// Optional. Gets or sets requested index state. We execute or cancel
        /// index operations by altering index state. Allowed state
        /// transitions are :Active          -> Pending          - Start index
        /// creation processPending         -> Active           - Cancel index
        /// creationActive/Pending  -> Ignored          - Ignore index
        /// recommendation so it will no longer show in active
        /// recommendationsIgnored         -> Active           - Restore index
        /// recommendationSuccess         -> Pending Revert   - Revert index
        /// that has been createdPending Revert  -> Revert Canceled  - Cancel
        /// index revert operation
        /// </summary>
        public string State
        {
            get { return this._state; }
            set { this._state = value; }
        }
        
        /// <summary>
        /// Initializes a new instance of the RecommendedIndexUpdateProperties
        /// class.
        /// </summary>
        public RecommendedIndexUpdateProperties()
        {
        }
    }
}