using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDevTechAss1.Models
{
    public class Transaction
    {
        public decimal Amount { get;set; }
        public string Comment { get;set; }
        public string TransactionTimeUtc { get;set; }

        public Transaction(decimal amount, string comment, string transactionTimeUtc)
        {

            Amount = amount;
            Comment = comment;
            TransactionTimeUtc = transactionTimeUtc;
    
        }

    }
}