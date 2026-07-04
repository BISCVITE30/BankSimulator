using System;
namespace Program
{
    class BankSimulator
    {
        static void ClearTerminal()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
        }

        static void CheckBalance(double user)
        {
            ClearTerminal();
            Console.WriteLine($"Your balance is {user}$");
            Menu(user);
        }

        static void Deposit(double user)
        {
            double amountDeposit;
            ClearTerminal();
            Console.Write("Enter deposit amount: $");
            amountDeposit = Convert.ToDouble(Console.ReadLine());
            user += amountDeposit;
            Console.WriteLine($"Successfully deposited {amountDeposit}$. Your new balance is {user}$");
            Menu(user);
        }

        static void Withdraw(double user)
        {
            double amountWithdraw;
            Console.Write("Enter your withdrawal: $");
                    amountWithdraw = Convert.ToDouble(Console.ReadLine());
                    if (amountWithdraw <= user)
                    {
                        ClearTerminal();
                        user -= amountWithdraw;

                        Console.WriteLine($"Successfully withdrew {amountWithdraw}$. Your current balance is {user}$");
                        Menu(user);
                    }
                    else
                    {

                        Console.WriteLine("Insufficient funds.");
                        Console.Write("You want return back? (Yes/No) ");
                        string turnBack = Console.ReadLine();
                        if (turnBack == "Yes")
                        {
                            ClearTerminal();
                            Menu(user);
                        }
                    }
        }
        static void Menu(double user)
        {
           
            int userChoise;

            string[] mainPage =
            [
                "1. Check the balance",
                "2. Make a deposit",
                "3. Withdraw",
                "4. Exit"
            ];

            Console.WriteLine("Welcome to our system!");
            foreach (string buttonOption in mainPage)
            {
                Console.WriteLine(buttonOption);
            }
            userChoise = Convert.ToInt32(Console.ReadLine());
            switch (userChoise)
            {
                case 1:
                    CheckBalance(user);
                    break;
                case 2:
                    Deposit(user);
                    break;
                case 3:
                    Withdraw(user);
                    break;
                case 4:
                    Console.WriteLine("Thanks you for using our system. Have a nice day!");
                    break;
            }
        }
        static bool IdentificationAge()
        {
            Console.Write("Are you 18 years old or older? (Yes/No) ");

            string userAge = Console.ReadLine();

            while (userAge != "Yes" && userAge != "No")
            {
                IdentificationAge();
                break;
            }

            if (userAge == "No")
            {

                Console.WriteLine("Sorry, you must be 18 or older to use this system");
                return false;
            }
            return true;
        }
        
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to our system.");
            bool isIdentified = IdentificationAge();
            if (!isIdentified)
            {
                return;
            }

            double user = 100;

            Menu(user);
        } 
    }
}