using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelerikDataAccess.Models
{
    public class Customer
    {
        public Customer()
        {
            DateCreated = DateTime.Now;
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public string EmailAddress { get; set; }
    }
}
