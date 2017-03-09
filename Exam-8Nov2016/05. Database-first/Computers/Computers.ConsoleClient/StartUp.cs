using Computers.Data;

namespace Computers.ConsoleClient
{
    class StartUp
    {
        static void Main(string[] args)
        {
            Seeder.SeedRandomEntities();
        }
    }
}
