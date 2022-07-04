using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDevTechAss1.Models
{
    public class Account
    {
        public int AccountNumber { get; }
        public char AccountType { get; }
        public int CustomerID { get; }
        public int Balance { get; }

        public Account(int accountNumber, char accountType, int customerID, int balance)
        {
            AccountNumber = accountNumber;
            AccountType = accountType;
            CustomerID = customerID;
            Balance = balance;
        }
    }
}