using System.Configuration;
using WebDevTechAss1.Controllers;
using WebDevTechAss1.Models;


namespace WebDevTechAss1
{
    public class Program
    {
        static readonly CustomerController customer = new CustomerController();

        static async Task Main(string[] args)
        {

            // Check Database for user entries and if entries
            // exist, do not populate using the webservice

            // if(customer.checkDb()){

                // Preload user data if entries not found and load into database
                await customer.PreloadData();

            // }


           

            


            //Menu

            // while(true)
            // {

            //     // //Main menu ui 

            //     //Console.WriteLine("--- "+ user.name +" ---");
            //     Console.WriteLine("[1] Deposit");
            //     Console.WriteLine("[2] Withdraw");
            //     Console.WriteLine("[3] Transfer");
            //     Console.WriteLine("[4] My Statement");
            //     Console.WriteLine("[5] Logout");
            //     Console.WriteLine("[6] Exit");
            //     Console.WriteLine("\n");
            //     Console.WriteLine("Enter an option:");

            // }
    
        }
    }
}