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
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.Azure.Management.Network;
using Microsoft.Azure.Management.Network.Models;

namespace Microsoft.Azure.Management.Network
{
    /// <summary>
    /// The Windows Azure Network management API provides a RESTful set of web
    /// services that interact with Windows Azure Networks service to manage
    /// your network resrources. The API has entities that capture the
    /// relationship between an end user and the Windows Azure Networks
    /// service.
    /// </summary>
    public partial interface INetworkResourceProviderClient : IDisposable
    {
        /// <summary>
        /// Gets the API version.
        /// </summary>
        string ApiVersion
        {
            get; 
        }
        
        /// <summary>
        /// Gets the URI used as the base for all cloud service requests.
        /// </summary>
        Uri BaseUri
        {
            get; 
        }
        
        /// <summary>
        /// Gets subscription credentials which uniquely identify Microsoft
        /// Azure subscription. The subscription ID forms part of the URI for
        /// every service call.
        /// </summary>
        SubscriptionCloudCredentials Credentials
        {
            get; 
        }
        
        /// <summary>
        /// Gets or sets the initial timeout for Long Running Operations.
        /// </summary>
        int LongRunningOperationInitialTimeout
        {
            get; set; 
        }
        
        /// <summary>
        /// Gets or sets the retry timeout for Long Running Operations.
        /// </summary>
        int LongRunningOperationRetryTimeout
        {
            get; set; 
        }
        
        /// <summary>
        /// The Network Resource Provider API includes operations managing the
        /// application gateways for your subscription.
        /// </summary>
        IApplicationGatewayOperations ApplicationGateways
        {
            get; 
        }
        
        /// <summary>
        /// The Network Resource Provider API includes operations for managing
        /// the load balancers for your subscription.
        /// </summary>
        ILoadBalancerOperations LoadBalancers
        {
            get; 
        }
        
        /// <summary>
        /// The Network Resource Provider API includes operations for managing
        /// the Virtual network Gateway for your subscription.
        /// </summary>
        ILocalNetworkGatewayOperations LocalNetworkGateways
        {
            get; 
        }
        
        /// <summary>
        /// The Network Resource Provider API includes operations for managing
        /// the subnets for your subscription.
        /// </summary>
        INetworkInterfaceOperations NetworkInterfaces
        {
            get; 
        }
        
        /// <summary>
        /// The Network Resource Provider API includes operations for managing
        /// the NetworkSecurityGroups for your subscription.
        /// </summary>
        INetworkSecurityGroupOperations NetworkSecurityGroups
        {
            get; 
        }
        
        /// <summary>
        /// The Network Resource Provider API includes operations for managing
        /// the PublicIPAddress for your subscription.
        /// </summary>
        IPublicIpAddressOperations PublicIpAddresses
        {
            get; 
        }
        
        /// <summary>
        /// The Network Resource Provider API includes operations for managing
        /// the Routes for your subscription.
        /// </summary>
        IRouteOperations Routes
        {
            get; 
        }
        
        /// <summary>
        /// The Network Resource Provider API includes operations for managing
        /// the RouteTables for your subscription.
        /// </summary>
        IRouteTableOperations RouteTables
        {
            get; 
        }
        
        /// <summary>
        /// The Network Resource Provider API includes operations for managing
        /// the SecurityRules for your subscription.
        /// </summary>
        ISecurityRuleOperations SecurityRules
        {
            get; 
        }
        
        /// <summary>
        /// The Network Resource Provider API includes operations for managing
        /// the subnets for your subscription.
        /// </summary>
        ISubnetOperations Subnets
        {
            get; 
        }
        
        /// <summary>
        /// Operations for listing usage.
        /// </summary>
        IUsageOperations Usages
        {
            get; 
        }
        
        /// <summary>
        /// The Network Resource Provider API includes operations for managing
        /// the Virtual network Gateway for your subscription.
        /// </summary>
        IVirtualNetworkGatewayConnectionOperations VirtualNetworkGatewayConnections
        {
            get; 
        }
        
        /// <summary>
        /// The Network Resource Provider API includes operations for managing
        /// the Virtual network Gateway for your subscription.
        /// </summary>
        IVirtualNetworkGatewayOperations VirtualNetworkGateways
        {
            get; 
        }
        
        /// <summary>
        /// The Network Resource Provider API includes operations for managing
        /// the Virtual Networks for your subscription.
        /// </summary>
        IVirtualNetworkOperations VirtualNetworks
        {
            get; 
        }
        
        /// <summary>
        /// Checks whether a domain name in the cloudapp.net zone is available
        /// for use.
        /// </summary>
        /// <param name='location'>
        /// The location of the domain name
        /// </param>
        /// <param name='domainNameLabel'>
        /// The domain name to be verified. It must conform to the following
        /// regular expression: ^[a-z][a-z0-9-]{1,61}[a-z0-9]$.
        /// </param>
        /// <param name='cancellationToken'>
        /// Cancellation token.
        /// </param>
        /// <returns>
        /// Response for CheckDnsNameAvailability Api servive call
        /// </returns>
        Task<DnsNameAvailabilityResponse> CheckDnsNameAvailabilityAsync(string location, string domainNameLabel, CancellationToken cancellationToken);
        
        /// <summary>
        /// The Get Operation Status operation returns the status of the
        /// specified operation. After calling an asynchronous operation, you
        /// can call Get Operation Status to determine whether the operation
        /// has succeeded, failed, or is still in progress.
        /// </summary>
        /// <param name='azureAsyncOperation'>
        /// Location value returned by the Begin operation.
        /// </param>
        /// <param name='cancellationToken'>
        /// Cancellation token.
        /// </param>
        /// <returns>
        /// The response body contains the status of the specified asynchronous
        /// operation, indicating whether it has succeeded, is inprogress, or
        /// has failed. Note that this status is distinct from the HTTP status
        /// code returned for the Get Operation Status operation itself. If
        /// the asynchronous operation succeeded, the response body includes
        /// the HTTP status code for the successful request. If the
        /// asynchronous operation failed, the response body includes the HTTP
        /// status code for the failed request and error information regarding
        /// the failure.
        /// </returns>
        Task<AzureAsyncOperationResponse> GetLongRunningOperationStatusAsync(string azureAsyncOperation, CancellationToken cancellationToken);
    }
}
