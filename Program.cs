using System.Configuration;
using WebDevTechAss1.Controllers;
using WebDevTechAss1.Models;


namespace WebDevTechAss1
{
    public class Program
    {
        static readonly CustomerController customer = new CustomerController();
        static readonly DatabaseController db = new DatabaseController();
        static readonly MainMenuController menu = new MainMenuController();
        

        static async Task Main(string[] args)
        {
            
            Console.Clear();

            //First the user needs to login

            Boolean loggingIn = true;

            while(loggingIn)
            {

                Console.Write("Enter Login ID:" );

                string username = null;
                while(true)
                {
                    var u = System.Console.ReadKey();
                    if(u.Key == ConsoleKey.Enter)
                        break;

                    username += u.KeyChar;

                }
                Console.Clear();

                Console.WriteLine("Enter Login ID:" + username );

                string password = null;
                Console.Write("Enter Password:");
                while(true)
                {
                    var p = System.Console.ReadKey(true);
                    if(p.Key == ConsoleKey.Enter)
                        break;

                    password += p.KeyChar;
                    Console.Write("*");

                }

                Console.WriteLine("\n" + username + " " + password);
                

                Boolean attempt = menu.Login(username, password);

                if(attempt)
                {
                    loggingIn = false;
                }
                
                Console.Clear();

                Console.WriteLine("Username or password incorrect, please try again");
                
            }


            // Check Database for user entries and if entries
            // exist, do not populate using the webservice

            if(db.checkDb()){
               
                await customer.PreloadData();

            }


           // Then display the menu loop and allow users to selct 
           // options from it 

            while(true)
            {
                Console.Clear();
                
                 //Console.WriteLine("--- "+ user.name +" ---");
                Console.WriteLine("[1] Deposit");
                Console.WriteLine("[2] Withdraw");
                Console.WriteLine("[3] Transfer");
                Console.WriteLine("[4] My Statement");
                Console.WriteLine("[5] Logout");
                Console.WriteLine("[6] Exit");
                Console.WriteLine("\n");
                Console.WriteLine("Enter an option:");

                string input = Console.ReadLine();

                switch(input)
                {
                    case "1":
                        Console.Clear();
                        await menu.Deposit();
                        break;
                    
                    case "2":
                        Console.Clear();
                        await menu.Withdraw();
                        break;

                    case "3":
                        Console.Clear();
                        await menu.Transfer();
                        break;

                    case "4":
                        Console.Clear();
                        await menu.MyStatement();
                        break;

                    case "5":
                        Console.Clear();
                        Console.WriteLine("Logging out...");
                        await menu.Logout();
                        break;

                    case "6":
                        Console.Clear();
                        Console.WriteLine("Exiting program...");
                        Environment.Exit(0);
                        break;

                }


            }
    
        }
    }
}