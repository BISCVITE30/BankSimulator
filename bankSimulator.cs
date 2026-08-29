using System;
using System.Security.Cryptography.X509Certificates;
namespace Program
{

    class BankAccount
    {
        public string OwnerName{ get; set; }
        public double Balance{ get; private set; }
        // readonly int AccountNumber;
        // readonly DateTime CreatedAt;
        // static double InterestRate;

        public BankAccount(string OwnerName, double Balance)
        {
            this.OwnerName = OwnerName;
            this.Balance = Balance;
            this.CreatedAt = DateTime.Now;
        }
        public void CheckBalance()
        {
            BankSimulator.ClearTerminal();
            Console.WriteLine($"Your balance is ${this.Balance}");
        }

        public void Deposit()
        {
            double amountDeposit;
            BankSimulator.ClearTerminal();
            Console.Write("Enter deposit amount: $");
            amountDeposit = Convert.ToDouble(Console.ReadLine());
            this.Balance += amountDeposit;
            Console.WriteLine($"Successfully deposited ${amountDeposit}. Your new balance is ${this.Balance}");
        }

        public void Withdraw()
        {
            BankSimulator.ClearTerminal();
            double amountWithdraw;
            Console.Write("Enter your withdrawal: $");
            amountWithdraw = Convert.ToDouble(Console.ReadLine());
            if (amountWithdraw <= this.Balance)
            {
                BankSimulator.ClearTerminal();
                this.Balance -= amountWithdraw;
                Console.WriteLine($"Successfully withdrew ${amountWithdraw}. Your current balance is ${this.Balance}");
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
                Console.Write("You want return back? (Yes/No) ");
                string? turnBack = Console.ReadLine();
                if (turnBack == "Yes")
                {
                    BankSimulator.ClearTerminal();
                }
            }
        }
    }

    class BankSimulator
    {

        
        public static void ClearTerminal()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
        }

        static bool Menu(BankAccount account)
        {
            
            string[] mainPage =
            [
                "1. Check the balance",
                "2. Make a deposit",
                "3. Withdraw",
                "4. Exit"
            ];
            int convertedNumber;
            Console.WriteLine("Welcome to our system!");
            foreach (string buttonOption in mainPage)
            {
                Console.WriteLine(buttonOption);
            }
            string? userChoise = Console.ReadLine();
            if (int.TryParse(userChoise, out  convertedNumber)){
                switch (userChoise)
            {
                case "1":
                    account.CheckBalance();
                    break;
                case "2":
                    account.Deposit();
                    break;
                case "3":
                    account.Withdraw();
                    break;
                case "4":
                        return false;
            }
            }
            return true;
        }
        static bool IdentificationAge()
        {
            Console.Write("Are you 18 years old or older? (Yes/No) ");

            string? userAge = Console.ReadLine();

            while (userAge != "Yes" && userAge != "No")
            {
                ClearTerminal();
                Console.WriteLine("Try again");
                Console.Write("Are you 18 years old or older? (Yes/No) ");
                userAge = Console.ReadLine();
            }

            if (userAge == "No")
            {

                Console.WriteLine("Sorry, you must be 18 or older to use this system");
                return false;
            }
            else if (userAge == "Yes")
            {
                ClearTerminal();
                return true;
            }
            ClearTerminal();
            return false;
        }
        
        static void Main(string[] args)
        {
            bool isSystemOn = true;
            BankAccount[] accounts = { new BankAccount("Panfil", 100), new BankAccount("Sarah", 300) };
            Console.WriteLine("Welcome to our system.");
            bool isIdentified = IdentificationAge();
            if (!isIdentified)
            {
                return;
            }

            while (isSystemOn)
            {

            ClearTerminal();

                Console.WriteLine("Select an account:");
                int index = 0;
                foreach (BankAccount account in accounts)
                {
                    index++;
                    Console.WriteLine($"{index}. {account.OwnerName}");
                }
                Console.WriteLine($"{index + 1}. Exit System");
                index = 0;
                int accountSelected = Convert.ToInt32(Console.ReadLine());
            if(accountSelected == (accounts.Length + 1))
                {
                    ClearTerminal();
                    Console.WriteLine("Thank you for using our system.");
                    break;
                }
            bool isSessionActive = true;
            while (isSessionActive)
                {
                    ClearTerminal();
                isSessionActive = Menu(accounts[accountSelected - 1]);    
            }
            }
        } 
    }
}