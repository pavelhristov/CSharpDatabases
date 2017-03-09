using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess.Metadata.Fluent;

namespace TelerikDataAccess.Models
{
    public partial class FluentModelMetadataSource : FluentMetadataSource
    {
        protected override IList<MappingConfiguration> PrepareMapping()
        {
            List<MappingConfiguration> configurations =
                new List<MappingConfiguration>();

            var customerMapping = new MappingConfiguration<Customer>();
            customerMapping.MapType(customer => new
            {
                ID = customer.ID,
                Name = customer.Name,
                EmailAddress = customer.EmailAddress,
                DateCreated = customer.DateCreated
            }).ToTable("Customer");
            customerMapping.HasProperty(c => c.ID).IsIdentity();

            configurations.Add(customerMapping);

            return configurations;
        }
    }
}
