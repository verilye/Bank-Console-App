using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDevTechAss1.Models
{
    public class Transaction
    {
        public int TransactionID { get; }
        public string TransactionType { get; }
        public int AccountNumber { get; }
        public int DestinationAccountNumber { get; }
        public int Amount { get; }
        public string Comment { get; }
        public string TransactionTimeUtc { get; }

        public Transaction(int transactionID, string transactionType, int accountNumber,
            int destinationAccountNumber, int  amount, string comment, string transactionTimeUtc)
        {

            TransactionID = transactionID;
            TransactionType = transactionType;
            AccountNumber = accountNumber;
            DestinationAccountNumber = destinationAccountNumber;
            Amount = amount;
            Comment = comment;
            TransactionTimeUtc = transactionTimeUtc;
    
        }

    }
}