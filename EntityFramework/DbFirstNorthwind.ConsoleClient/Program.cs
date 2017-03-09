using DbFirstNorthwind.ConsoleClient.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFirstNorthwind.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new NorthwindEntities())
            {

            }
        }

        static int CreateNewProduct(string firstName, string lastName)
        {
            NorthwindEntities northwindEntities = new NorthwindEntities();
            Employees newEmployee = new Employees
            {
                FirstName = firstName,
                LastName = lastName
            };
            northwindEntities.Employees.Add(newEmployee);
            northwindEntities.SaveChanges();
            return newEmployee.EmployeeID;
        }
    }
}
