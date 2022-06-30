using WebDevTechAss1.Controllers;
using WebDevTechAss1.Models;

namespace WebDevTechAss1
{
    public class Program
    {

        
        static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {

            // For testing purposes,
            // spitting out info from web service here

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
               
            // User user = new User();

            // user.name = "Connor";

            // //Main menu ui 

            // Console.WriteLine("--- "+ user.name +" ---");
            // Console.WriteLine("[1] Deposit");
            // Console.WriteLine("[2] Withdraw");
            // Console.WriteLine("[3] Transfer");
            // Console.WriteLine("[4] My Statement");
            // Console.WriteLine("[5] Logout");
            // Console.WriteLine("[6] Exit");
            // Console.WriteLine("\n");
            // Console.WriteLine("Enter an option:");
        }
    }
}