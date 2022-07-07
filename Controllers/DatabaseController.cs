using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WebDevTechAss1.Models;
using Newtonsoft.Json;
using System.Data;

namespace WebDevTechAss1.Controllers
{
    public class DatabaseController
    {

         //Extract connection string from appsettings.json

        private static IConfigurationRoot Configuration { get; } =
                new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        private static string ConnectionString { get; } = Configuration["ConnectionString"];


        //Check if any customer entries are in the database

        public Boolean checkDb()
        {

            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand(
                "SELECT COUNT(*) FROM dbo.Customer",
                connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            if(reader.HasRows){
                
                return false;
                
            }

            return true;

        }
        
        // Get number of account so that amounts of money can be transferred to
        // and from it

        public int GetAccountNumber(int customerID, char accountType)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand(
                "SELECT AccountNumber FROM [Account] WHERE CustomerID = @customerID AND AccountType = @accountType",
                connection);
            connection.Open();

            command.Parameters.AddWithValue("customerID", customerID);
            command.Parameters.AddWithValue("accountType", accountType);

            int accountNumber = (int)command.ExecuteScalar();

            return accountNumber;
            
        }

        //
        public void UpdateAccountBalance(int accountNumber, decimal amount)
        {   
            try
            {
            
                SqlConnection connection = new SqlConnection(ConnectionString);

                SqlCommand command1 = new SqlCommand(
                    "SELECT Balance FROM [Account] WHERE AccountNumber = @accountNumber",
                    connection);
                connection.Open();

                command1.Parameters.AddWithValue("accountNumber", accountNumber);

                decimal balance = (decimal)command1.ExecuteScalar();

                connection.Close();

                if(amount + balance < 0 )
                {
                    Console.WriteLine("You do not have enough money for this transfer");
                }
                else
                {
                    var newBalance = amount+balance;

                    SqlCommand command2 = new SqlCommand(
                    "UPDATE [Account] SET Balance = @newBalance WHERE AccountNumber = @accountNumber",
                    connection);
                    connection.Open();
                    
                    command2.Parameters.AddWithValue("newBalance", newBalance);
                    command2.Parameters.AddWithValue("accountNumber", accountNumber);

                    command2.ExecuteNonQuery();

                    Console.WriteLine("Transfer successful!");

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            
        }


        // Helper method to get customer ID from username

        public int GetCustomerID(string username)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand(
                "SELECT CustomerID FROM dbo.Login WHERE LoginID = @username ",
                connection);
            connection.Open();

            command.Parameters.AddWithValue("username", username);

            int customerID = (int)command.ExecuteScalar();

            return customerID;
        }

        
        //Compare entered password to hashed password on the database

        public string VerifyLogin(string username)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand(
                "SELECT PasswordHash FROM dbo.Login WHERE LoginID = @username ",
                connection);
            connection.Open();

            command.Parameters.AddWithValue("username", username);

            string password = (string)command.ExecuteScalar();

            return password;

        }

        //Insert deserialised JSON into the SQL database with
        //these helper methods

        public void InsertCustomer(Customer customer)
        {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            var cmd = connection.CreateCommand();

            cmd.Connection = connection;

            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "insert into dbo.Customer (CustomerID, Name, Address, City, PostCode) values (@customerID, @name, @address, @city, @postCode)";

            cmd.Parameters.AddWithValue("customerID", customer.CustomerID);
            cmd.Parameters.AddWithValue("name", customer.Name);
            cmd.Parameters.AddWithValue("address", customer.Address ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("city ", customer.City ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("postCode ", customer.PostCode ?? (object)DBNull.Value);
            
            cmd.ExecuteNonQuery();

        }

        public void InsertAccount(Account account)
        {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            var cmd = connection.CreateCommand();

            cmd.Connection = connection;

            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "insert into dbo.Account (AccountNumber, AccountType, CustomerId, Balance) values (@accountNumber, @accountType, @customerId, @balance)";

            cmd.Parameters.AddWithValue("accountNumber", account.AccountNumber);
            cmd.Parameters.AddWithValue("accountType", account.AccountType);
            cmd.Parameters.AddWithValue("customerId", account.CustomerID);
            cmd.Parameters.AddWithValue("balance", account.Balance);
            
            
            cmd.ExecuteNonQuery();

        }

        public void InsertLogin(Login login, int customerID)
        {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            var cmd = connection.CreateCommand();

            cmd.Connection = connection;

            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "insert into dbo.Login (LoginID,PasswordHash,CustomerID) values (@loginId, @passwordHash, @customerId)";

            cmd.Parameters.AddWithValue("loginId", login.LoginID);
            cmd.Parameters.AddWithValue("passwordHash", login.PasswordHash); 
            cmd.Parameters.AddWithValue("customerId", customerID); 
            
            cmd.ExecuteNonQuery();

        }
        
        public void InsertTransaction(Transaction transaction, char transactionType, int accountNumber, int destinationAccountNumber)
        {


            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            var cmd = connection.CreateCommand();

            cmd.Connection = connection;

            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "insert into [Transaction] (TransactionType,AccountNumber,DestinationAccountNumber,Amount,Comment,TransactionTimeUtc) values (@transactionType, @accountNumber, @destinationAccountNumber, @amount, @comment, @transactionTimeUtc)";

            cmd.Parameters.AddWithValue("transactionType", transactionType); 
            cmd.Parameters.AddWithValue("accountNumber", accountNumber); 

            if(destinationAccountNumber == 0)
            {
                cmd.Parameters.AddWithValue("destinationAccountNumber", (object)DBNull.Value);
            }else
            {
                cmd.Parameters.AddWithValue("destinationAccountNumber", destinationAccountNumber);
            }

            cmd.Parameters.AddWithValue("amount", transaction.Amount);
            cmd.Parameters.AddWithValue("comment", transaction.Comment ?? (object)DBNull.Value); 
            cmd.Parameters.AddWithValue("transactionTimeUtc", transaction.TransactionTimeUtc); 
            
            cmd.ExecuteNonQuery();

        }
    }
}