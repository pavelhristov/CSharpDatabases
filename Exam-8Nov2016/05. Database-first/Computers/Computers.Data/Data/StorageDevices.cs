//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Computers.Data.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class StorageDevices
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StorageDevices()
        {
            this.Computers = new HashSet<Computers>();
        }
    
        public int Id { get; set; }
        public int VendorId { get; set; }
        public string Model { get; set; }
        public int TypeId { get; set; }
        public string Size { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Computers> Computers { get; set; }
        public virtual StorageDeviceTypes StorageDeviceTypes { get; set; }
        public virtual StorageDeviceVendors StorageDeviceVendors { get; set; }
    }
}
