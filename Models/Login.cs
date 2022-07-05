using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDevTechAss1.Models
{
    public class Login
    {
        public string LoginID { get; }
        public int CustomerID { get; }
        public string PasswordHash { get; }

        public Login(string loginID, int customerID, string passwordHash )
        {
            LoginID = loginID;
            CustomerID = customerID;
            PasswordHash = passwordHash;

        }

    }
}