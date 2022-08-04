using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDevTechAss1.Models
{
    public class Account
    {
        public int AccountNumber { get; set; }
        public char AccountType { get; set; }
        public int CustomerID { get; set; }
        public decimal Balance { get; set; }
        public Transaction[] Transactions { get; set; }

        public Account(int accountNumber, char accountType, int customerID, int balance)
        {
            AccountNumber = accountNumber;
            AccountType = accountType;
            CustomerID = customerID;
            Balance = balance;
        }
    }
}