using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using WebDevTechAss1.Models;

namespace WebDevTechAss1.Controllers
{
    public class CustomerController
    {

        private static IConfigurationRoot Configuration { get; } =
                new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            private static string ConnectionString { get; } = Configuration["ConnectionString"];


        public async Task addCustomer()
        {

            Customer acc = new Customer(1,"Connor","My House","My City","4000");
        
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            var cmd = connection.CreateCommand();

            cmd.Connection = connection;

            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "insert into dbo.Customer (CustomerID, Name, Address, City, PostCode) values (@customerID,@name,@address,@city,@postCode)";

            cmd.Parameters.AddWithValue("customerID", acc.CustomerID);
            cmd.Parameters.AddWithValue("name", acc.Name);
            cmd.Parameters.AddWithValue("address", acc.Address);
            cmd.Parameters.AddWithValue("city ", acc.City);
            cmd.Parameters.AddWithValue("postCode ", acc.PostCode);
            
            cmd.ExecuteNonQuery();
            
        }

    }
}