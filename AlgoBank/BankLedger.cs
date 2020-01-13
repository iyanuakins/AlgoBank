using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AlgoBank
{
    class BankLedger
    {
        public static List<Customer> Customers = new List<Customer>();
        public static List<Account> AllAccounts = new List<Account>();
        public static List<Transaction> AllTransactions = new List<Transaction>();
        public static double TotalAmountInBank = 0.0;
        public static int TotalCustomer = 0;
        public static int TotalSavingsAccount = 0;
        public static int TotalCurrentAccount = 0;
        public static int TotalDomiciliaryAccount = 0;
        public static double USDToNaira = 362.5;
        public static double EURToNaira = 403.14;
        public static double GBPToNaira = 473.5;

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
                    Console.WriteLine("Enter your desired password");
                    Console.WriteLine("\nNote:\n1. Password should be minimun of 6 and maximum of 32 characters\n2. Password Can contain alphabets, number and symbols\n");
                    password = Console.ReadLine();
                    bool PasswordValid = 6 <= password.Length && password.Length <= 36;
                    if (PasswordValid)
                    {
                        Console.WriteLine("Confirm Password: ");
                        string ConfirmPassword = Console.ReadLine();
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
                password = Console.ReadLine();

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
