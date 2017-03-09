namespace SocialNetwork.ConsoleClient
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Searcher;
    using Data;
    using SocialNetwork.Data.Migrations;

    public class Startup
    {
        public static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SocialNetworkDbContext, Configuration>());

            using (var dbContex = new SocialNetworkDbContext())
            {
                dbContex.Database.CreateIfNotExists();
            }
        }
    }
}
