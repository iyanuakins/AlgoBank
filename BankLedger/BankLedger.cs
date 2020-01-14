using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TransactionApi;
using AccountApi;
using UserApi;

namespace BankLedgerApi
{
    public class BankLedger
    {
        private static List<User> allUsers = new List<User>();

        public BankLedger()
        {
            //Create a super admin
            User SuperAdmin = new Admin("Iyanu Akins", "admin@test.com", "000000", 3);
            AllUsers.Add(SuperAdmin);
        }

        private static List<User> AllUsers { get => AllUsers; }

        public static bool RegisterCustomer()
        {
            bool IsValid = false;
            string name = "";
            string email = "";
            string password = "";
            do
            {
                bool IsValidName = false;

                do
                {
                    Console.WriteLine("================================================================");
                    Console.WriteLine("                     Fill Registration Form                     ");
                    Console.WriteLine("================================================================");

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
                    else if (AllUsers.Count != 0)
                    {
                        foreach (User user in BankLedger.AllUsers)
                        {
                            if (user.Email == email)
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

                                if (option == 2)
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
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine("\nNote:\n1. Password should be minimun of 6 and maximum of 32 characters\n2. Password Can contain alphabets, number and symbols\n");
                    Console.WriteLine("----------------------------------------------------------------");
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
                            string Asterisks = "".PadLeft(number, '*');
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
                                string Asterisks = "".PadLeft(number, '*');
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

            User NewCustomer = new Customer(name, email, password);
            BankLedger.AllUsers.Add(NewCustomer);
            Customer.TotalCustomer++;
            return true;
        }

        public static User AuthenticateCustomer()
        {
            bool IsRetry = true;
            string email = "";
            string password = "";
            User LoggedUser = null;
            do
            {
                Console.WriteLine("================================================================");
                Console.WriteLine("                     Fill Login Form                     ");
                Console.WriteLine("================================================================");
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
                        string Asterisks = "".PadLeft(number, '*');
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
                foreach (User customer in BankLedger.AllUsers)
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

        public void CreateAdmin(Admin admin, string name, string email, int level = 1)
        {
            if(admin.Level > 2)
            {
                //Create a super admin
                User Admin = new Admin(name, email, "newadmin", level);
                AllUsers.Add(Admin);
                Console.WriteLine($"An administrative account of level {level} has been opened for {name}");
            }
            else
            {
                Console.WriteLine("Operation require higher clearance level");
            }
        }

        public static User GetUserByID(int id)
        {
            foreach (User customer in AllUsers)
            {
                if (customer.Id == id)
                {
                    return customer;
                }
            }
            return null;
        }

        public static List<User> GetAllAllUsers()
        {
            List<User> AllCustomer = new List<User>();
            foreach (User customer in AllUsers)
            {
                if (customer.GetType() == typeof(Customer))
                {
                    AllCustomer.Add(customer);
                }
            }

            return AllCustomer.Count == 0 ? null : AllCustomer;
        }

        public static List<Account> GetAllAccounts()
        {
            return Account.AllAccounts;
        }

        public static List<Account> GetAllAccountsByType(string type)
        {
            List<Account> AllAccount = new List<Account>();
            foreach (Account account in Account.AllAccounts)
            {
                if (account.Type == type)
                {
                    AllAccount.Add(account);
                }
            }
            return AllAccount.Count == 0 ? null : AllAccount;
        }

        public static Account GetAccountByNumber(string number)
        {
            foreach (Account account in Account.AllAccounts)
            {
                if (account.Number == number)
                {
                    return account;
                }
            }

            return null;
        }

        public static List<Transaction> GetAllTransactions()
        {
            return Transaction.AllTransactions.Count == 0 ? null : Transaction.AllTransactions;
        }

        public static List<Transaction> GetAllTransactionsByType(string type)
        {
            List<Transaction> AllTransactions = new List<Transaction>();
            foreach (Transaction transaction in Transaction.AllTransactions)
            {
                if (transaction.Type == type)
                {
                    AllTransactions.Add(transaction);
                }
            }

            return AllTransactions.Count == 0 ? null : AllTransactions;
        }

        public static Transaction GetTransactionByID(string id)
        {
            foreach (Transaction transaction in Transaction.AllTransactions)
            {
                if (transaction.Id == id)
                {
                    return transaction;
                }
            }
            return null;
        }

        public static int[] GetTotalAccountCount()
        {
            int[] TotalAccountCount = new int[4];
            TotalAccountCount[0] = Account.TotalSavingsAccount;
            TotalAccountCount[1] = Account.TotalCurrentAccount;
            TotalAccountCount[2] = Account.TotalDomiciliaryAccount;
            TotalAccountCount[3] = Account.TotalSavingsAccount + Account.TotalDomiciliaryAccount + Account.TotalCurrentAccount;

            return TotalAccountCount;
        }
    }
}
