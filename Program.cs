using WebDevTechAss1.Models;

namespace WebDevTechAss1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            User user = new User();

            user.name = "Connor";

            //Main menu ui 

            Console.WriteLine("--- "+ user.name +" ---");
            Console.WriteLine("[1] Deposit");
            Console.WriteLine("[2] Withdraw");
            Console.WriteLine("[3] Transfer");
            Console.WriteLine("[4] My Statement");
            Console.WriteLine("[5] Logout");
            Console.WriteLine("[6] Exit");
            Console.WriteLine("\n");
            Console.WriteLine("Enter an option:");
        }
    }
}