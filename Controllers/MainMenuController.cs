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


            //Use PBKDF2.Verify(passwordHash, password) to verify password is correct
            
            bool isPasswordValid = PBKDF2.Verify(hash,password);


            return isPasswordValid;

        }

        public async Task Deposit()
        {

        }

        public async Task Withdraw()
        {

        }

        public async Task Transfer()
        {

        }

        public async Task MyStatement()
        {

        }

        
    }
}