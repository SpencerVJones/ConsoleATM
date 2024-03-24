using System;
using System.Collections.Generic;
using System.Linq;

namespace AtmMachine
{
    internal class Program
    {
        // Define User class to represent ATM users
        public class User
        {
            // Encapsulate fields and provide properties
            public string FirstName { get; private set; }
            public string LastName { get; private set; }
            public long CardNum { get; private set; }
            public int Pin { get; private set; }
            public double Balance { get; set; } 

            // Constructor to initialize user details
            public User(string firstName, string lastName, long cardNum, int pin, double balance)
            {
                FirstName = firstName;
                LastName = lastName;
                CardNum = cardNum;
                Pin = pin;
                Balance = balance;
            }
        }

        // Main method
        public static void Main(string[] args)
        {
            // Initialize list of users with test data
            List<User> users = new List<User>
            {
                new User("Spencer", "Jones", 1234567890123456, 1234, 10234.43),
                new User("John", "Smith", 7890123456789012, 2345, 4534.32),
                new User("Alice", "Cooper", 3456789012345678, 6789, 832974.54),
                new User("Alex", "Hicks", 7890123456789012, 2340, 32.23),
                new User("Drake", "Wilson", 3456789012345678, 6780, 423.53)
            };

            // Authenticate user and retrieve current user object
            User currentUser = AuthenticateUser(users);
            if (currentUser == null)
            {
                Console.WriteLine("Authentication failed. Exiting...");
                return;
            }

            // Welcome message for the authenticated user
            Console.WriteLine($"Welcome, {currentUser.FirstName} {currentUser.LastName}");

            int option;
            // Main ATM operations loop
            do
            {
                PrintOptions();
                option = GetOption();

                switch (option)
                {
                    case 1:
                        DepositMoney(currentUser);
                        break;
                    case 2:
                        WithdrawMoney(currentUser);
                        break;
                    case 3:
                        ShowBalance(currentUser);
                        break;
                    case 4:
                        Console.WriteLine("Thank you for your business.");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            } while (option != 4);
        }

        // Authentication logic to verify user credentials
        private static User AuthenticateUser(List<User> users)
        {
            Console.WriteLine("Welcome to Spencer's ATM");
            Console.WriteLine("Please enter your card number:");

            long userCard;
            int userPin;
            User currentUser = null;

            while (true)
            {
                if (!long.TryParse(Console.ReadLine(), out userCard))
                {
                    Console.WriteLine("Invalid card number format. Please try again.");
                    continue;
                }

                currentUser = users.FirstOrDefault(a => a.CardNum == userCard);
                if (currentUser != null)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Card not recognized, please try a different card");
                }
            }

            Console.WriteLine("Please enter your PIN:");
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out userPin))
                {
                    Console.WriteLine("Invalid PIN format. Please enter a valid PIN:");
                    continue;
                }

                if (currentUser.Pin == userPin)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect PIN, please re-enter your PIN:");
                }
            }

            return currentUser;
        }

        // Function to display ATM options
        static void PrintOptions()
        {
            Console.WriteLine("Please choose from one of the following options:");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Show Balance");
            Console.WriteLine("4. Exit");
        }

        // Function to get user input for selected option
        static int GetOption()
        {
            int option;
            while (!int.TryParse(Console.ReadLine(), out option) || option < 1 || option > 4)
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 4:");
            }
            return option;
        }

        // Function to handle deposit operation
        static void DepositMoney(User currentUser)
        {
            Console.WriteLine("How much money are you depositing?");
            if (!double.TryParse(Console.ReadLine(), out double deposit) || deposit <= 0)
            {
                Console.WriteLine("Invalid amount. Please enter a valid positive number.");
                return;
            }

            currentUser.Balance += deposit;
            Console.WriteLine($"Successfully deposited {deposit:C}. Your new balance is: {currentUser.Balance:C}");
        }

        // Function to handle withdrawal operation
        static void WithdrawMoney(User currentUser)
        {
            Console.WriteLine("How much money would you like to withdraw?");
            if (!double.TryParse(Console.ReadLine(), out double withdrawal) || withdrawal <= 0)
            {
                Console.WriteLine("Invalid amount. Please enter a valid positive number.");
                return;
            }

            if (withdrawal > currentUser.Balance)
            {
                Console.WriteLine("Insufficient balance.");
            }
            else
            {
                currentUser.Balance -= withdrawal;
                Console.WriteLine($"Successfully withdrew {withdrawal:C}. Your new balance is: {currentUser.Balance:C}");
            }
        }

        // Function to display current balance
        static void ShowBalance(User currentUser)
        {
            Console.WriteLine($"Your current balance is: {currentUser.Balance:C}");
        }
    }
}