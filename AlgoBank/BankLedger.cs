using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AlgoBank
{
    class BankLedger
    {
        private static List<Customer> customers = new List<Customer>();
        private static List<Account> allAccounts = new List<Account>();
        private static List<Transaction> allTransactions = new List<Transaction>();
        private static double totalAmountInBank = 0.0;
        private static int totalCustomer = 0;
        private static int totalSavingsAccount = 0;
        private static int totalCurrentAccount = 0;
        private static int totalDomiciliaryAccount = 0;
        private static double uSDToNaira = 362.5;
        private static double eURToNaira = 403.14;
        private static double gBPToNaira = 473.5;

        internal static List<Customer> Customers { get => customers; }
        internal static List<Account> AllAccounts { get => allAccounts; }
        internal static List<Transaction> AllTransactions { get => allTransactions; }
        public static double TotalAmountInBank { get => totalAmountInBank; set => totalAmountInBank = value; }
        public static int TotalCustomer { get => totalCustomer; set => totalCustomer = value; }
        public static int TotalSavingsAccount { get => totalSavingsAccount; set => totalSavingsAccount = value; }
        public static int TotalCurrentAccount { get => totalCurrentAccount; set => totalCurrentAccount = value; }
        public static int TotalDomiciliaryAccount { get => totalDomiciliaryAccount; set => totalDomiciliaryAccount = value; }
        public static double USDToNaira { get => uSDToNaira; }
        public static double EURToNaira { get => eURToNaira; }
        public static double GBPToNaira { get => gBPToNaira; }

        public static bool RegisterCustomer()
        {
            bool IsValid = false;
            bool IsAdmin = Customers.Count == 0 ? true : false;
            string name = "";
            string email = "";
            string password = "";
            do
            {
                bool IsValidName = false;
                do
                {
                    Console.Write("Enter First name and Last name: ");
                    name = Console.ReadLine();
                    IsValidName = Regex.IsMatch(name, @"^[A-Za-z\s.\'\-]+$", RegexOptions.IgnoreCase);
                    if (!IsValidName)
                    {
                        //@"^[\.'\-]+$"
                        Console.WriteLine("\nPlease enter a valid name\n");
                    }
                } while (!IsValidName);
                
                bool IsValidEmail = false;
                do
                {
                    Console.Write("Enter email address: ");
                    email = Console.ReadLine();
                    IsValidEmail = Regex.IsMatch(email, @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                                        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                                                        RegexOptions.IgnoreCase);
                    if (!IsValidEmail)
                    {
                        Console.WriteLine("\nPlease enter a valid email address\n");
                    }
                    else if (Customers.Count != 0)
                    {
                        foreach (Customer customer in BankLedger.Customers)
                        {
                            if (customer.Email == email)
                            {
                                IsValidEmail = false;
                                Console.WriteLine("\nEmail already in use by another customer\nTry another email or try to login\n");
                                int option = 0;
                                do
                                {
                                    Console.WriteLine("\nEnter 1 to try another email\nEnter 2 to exit registration process");
                                    string UserInput = Console.ReadLine();
                                    int SelectedOption;
                                    bool IsValidInput = int.TryParse(UserInput, out SelectedOption);

                                    if (IsValidInput && (SelectedOption == 1 || SelectedOption == 2))
                                    {
                                        option = SelectedOption;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nInvalid option, Please select valid option\n");
                                    }
                                } while (option == 0);

                                if(option == 2)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                } while (!IsValidEmail);
                
                bool IsValidPassword = false;
                do
                {
                    Console.WriteLine("\nNote:\n1. Password should be minimun of 6 and maximum of 32 characters\n2. Password Can contain alphabets, number and symbols\n");
                    Console.Write("Enter your desired password: ");
                    password = "";
                    do
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        // Backspace Should Not Work
                        if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                        {
                            password += key.KeyChar;
                            Random Rand = new Random();
                            int number = Rand.Next(1, 3);
                            string Asterisks = "".PadLeft(number, 'X').Replace('X', '*');
                            Console.Write($"{Asterisks}");
                        }
                        else
                        {
                            if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                            {
                                password = password.Substring(0, (password.Length - 1));
                                Console.Write("\b \b");
                            }
                            else if (key.Key == ConsoleKey.Enter)
                            {
                                Console.WriteLine();
                                break;
                            }
                        }
                    } while (true);
                    bool PasswordValid = 6 <= password.Length && password.Length <= 36;
                    if (PasswordValid)
                    {
                        Console.Write("Confirm Password: ");
                        string ConfirmPassword = "";
                        do
                        {
                            ConsoleKeyInfo key = Console.ReadKey(true);
                            // Backspace Should Not Work
                            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                            {
                                ConfirmPassword += key.KeyChar;
                                Random Rand = new Random();
                                int number = Rand.Next(1, 3);
                                string Asterisks = "".PadLeft(number, 'X').Replace('X', '*');
                                Console.Write($"{Asterisks}");
                            }
                            else
                            {
                                if (key.Key == ConsoleKey.Backspace && ConfirmPassword.Length > 0)
                                {
                                    ConfirmPassword = ConfirmPassword.Substring(0, (ConfirmPassword.Length - 1));
                                    Console.Write("\b \b");
                                }
                                else if (key.Key == ConsoleKey.Enter)
                                {
                                    Console.WriteLine();
                                    break;
                                }
                            }
                        } while (true);
                        IsValidPassword = password == ConfirmPassword;
                        if (!IsValidPassword)
                        {
                            Console.WriteLine("\nPassword does not match, try again\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nPlease enter a valid password\n");

                    }
                } while (!IsValidPassword);

                IsValid = IsValidEmail && IsValidName && IsValidPassword;
            } while (!IsValid);

            Customer NewCustomer = new Customer(name, email, password, IsAdmin);
            BankLedger.Customers.Add(NewCustomer);
            BankLedger.TotalCustomer++;
            return true;
        }
        public static Customer AuthenticateCustomer()
        {
            bool IsRetry = true;
            string email = "";
            string password = "";
            Customer LoggedUser = null;
            do
            {
                bool IsValidEmail = false;
                do
                {
                    Console.Write("Enter your email address: ");
                    email = Console.ReadLine();
                    IsValidEmail = Regex.IsMatch(email, @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                                        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                                                        RegexOptions.IgnoreCase);
                    if (!IsValidEmail)
                    {
                        Console.WriteLine("\nPlease enter a valid email address\n");
                    }
                } while (!IsValidEmail);
                
                Console.Write("Enter your account password: ");
                password = "";
                do
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    // Backspace Should Not Work
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        password += key.KeyChar;
                        Random Rand = new Random();
                        int number = Rand.Next(1, 3);
                        string Asterisks = "".PadLeft(number, 'X').Replace('X', '*');
                        Console.Write($"{Asterisks}");
                    }
                    else
                    {
                        if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                        {
                            password = password.Substring(0, (password.Length - 1));
                            Console.Write("\b \b");
                        }
                        else if (key.Key == ConsoleKey.Enter)
                        {
                            break;
                        }
                    }
                } while (true);
                foreach (Customer customer in BankLedger.Customers)
                {
                    if (customer.Email == email && customer.Password == password)
                    {
                        LoggedUser = customer;
                        return LoggedUser;
                    }
                }
                int option = 0;
                Console.WriteLine("\nEmail and/or password is Incorrect\n");
                do
                {
                    Console.WriteLine("\nEnter 1 to try again\nEnter 2 to exit");
                    string UserInput = Console.ReadLine();
                    int SelectedOption;
                    bool IsValidInput = int.TryParse(UserInput, out SelectedOption);
                    
                    if (IsValidInput && (SelectedOption == 1 || SelectedOption == 2))
                    {
                        option = SelectedOption;
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid option, Please select valid option\n");
                    }
                } while (option == 0);

                IsRetry = option == 1 ? true : false;
            } while (IsRetry);

            return LoggedUser;
        }
        public static Customer GetCustomerByID(int id)
        {
            foreach (Customer customer in Customers)
            {
                if (customer.Id == id)
                {
                    return customer;
                }
            }
            return null;
        }
        public static List<Customer> GetAllCustomers(Customer customer)
        {
            if (customer.IsAdmin)
            {
                return Customers;
            }

            throw new Exception("\nUnauthorize access\n");
        }
        public static List<Account> GetAllAccounts(Customer customer)
        {
            if (customer.IsAdmin)
            {
                return AllAccounts;
            }

            throw new Exception("\nUnauthorize access\n");
        }
        public static List<Account> GetAllAccountsByType(Customer customer, string type)
        {
            if (customer.IsAdmin)
            {
                List<Account> AllAccount = new List<Account>();
                foreach (Account account in AllAccounts)
                {
                    if (account.Type == type)
                    {
                        AllAccount.Add(account);
                    }
                }

                return AllAccount;
            }

            throw new Exception("\nUnauthorize access\n");
        }
        public static Account GetAccountByNumber(string number)
        {
            foreach (Account account in AllAccounts)
            {
                if (account.Number == number)
                {
                    return account;
                }
            }

            return null;
        }
        public static List<Transaction> GetAllTransactions(Customer customer)
        {
            if (customer.IsAdmin)
            {
                return AllTransactions;
            }

            throw new Exception("\nUnauthorize access\n");
        }
        public static List<Transaction> GetAllTransactionsByType(Customer customer, string type)
        {
            if (customer.IsAdmin)
            {
                List<Transaction> AllTransaction = new List<Transaction>();
                foreach (Transaction transaction in AllTransactions)
                {
                    if (transaction.Type == type)
                    {
                        AllTransaction.Add(transaction);
                    }
                }

                return AllTransaction;
            }

            throw new Exception("\nUnauthorize access\n");
        }
        public static Transaction GetTransactionByID(Customer customer, string id)
        {
            if (customer.IsAdmin)
            {
                foreach (Transaction transaction in AllTransactions)
                {
                    if (transaction.Id == id)
                    {
                        return transaction;
                    }
                }
                return null;
            }

            throw new Exception("Unauthorize access");
        }
        public static int[] GetTotalAccountCount(Customer customer)
        {
            int[] TotalAccountCount = new int[4];
            TotalAccountCount[0] = TotalSavingsAccount;
            TotalAccountCount[1] = TotalCurrentAccount;
            TotalAccountCount[2] = TotalDomiciliaryAccount;
            TotalAccountCount[3] = TotalSavingsAccount + TotalDomiciliaryAccount + TotalCurrentAccount;

            return TotalAccountCount;
        }
    }
}
