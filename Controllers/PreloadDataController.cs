using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace WebDevTechAss1.Controllers
{
    public class PreloadDataController
    {
        static readonly HttpClient client = new HttpClient();

        public async Task PreloadData()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://coreteaching01.csit.rmit.edu.au/~e103884/wdt/services/customers/");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);

            }
           catch(HttpRequestException e)
           {
                Console.WriteLine("\nException Caught!");	
                Console.WriteLine("Message :{0} ",e.Message);
                
           }
        }
        
    }
}