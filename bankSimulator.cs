using System;
namespace Program
{
    class BankSimulator
    {

        static double user = 100;
        static void ClearTerminal()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
        }

        static void CheckBalance()
        {
            ClearTerminal();
            Console.WriteLine($"Your balance is ${user}");
        }

        static void Deposit()
        {
            double amountDeposit;
            ClearTerminal();
            Console.Write("Enter deposit amount: $");
            amountDeposit = Convert.ToDouble(Console.ReadLine());
            user += amountDeposit;
            Console.WriteLine($"Successfully deposited ${amountDeposit}. Your new balance is ${user}");
        }

        static void Withdraw()
        {
            ClearTerminal();
            double amountWithdraw;
            Console.Write("Enter your withdrawal: $");
            amountWithdraw = Convert.ToDouble(Console.ReadLine());
            if (amountWithdraw <= user)
            {
                ClearTerminal();
                user -= amountWithdraw;
                Console.WriteLine($"Successfully withdrew ${amountWithdraw}. Your current balance is ${user}");
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
                Console.Write("You want return back? (Yes/No) ");
                string? turnBack = Console.ReadLine();
                if (turnBack == "Yes")
                {
                    ClearTerminal();
                }
            }
        }
        static bool Menu()
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
                    CheckBalance();
                    break;
                case "2":
                    Deposit();
                    break;
                case "3":
                    Withdraw();
                    break;
                case "4":
                    ClearTerminal();
                    Console.WriteLine("Thanks you for using our system. Have a nice day!");
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

            Console.WriteLine("Welcome to our system.");
            bool isActive = true;
            bool isIdentified = IdentificationAge();
            if (!isIdentified)
            {
                return;
            }

            while (isActive)
            {
                isActive = Menu();    
            }
        } 
    }
}