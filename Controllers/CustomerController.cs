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

namespace WebDevTechAss1.Controllers
{
    public class CustomerController
    {
    
        static readonly HttpClient client = new HttpClient();
        static readonly DatabaseController db = new DatabaseController();

        //Pull users from webservice and add them to database if no customers are present

        public async Task PreloadData()
        {

            try
            {
                HttpResponseMessage response = await client.GetAsync("https://coreteaching01.csit.rmit.edu.au/~e103884/wdt/services/customers/");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                var customers = JsonConvert.DeserializeObject<List<Customer>>(responseBody);

                foreach(var customer in customers)
                {

                    db.InsertCustomer(customer);
                    db.InsertLogin(customer.Login, customer.CustomerID);

                    for(int i = 0; i<customer.Accounts.Length;i++)
                    {
                        db.InsertAccount(customer.Accounts[i]);

                        for(int j = 0; j<customer.Accounts[i].Transactions.Length;j++)
                        {
                            db.InsertTransaction(customer.Accounts[i].Transactions[j], 'D', customer.Accounts[i].AccountNumber,0);
                            db.UpdateAccountBalance(customer.Accounts[i].AccountNumber, customer.Accounts[i].Transactions[j].Amount);
                        }
                    }            
                }

                Console.WriteLine("Data preloaded to database!");
        

            }
            catch(HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");	
                Console.WriteLine("Message :{0} ",e.Message);
                
            }
            
        }

        

    }
}