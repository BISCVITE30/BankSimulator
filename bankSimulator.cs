using System;
using System.Diagnostics.Metrics;
using System.Security.Cryptography.X509Certificates;
namespace Program
{

    class BankAccount
    {
        public string OwnerName{ get; set; }
        public double Balance{ get; private set; }
        public string Pin{ get; private set; }
        // readonly int AccountNumber;
        readonly DateTime CreatedAt;
        // static double InterestRate;

        public BankAccount(string OwnerName, double Balance, string Pin)
        {
            this.OwnerName = OwnerName;
            this.Balance = Balance;
            this.Pin = Pin;
            this.CreatedAt = DateTime.Now;
        }
        public void CheckBalance()
        {
            BankSimulator.ClearTerminal();
            Console.WriteLine($"Your balance is ${this.Balance}");

            BankSimulator.PressF();
        }

        public void Deposit()
        {
            BankSimulator.ClearTerminal();
            Console.Write("Enter deposit amount: $");
            if(double.TryParse(Console.ReadLine(), out double amountDeposit) && amountDeposit > 0)
            {   
                this.Balance += amountDeposit;
                Console.WriteLine($"Successfully deposited ${amountDeposit}. Your new balance is ${this.Balance}");
            }
            else
            {
                Console.WriteLine("Invalid deposit amount");
            };
            BankSimulator.PressF();
        }

        public void Withdraw()
        {
            BankSimulator.ClearTerminal();
            Console.Write("Enter your withdrawal: $");
            if (double.TryParse(Console.ReadLine(), out double amountWithdraw) && amountWithdraw <= this.Balance)
            {
                BankSimulator.ClearTerminal();
                this.Balance -= amountWithdraw;
                Console.WriteLine($"Successfully withdrew ${amountWithdraw}. Your current balance is ${this.Balance}");

                BankSimulator.PressF();
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
                BankSimulator.PressF();
            }
        }

        public void Transfer(BankAccount targetAccount)
        {
            BankSimulator.ClearTerminal();
            Console.Write("Enter tranfer amount: $");

            if(double.TryParse(Console.ReadLine(), out double amountTransfer) && amountTransfer > 0)
            {
                if(amountTransfer <= this.Balance)
                {
                    this.Balance -= amountTransfer;
                    targetAccount.Balance += amountTransfer;
                    BankSimulator.ClearTerminal();
                    Console.WriteLine($"Successfully transfered {amountTransfer}$ to {targetAccount.OwnerName}");
                    Console.WriteLine($"Your new Balance is: {this.Balance}$");

                }
                else
                {
                    Console.WriteLine("Insufficient funds.");
                };

            } 
            else
            {
                Console.WriteLine("Invalid amount");    
            }

            BankSimulator.PressF();
        }
    }

    class BankSimulator
    {

        public static void PressF()
        {
            Console.WriteLine("\nPress Enter to continue");
            Console.ReadLine();
        }

        public static void EnterPass( BankAccount account )
        {
            int attempts = 0;
            while ( attempts < 3)
            {
            Console.Write("Enter your password: ");
            string? correctPin = Console.ReadLine();
                
            if(correctPin == account.Pin)
            {
                return;
            }
            
            attempts++;
            ClearTerminal();
            Console.WriteLine($"Incorrect password. Attempts remaining: {3 - attempts}");
            }

            Console.WriteLine("\nToo many failed attempts. Security lock engaged.");
            PressF();
            Environment.Exit(0);
        }
        
        public static void ClearTerminal()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
        }

        static bool Menu(BankAccount account, BankAccount[] allAccounts)
        {
            string[] mainPage =
            [
                "1. Check the balance",
                "2. Make a deposit",
                "3. Withdraw",
                "4. Transfer Money",
                "5. Exit"
            ];
            Console.WriteLine("Welcome to our system!");
            foreach (string buttonOption in mainPage)
            {
                Console.WriteLine(buttonOption);
            }
            if (int.TryParse(Console.ReadLine() ?? "", out int userChoice))
            {
                switch (userChoice)
            {
                case 1:
                    account.CheckBalance();
                    break;
                case 2:
                    account.Deposit();
                    break;
                case 3:
                    account.Withdraw();
                    break;
                case 4:
                    Console.WriteLine("Choose an account to transfer to: ");
                    for (int i = 0; i < allAccounts.Length; i++)
                    {
                        if(allAccounts[i] != account)
                        {
                            Console.WriteLine($"{i + 1} {allAccounts[i].OwnerName}");
                        }
                    }
                    Console.Write("Choise: ");
                    if(int.TryParse(Console.ReadLine(), out int targetIndex) && targetIndex > 0 && targetIndex <= allAccounts.Length && allAccounts[targetIndex - 1] != account)
                    {
                        account.Transfer(allAccounts[targetIndex - 1]);
                    } 
                    else
                    {
                        Console.WriteLine("Invalid selection");
                    }
                    break;
                case 5:
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
            BankAccount[] accounts = { new BankAccount("Panfil", 100, "0001"), new BankAccount("Sarah", 300, "0002") };
            Console.WriteLine("Welcome to our system.");
            bool isSystemOn = IdentificationAge();

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
                if(int.TryParse(Console.ReadLine(), out int accountSelected) && accountSelected > 0 && accountSelected <= (accounts.Length + 1))
                {
                    
                    if(accountSelected == (accounts.Length + 1))
                    {
                        ClearTerminal();
                        Console.WriteLine("Thank you for using our system.");
                        break;
                    }
                    bool isSessionActive = true;
                    ClearTerminal();
                    EnterPass(accounts[accountSelected - 1]);
                    while (isSessionActive)
                    {
                        ClearTerminal();
                        isSessionActive = Menu(accounts[accountSelected - 1], accounts);    
                    }
                } 
                else
                {
                    Console.WriteLine("Invalid choice. Please pick a valid option number");
                    PressF();
                }
            }
        } 
    }
}