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
            LoginUI();
    
        }
    
    
        static async Task LoginUI()
        {

            //First the user needs to login
            //If successful they will be redirected to the main menu

            Boolean loggingIn = true;

            string id = null;

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

                Boolean attempt = menu.Login(username, password);

                if(attempt)
                {
                    id = username;
                    loggingIn = false;
                }
                
                Console.Clear();

                Console.WriteLine("Username or password incorrect, please try again");
                
            }

            Console.Clear();
            // Check Database for user entries and if entries
            // exist, do not populate using the webservice

            if(db.checkDb()){
            
                await customer.PreloadData();

            }

            
            int customerID = db.GetCustomerID(id);

            await MenuUI(customerID);

            

        }

        static async Task MenuUI(int customerID)
        {
           // Then display the menu loop and allow users to selct 
           // options from it 

           // Refer to MainMenuController for implementaions of all these
           // menu functions
            Boolean morbing = true;
            while(morbing)
            {
                try{

                    Console.WriteLine("--- MCBA Banking Application ---");
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
                        
                            menu.Deposit(customerID);
                            break;
                        
                        case "2":
                        
                            menu.Withdraw(customerID);
                            break;

                        case "3":
                            
                            menu.Transfer(customerID);
                            break;

                        case "4":
                            
                            menu.MyStatement();
                            break;

                        case "5":
                            Console.Clear();
                            Console.WriteLine("Logging out...");
                            morbing = false;
                            LoginUI();
                            break;

                        case "6":
                            Console.Clear();
                            Console.WriteLine("Exiting program...");
                            Environment.Exit(0);
                            break;

                    }
                }
                catch(Exception)
                {
                    Console.WriteLine("Invalid input");
                }
            }
        }
    }
}