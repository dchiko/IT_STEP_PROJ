using System;
using System.IO;

namespace ATMConsoleApp
{
    class Program
    {
        static string clientInfoFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ClientInfo.txt");
        static string currentUsername = null;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Welcome to the ATM Console Application!");
                if (currentUsername == null)
                {
                    Console.WriteLine("1. Create New Account");
                    Console.WriteLine("2. Access Existing Account");
                    Console.WriteLine("3. Exit");
                }
                else
                {
                    Console.WriteLine("1. Withdraw");
                    Console.WriteLine("2. Deposit");
                    Console.WriteLine("3. Show Balance");
                    Console.WriteLine("4. Exit");
                }
                Console.Write("Choose an option: ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid choice. Please enter a number.");
                    continue;
                }

                if (currentUsername == null)
                {
                    switch (choice)
                    {
                        case 1:
                            CreateNewAccount();
                            break;
                        case 2:
                            AccessExistingAccount();
                            break;
                        case 3:
                            Exit();
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please select a valid option.");
                            break;
                    }
                }
                /*else
                {
                    switch (choice)
                    {
                        case 1:
                            Withdraw();
                            break;
                        case 2:
                            Deposit();
                            break;
                        case 3:
                            ShowBalance();
                            break;
                        case 4:
                            Exit();
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please select a valid option.");
                            break;
                    }
                }*/
            }
        }

        static void CreateNewAccount()
        {
            Console.WriteLine("\nCreating New Account...");
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();

            if (CheckUsernameExists(username))
            {
                Console.WriteLine("Username already exists. Please choose a different username.");
                return;
            }

            Console.Write("Enter PIN (4 digits): ");
            int pin;
            if (!int.TryParse(Console.ReadLine(), out pin) || pin < 1000 || pin > 9999)
            {
                Console.WriteLine("PIN must be a 4-digit integer.");
                return;
            }

            Console.Write("Confirm PIN: ");
            int confirmPin;
            if (!int.TryParse(Console.ReadLine(), out confirmPin))
            {
                Console.WriteLine("Invalid PIN format.");
                return;
            }

            if (pin != confirmPin)
            {
                Console.WriteLine("PINs do not match. Account creation failed.");
                return;
            }

            using (StreamWriter sw = File.AppendText(clientInfoFilePath))
            {
                sw.WriteLine(username);
                sw.WriteLine(pin);
                sw.WriteLine("0");
            }

            Console.WriteLine("Account created successfully!");
        }

        static void AccessExistingAccount()
        {
            Console.WriteLine("\nAccessing Existing Account...");
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();

            string[] clientInfo = GetClientInfo(username);

            if (clientInfo == null)
            {
                Console.WriteLine("Username does not exist.");
                return;
            }

            Console.Write("Enter PIN: ");
            int pin;
            if (!int.TryParse(Console.ReadLine(), out pin))
            {
                Console.WriteLine("Invalid PIN format.");
                return;
            }

            if (pin != int.Parse(clientInfo[1]))
            {
                Console.WriteLine("Invalid PIN.");
                return;
            }

            Console.WriteLine($"Logged in as {username}.");
            currentUsername = username;

            while (true)
            {
                Console.WriteLine("\nAccount Options:");
                Console.WriteLine("1. Withdraw");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Show Balance");
                Console.WriteLine("4. Exit");

                Console.Write("Choose an option: ");
                int accountOption;
                if (!int.TryParse(Console.ReadLine(), out accountOption))
                {
                    Console.WriteLine("Invalid choice. Please enter a number.");
                    continue;
                }

                switch (accountOption)
                {
                    case 1:
                        Withdraw();
                        break;
                    case 2:
                        Deposit();
                        break;
                    case 3:
                        ShowBalance();
                        break;
                    case 4:
                        Console.WriteLine("Exiting to Main Menu...");
                        currentUsername = null;
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
            }
        }

        static bool CheckUsernameExists(string username)
        {
            if (!File.Exists(clientInfoFilePath))
                return false;

            string[] lines = File.ReadAllLines(clientInfoFilePath);
            for (int i = 0; i < lines.Length; i += 3)
            {
                if (lines[i] == username)
                    return true;
            }
            return false;
        }

        static string[] GetClientInfo(string username)
        {
            if (!File.Exists(clientInfoFilePath))
                return null;

            string[] lines = File.ReadAllLines(clientInfoFilePath);
            for (int i = 0; i < lines.Length; i += 3)
            {
                if (lines[i] == username)
                    return new string[] { lines[i], lines[i + 1], lines[i + 2] };
            }
            return null;
        }

        static void Withdraw()
        {
            Console.Write("Enter amount to withdraw: ");
            decimal amount;
            if (!decimal.TryParse(Console.ReadLine(), out amount))
            {
                Console.WriteLine("Invalid amount.");
                return;
            }

            string[] clientInfo = GetClientInfo(currentUsername);
            decimal balance = decimal.Parse(clientInfo[2]);

            if (amount > balance)
            {
                Console.WriteLine("Insufficient balance.");
                return;
            }

            balance -= amount;
            UpdateBalance(balance);
            Console.WriteLine($"Withdrawal successful. New balance: {balance}");
        }

        static void Deposit()
        {
            Console.Write("Enter amount to deposit: ");
            decimal amount;
            if (!decimal.TryParse(Console.ReadLine(), out amount))
            {
                Console.WriteLine("Invalid amount.");
                return;
            }

            string[] clientInfo = GetClientInfo(currentUsername);
            decimal balance = decimal.Parse(clientInfo[2]);

            balance += amount;
            UpdateBalance(balance);
            Console.WriteLine($"Deposit successful. New balance: {balance}");
        }

        static void ShowBalance()
        {
            string[] clientInfo = GetClientInfo(currentUsername);
            decimal balance = decimal.Parse(clientInfo[2]);
            Console.WriteLine($"Current Balance: {balance}");
        }

        static void Exit()
        {
            Console.WriteLine("Exiting the program. Goodbye!");
            Environment.Exit(0);
        }

        static void UpdateBalance(decimal newBalance)
        {
            string[] lines = File.ReadAllLines(clientInfoFilePath);
            for (int i = 0; i < lines.Length; i += 3)
            {
                if (lines[i] == currentUsername)
                {
                    lines[i + 2] = newBalance.ToString();
                    break;
                }
            }
            File.WriteAllLines(clientInfoFilePath, lines);
        }
    }
}
