namespace StudentSystem.Data.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<StudentSystem.Data.StudentSystemDbContext>
    {
        public Configuration()
        {
            // I'm aware its true!
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(StudentSystem.Data.StudentSystemDbContext context)
        {
            context.Students.Add(new Student()
            {
                Name = "Pesho",
                Number = "8734632848AB"
            });

            context.Students.Add(new Student()
            {
                Name = "Gosho",
                Number = "555whoCares"
            });

            context.Courses.Add(new Course()
            {
                Name = "Complex Calculus",
                Description = "Beginner mathematics for everyone!"
            });

            context.StudentsCourses.Add(new StudentCourse()
            {
                StudentId = 1,
                CourseId = 1,
            });

            context.StudentsCourses.Add(new StudentCourse()
            {
                StudentId = 2,
                CourseId = 1,
            });

            context.Homeworks.Add(new Homework()
            {
                StudentId = 1,
                TimeSent = DateTime.Now
            });
        }
    }
}
