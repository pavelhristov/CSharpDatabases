using StudentSystem.Data;
using StudentSystem.Data.Migrations;
using StudentSystem.Models;
using System.Data.Entity;

namespace StudentSystem.ConsoleClient
{
    class StartUp
    {
        static void Main(string[] args)
        {
            // last minute job, could not finish!
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentSystemDbContext, Configuration>());

            using (var dbContext = new StudentSystemDbContext())
            {
                dbContext.Database.CreateIfNotExists();

                dbContext.Students.Add(new Student()
                {
                    Name = "Pesho2",
                    Number = "123noonecares123"
                });
                dbContext.SaveChanges();
            }
        }
    }
}
