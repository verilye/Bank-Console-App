using WebDevTechAss1.Models;
using SimpleHashing;

namespace WebDevTechAss1.Controllers
{
    public class MainMenuController
    {

        static readonly DatabaseController db = new DatabaseController();

        public Boolean Login(string username, string password)
        {            

                //Query database for username and resulting password hash


                string hash = db.VerifyLogin(username);

                if(hash == null)
                {
                    return false;
                }


                //Use PBKDF2.Verify(passwordHash, password) to verify password is correct
                
                bool isPasswordValid = PBKDF2.Verify(hash,password);


                return isPasswordValid;
           
        }


        //Deposit money to an account

        public void Deposit(int customerID)
        {
            Console.Clear();
            Console.WriteLine("--- Deposit Money ---");

            Console.WriteLine("Enter the amount you would like to deposit:");

            string amount = Console.ReadLine();

            Console.WriteLine("Deposit to your Credit or Savings account? (Enter 'C' or 'S')");

            char type = (char)Console.Read();


            DateTime time = DateTime.Now;

            Transaction deposit = new Transaction(Convert.ToDecimal(amount), null, time.ToString());


            int account = db.GetAccountNumber(customerID, 'S');

            db.InsertTransaction(deposit, 'D', account, 0);
            db.UpdateAccountBalance(account, Int32.Parse(amount));

        
        }

        public void Withdraw(int customerID)
        {
            Console.Clear();
            Console.WriteLine("--- Withdraw Money ---");

            Console.WriteLine("Enter the amount you would like to withdraw:");

            string amount = Console.ReadLine();

            Console.WriteLine("Withdraw from your Credit or Savings account? (Enter 'C' or 'S')");

            char type = (char)Console.Read();


            DateTime time = DateTime.Now;

            int account = db.GetAccountNumber(customerID, type);

            int a = Int32.Parse(amount);
            int negatize = a*2;
            db.UpdateAccountBalance(account, a-negatize);

            Transaction withdraw = new Transaction(Convert.ToDecimal(a), null, time.ToString());

            db.InsertTransaction(withdraw, 'W', account, 0);

           
        }



        //Handles transfers to any other account in the system

        public  void Transfer(int customerID)
        {


            try
            {
                //Gather information

                Console.Clear();
                Console.WriteLine("--- Transfer Money ---");

                Console.WriteLine("Would you like to transfer money from your Checking or Savings account ('C' or 'S')");

                string acc = Console.ReadLine();

                Console.WriteLine("Enter the account number you would like to transfer to");

                string dest = Console.ReadLine();

                Console.WriteLine("Enter the amount you would like to transfer:");

                string amount = Console.ReadLine();

                Console.WriteLine("Add comment:");

                string comment = Console.ReadLine();


                DateTime time = DateTime.Now;

                Transaction transfer = new Transaction(Convert.ToDecimal(amount), comment, time.ToString());

                //Check to see if user has enough balance for transaction

                //Insert Transaction for both accounts;

                //Change balance for destination account

                //Change balance for account being debited


                int account = db.GetAccountNumber(customerID, 'S');

                int a = Int32.Parse(amount);
                int negatize = a*2;

                db.UpdateAccountBalance(account, a-negatize);
                db.UpdateAccountBalance(Int32.Parse(dest), a);

                db.InsertTransaction(transfer, 'T', account,Int32.Parse(dest));
                db.InsertTransaction(transfer, 'T', Int32.Parse(dest),  Int32.Parse(dest));

            }
             catch (Exception e)
            {
                Console.WriteLine("Invalid input");
            }
        }

        public void MyStatement()
        {

        }

        
    }
}