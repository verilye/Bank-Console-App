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

            db.InsertTransaction(deposit, type, account, 0);
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

            Transaction withdraw = new Transaction(Convert.ToDecimal(amount), null, time.ToString());


            int account = db.GetAccountNumber(customerID, 'S');

            db.InsertTransaction(withdraw, type, account, 0);

            int a = Int32.Parse(amount);
            int negatize = a*2;
            db.UpdateAccountBalance(account, a-negatize);
        }

        public  void Transfer()
        {

        }

        public void MyStatement()
        {

        }

        
    }
}