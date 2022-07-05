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

        //Extract connection string from appsettings.json

        private static IConfigurationRoot Configuration { get; } =
                new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        private static string ConnectionString { get; } = Configuration["ConnectionString"];

    
        static readonly HttpClient client = new HttpClient();



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
                
                Console.WriteLine("BING BONG");

                return false;}

            return true;

        }


        //Pull users from webservice and add them to database if no customers are present

        public async Task PreloadData()
        {

            try
            {
                HttpResponseMessage response = await client.GetAsync("https://coreteaching01.csit.rmit.edu.au/~e103884/wdt/services/customers/");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

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
            catch(HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");	
                Console.WriteLine("Message :{0} ",e.Message);
                
            }
            
        }

        

    }
}