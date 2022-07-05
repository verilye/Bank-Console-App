using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDevTechAss1.Models
{
    public class Login
    {
        public string LoginID { get;set; }
        public string PasswordHash { get;set; }

        public Login(string loginID, string passwordHash )
        {
            LoginID = loginID;
            PasswordHash = passwordHash;

        }

    }
}