// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.Network.Models
{
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// NetworkVirtualAppliance Resource.
    /// </summary>
    [Rest.Serialization.JsonTransformation]
    public partial class NetworkVirtualAppliance : Resource
    {
        /// <summary>
        /// Initializes a new instance of the NetworkVirtualAppliance class.
        /// </summary>
        public NetworkVirtualAppliance()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the NetworkVirtualAppliance class.
        /// </summary>
        /// <param name="id">Resource ID.</param>
        /// <param name="name">Resource name.</param>
        /// <param name="type">Resource type.</param>
        /// <param name="location">Resource location.</param>
        /// <param name="tags">Resource tags.</param>
        /// <param name="bootStrapConfigurationBlob">BootStrapConfigurationBlob
        /// storage URLs.</param>
        /// <param name="virtualHub">The Virtual Hub where Network Virtual
        /// Appliance is being deployed.</param>
        /// <param name="cloudInitConfigurationBlob">CloudInitConfigurationBlob
        /// storage URLs.</param>
        /// <param name="virtualApplianceAsn">VirtualAppliance ASN.</param>
        /// <param name="virtualApplianceNics">List of Virtual Appliance
        /// Network Interfaces.</param>
        /// <param name="provisioningState">The provisioning state of the
        /// resource. Possible values include: 'Succeeded', 'Updating',
        /// 'Deleting', 'Failed'</param>
        /// <param name="identity">The service principal that has read access
        /// to cloud-init and config blob.</param>
        /// <param name="sku">Network Virtual Appliance SKU.</param>
        /// <param name="etag">A unique read-only string that changes whenever
        /// the resource is updated.</param>
        public NetworkVirtualAppliance(string id = default(string), string name = default(string), string type = default(string), string location = default(string), IDictionary<string, string> tags = default(IDictionary<string, string>), IList<string> bootStrapConfigurationBlob = default(IList<string>), SubResource virtualHub = default(SubResource), IList<string> cloudInitConfigurationBlob = default(IList<string>), long? virtualApplianceAsn = default(long?), IList<VirtualApplianceNicProperties> virtualApplianceNics = default(IList<VirtualApplianceNicProperties>), string provisioningState = default(string), ManagedServiceIdentity identity = default(ManagedServiceIdentity), VirtualApplianceSkuProperties sku = default(VirtualApplianceSkuProperties), string etag = default(string))
            : base(id, name, type, location, tags)
        {
            BootStrapConfigurationBlob = bootStrapConfigurationBlob;
            VirtualHub = virtualHub;
            CloudInitConfigurationBlob = cloudInitConfigurationBlob;
            VirtualApplianceAsn = virtualApplianceAsn;
            VirtualApplianceNics = virtualApplianceNics;
            ProvisioningState = provisioningState;
            Identity = identity;
            Sku = sku;
            Etag = etag;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets bootStrapConfigurationBlob storage URLs.
        /// </summary>
        [JsonProperty(PropertyName = "properties.bootStrapConfigurationBlob")]
        public IList<string> BootStrapConfigurationBlob { get; set; }

        /// <summary>
        /// Gets or sets the Virtual Hub where Network Virtual Appliance is
        /// being deployed.
        /// </summary>
        [JsonProperty(PropertyName = "properties.virtualHub")]
        public SubResource VirtualHub { get; set; }

        /// <summary>
        /// Gets or sets cloudInitConfigurationBlob storage URLs.
        /// </summary>
        [JsonProperty(PropertyName = "properties.cloudInitConfigurationBlob")]
        public IList<string> CloudInitConfigurationBlob { get; set; }

        /// <summary>
        /// Gets or sets virtualAppliance ASN.
        /// </summary>
        [JsonProperty(PropertyName = "properties.virtualApplianceAsn")]
        public long? VirtualApplianceAsn { get; set; }

        /// <summary>
        /// Gets list of Virtual Appliance Network Interfaces.
        /// </summary>
        [JsonProperty(PropertyName = "properties.virtualApplianceNics")]
        public IList<VirtualApplianceNicProperties> VirtualApplianceNics { get; private set; }

        /// <summary>
        /// Gets the provisioning state of the resource. Possible values
        /// include: 'Succeeded', 'Updating', 'Deleting', 'Failed'
        /// </summary>
        [JsonProperty(PropertyName = "properties.provisioningState")]
        public string ProvisioningState { get; private set; }

        /// <summary>
        /// Gets or sets the service principal that has read access to
        /// cloud-init and config blob.
        /// </summary>
        [JsonProperty(PropertyName = "identity")]
        public ManagedServiceIdentity Identity { get; set; }

        /// <summary>
        /// Gets or sets network Virtual Appliance SKU.
        /// </summary>
        [JsonProperty(PropertyName = "sku")]
        public VirtualApplianceSkuProperties Sku { get; set; }

        /// <summary>
        /// Gets a unique read-only string that changes whenever the resource
        /// is updated.
        /// </summary>
        [JsonProperty(PropertyName = "etag")]
        public string Etag { get; private set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (VirtualApplianceAsn > 4294967295)
            {
                throw new ValidationException(ValidationRules.InclusiveMaximum, "VirtualApplianceAsn", 4294967295);
            }
            if (VirtualApplianceAsn < 0)
            {
                throw new ValidationException(ValidationRules.InclusiveMinimum, "VirtualApplianceAsn", 0);
            }
        }
    }
}
