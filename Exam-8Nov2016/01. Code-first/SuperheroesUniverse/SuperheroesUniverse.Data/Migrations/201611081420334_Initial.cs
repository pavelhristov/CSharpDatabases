namespace SuperheroesUniverse.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alignments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AlignmentValue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Fractions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Alignment_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Alignments", t => t.Alignment_Id)
                .Index(t => t.Name, unique: true)
                .Index(t => t.Alignment_Id);
            
            CreateTable(
                "dbo.Planets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Fraction_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fractions", t => t.Fraction_Id)
                .Index(t => t.Name, unique: true)
                .Index(t => t.Fraction_Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Planet_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Planets", t => t.Planet_Id, cascadeDelete: true)
                .Index(t => t.Name, unique: true)
                .Index(t => t.Planet_Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Country_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id, cascadeDelete: true)
                .Index(t => t.Name, unique: true)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.Superheroes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60),
                        SecretIdentity = c.String(nullable: false, maxLength: 20),
                        Story = c.String(nullable: false),
                        alignment_Id = c.Int(nullable: false),
                        city_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Alignments", t => t.alignment_Id, cascadeDelete: true)
                .ForeignKey("dbo.Cities", t => t.city_Id, cascadeDelete: true)
                .Index(t => t.SecretIdentity, unique: true)
                .Index(t => t.alignment_Id)
                .Index(t => t.city_Id);
            
            CreateTable(
                "dbo.Powers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 35),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Relationships",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SuperheroId1 = c.Int(nullable: false),
                        SuperheroId2 = c.Int(nullable: false),
                        RelationshipType = c.Int(nullable: false),
                        Superhero1_Id = c.Int(),
                        Superhero2_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Superheroes", t => t.Superhero1_Id)
                .ForeignKey("dbo.Superheroes", t => t.Superhero2_Id)
                .Index(t => t.Superhero1_Id)
                .Index(t => t.Superhero2_Id);
            
            CreateTable(
                "dbo.SuperheroFractions",
                c => new
                    {
                        Superhero_Id = c.Int(nullable: false),
                        Fraction_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Superhero_Id, t.Fraction_Id })
                .ForeignKey("dbo.Superheroes", t => t.Superhero_Id, cascadeDelete: true)
                .ForeignKey("dbo.Fractions", t => t.Fraction_Id, cascadeDelete: true)
                .Index(t => t.Superhero_Id)
                .Index(t => t.Fraction_Id);
            
            CreateTable(
                "dbo.PowerSuperheroes",
                c => new
                    {
                        Power_Id = c.Int(nullable: false),
                        Superhero_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Power_Id, t.Superhero_Id })
                .ForeignKey("dbo.Powers", t => t.Power_Id, cascadeDelete: true)
                .ForeignKey("dbo.Superheroes", t => t.Superhero_Id, cascadeDelete: true)
                .Index(t => t.Power_Id)
                .Index(t => t.Superhero_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Relationships", "Superhero2_Id", "dbo.Superheroes");
            DropForeignKey("dbo.Relationships", "Superhero1_Id", "dbo.Superheroes");
            DropForeignKey("dbo.Planets", "Fraction_Id", "dbo.Fractions");
            DropForeignKey("dbo.Countries", "Planet_Id", "dbo.Planets");
            DropForeignKey("dbo.PowerSuperheroes", "Superhero_Id", "dbo.Superheroes");
            DropForeignKey("dbo.PowerSuperheroes", "Power_Id", "dbo.Powers");
            DropForeignKey("dbo.SuperheroFractions", "Fraction_Id", "dbo.Fractions");
            DropForeignKey("dbo.SuperheroFractions", "Superhero_Id", "dbo.Superheroes");
            DropForeignKey("dbo.Superheroes", "city_Id", "dbo.Cities");
            DropForeignKey("dbo.Superheroes", "alignment_Id", "dbo.Alignments");
            DropForeignKey("dbo.Cities", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.Fractions", "Alignment_Id", "dbo.Alignments");
            DropIndex("dbo.PowerSuperheroes", new[] { "Superhero_Id" });
            DropIndex("dbo.PowerSuperheroes", new[] { "Power_Id" });
            DropIndex("dbo.SuperheroFractions", new[] { "Fraction_Id" });
            DropIndex("dbo.SuperheroFractions", new[] { "Superhero_Id" });
            DropIndex("dbo.Relationships", new[] { "Superhero2_Id" });
            DropIndex("dbo.Relationships", new[] { "Superhero1_Id" });
            DropIndex("dbo.Powers", new[] { "Name" });
            DropIndex("dbo.Superheroes", new[] { "city_Id" });
            DropIndex("dbo.Superheroes", new[] { "alignment_Id" });
            DropIndex("dbo.Superheroes", new[] { "SecretIdentity" });
            DropIndex("dbo.Cities", new[] { "Country_Id" });
            DropIndex("dbo.Cities", new[] { "Name" });
            DropIndex("dbo.Countries", new[] { "Planet_Id" });
            DropIndex("dbo.Countries", new[] { "Name" });
            DropIndex("dbo.Planets", new[] { "Fraction_Id" });
            DropIndex("dbo.Planets", new[] { "Name" });
            DropIndex("dbo.Fractions", new[] { "Alignment_Id" });
            DropIndex("dbo.Fractions", new[] { "Name" });
            DropTable("dbo.PowerSuperheroes");
            DropTable("dbo.SuperheroFractions");
            DropTable("dbo.Relationships");
            DropTable("dbo.Powers");
            DropTable("dbo.Superheroes");
            DropTable("dbo.Cities");
            DropTable("dbo.Countries");
            DropTable("dbo.Planets");
            DropTable("dbo.Fractions");
            DropTable("dbo.Alignments");
        }
    }
}
