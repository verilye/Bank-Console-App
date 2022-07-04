using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDevTechAss1.Models
{
    public class Customer
    {
        public int CustomerID { get; }
        public string Name { get; }
        public string Address { get; }
        public string City { get; }
        public string PostCode { get; }

        public Customer(int customerID, string name, string address, string city, string postCode)
        {
            CustomerID = customerID;
            Name = name;
            Address = address;
            City = city;
            PostCode = postCode;

        }
    }
}