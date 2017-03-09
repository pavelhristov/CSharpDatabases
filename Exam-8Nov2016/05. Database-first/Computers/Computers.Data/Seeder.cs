using Computers.Data.Data;
using System.Linq;

namespace Computers.Data
{
    public static class Seeder
    {
        public static void SeedRandomEntities()
        {
            var computersEntities = new ComputersDbEntities();

            for (int i = 0; i < 5; i++)
            {
                computersEntities.GPUVendors.Add(new GPUVendors()
                {
                    Name = RandomGenerator.RandomString(3, 20)
                });
            }

            computersEntities.SaveChanges();

            for (int i = 0; i < 15; i++)
            {
                computersEntities.GPUs.Add(new GPUs()
                {
                    TypeId = RandomGenerator.RandomNumber(1, 2),
                    Model = RandomGenerator.RandomString(3, 20),
                    VendorId = RandomGenerator.RandomNumber(1, computersEntities.GPUVendors.Count()),
                    Memory = RandomGenerator.RandomNumber(1, 16) + "GB"
                });
            }

            computersEntities.SaveChanges();

            for (int i = 0; i < 5; i++)
            {
                computersEntities.CPUVendors.Add(new CPUVendors()
                {
                    Name = RandomGenerator.RandomString(3, 20)
                });
            }

            computersEntities.SaveChanges();

            for (int i = 0; i < 10; i++)
            {
                computersEntities.CPUs.Add(new CPUs()
                {
                    ClockCycle = RandomGenerator.RandomNumber(1, 3) + "." + RandomGenerator.RandomNumber(0, 9) + " GHz",
                    VendorId = RandomGenerator.RandomNumber(1, computersEntities.CPUVendors.Count()),
                    Model = RandomGenerator.RandomString(3, 20)
                });
            }

            computersEntities.SaveChanges();

            for (int i = 0; i < 5; i++)
            {
                computersEntities.StorageDeviceVendors.Add(new StorageDeviceVendors()
                {
                    Name = RandomGenerator.RandomString(3, 20)
                });
            }

            computersEntities.SaveChanges();

            for (int i = 0; i < 30; i++)
            {
                computersEntities.StorageDevices.Add(new StorageDevices()
                {
                    Model = RandomGenerator.RandomString(3, 20),
                    Size = RandomGenerator.RandomNumber(80, 1000) + " GB",
                    TypeId = RandomGenerator.RandomNumber(1, 2),
                    VendorId = RandomGenerator.RandomNumber(1, computersEntities.StorageDeviceVendors.Count())
                });
            }

            computersEntities.SaveChanges();

            for (int i = 0; i < 5; i++)
            {
                computersEntities.ComputerVendors.Add(new ComputerVendors()
                {
                    Name = RandomGenerator.RandomString(3, 20)
                });
            }

            computersEntities.SaveChanges();

            for (int i = 0; i < 50; i++)
            {
                computersEntities.Computers.Add(new Data.Computers()
                {
                    TypeId = i % 3 + 1,
                    VendorId = RandomGenerator.RandomNumber(1, computersEntities.ComputerVendors.Count()),
                    GPUId = RandomGenerator.RandomNumber(1, computersEntities.GPUs.Count()),
                    CPUId = RandomGenerator.RandomNumber(1, computersEntities.CPUs.Count()),
                    StorageDeviceId = RandomGenerator.RandomNumber(1, computersEntities.StorageDevices.Count()),
                    Memory = RandomGenerator.RandomNumber(4, 64) + " GB",
                    Model = RandomGenerator.RandomString(3, 20)
                });
            }

            computersEntities.SaveChanges();
        }
    }
}
