using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;

namespace TelerikDataAccess.Models
{
    public partial class TestDbContext : OpenAccessContext
    {
        private static string connectionStringName = @"connectionId";
        private static BackendConfiguration backend = GetBackendConfiguration();
        private static MetadataSource metadataSource = new FluentModelMetadataSource();

        public TestDbContext()
            : base(connectionStringName, backend, metadataSource)
        { }

        //public IQueryable<Customer> Customers
        //{
        //    get
        //    {
        //        return this.GetAll<Customer>();
        //    }
        //}

        public static BackendConfiguration GetBackendConfiguration()
        {
            BackendConfiguration backend = new BackendConfiguration();
            backend.Backend = "MySql";
            backend.ProviderName = "MySql.Data.MySqlClient";

            return backend;
        }
    }
}
