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
    
    public partial class Computers
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int VendorId { get; set; }
        public string Model { get; set; }
        public string Memory { get; set; }
        public int GPUId { get; set; }
        public int CPUId { get; set; }
        public int StorageDeviceId { get; set; }
    
        public virtual ComputerTypes ComputerTypes { get; set; }
        public virtual ComputerVendors ComputerVendors { get; set; }
        public virtual CPUs CPUs { get; set; }
        public virtual GPUs GPUs { get; set; }
        public virtual StorageDevices StorageDevices { get; set; }
    }
}