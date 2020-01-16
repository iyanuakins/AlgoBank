using System;
using TransactionApi;
using AccountApi;
using UserApi;
using BankLedgerApi;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AlgoBank
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Algo Bank\n");
            bool IsUserSessionOn = false;
            BankLedger.RegisterSuperAdmin();
            do
            {
                int FirstSelectedOption;
                bool IsValid = false;
                do
                {
                    Console.WriteLine("1)  =>  Register\n" +
                                      "2)  =>  Login\n" +
                                      "3)  =>  Exit bank");
                    Console.Write("Select option: ");
                    string UserInput = Console.ReadLine();
                    IsValid = int.TryParse(UserInput, out FirstSelectedOption);
                    if (!(IsValid && (1 <= FirstSelectedOption && FirstSelectedOption <= 3)))
                    {
                        IsValid = false;
                        Console.WriteLine("Invalid option, Please select valid option\n");
                    }
                } while (!IsValid);

                if (FirstSelectedOption == 1)
                {
                    bool IsSuccess = BankLedger.RegisterCustomer();
                    if (IsSuccess)
                    {
                        Console.WriteLine("Registration successful, you can now login\n");
                    }
                }
                else if(FirstSelectedOption == 2)
                {
                    User AuthenticatedUser = BankLedger.AuthenticateCustomer();
                    if (AuthenticatedUser != null)
                    {
                        IsUserSessionOn = true;
                        if (AuthenticatedUser.GetType() == typeof(Admin))
                        {
                            Admin LoggedInUser = (Admin)AuthenticatedUser;
                            //Check if the admin is not supended
                            if(LoggedInUser.Level > 0)
                            {
                                int[] AccountCount = BankLedger.GetTotalAccountCount();
                                Console.WriteLine($"Welcome Admin {LoggedInUser.Name}\n");
                                Console.WriteLine("================================================================");
                                Console.WriteLine("                           Bank ledger summary            ");
                                Console.WriteLine("================================================================"); 
                                Console.WriteLine($"A total of {Customer.TotalCustomer} customer(s) are operating a total of {AccountCount[3]} account(s)\n" +
                                                    "Breakdown:\n" +
                                                    $"Savings accounts: {AccountCount[0]}\n" +
                                                    $"Current accounts: {AccountCount[1]}\n" +
                                                    $"Domiciliary accounts: {AccountCount[2]}\n" +
                                                    "\n" +
                                                    $"Total Amount in bank: NGN{Account.TotalAmountInBank}\n" +
                                                    "\n");
                                do
                            {
                                int SecondSelectedOption;
                                bool IsValidOption = false;
                                do
                                {
                                    string CreateAdmin = LoggedInUser.Level < 3 ? "Create Admins (Unavailbale)" : "Create Admins";
                                    string ManageAdmin = LoggedInUser.Level < 3 ? "Manage Admins (Unavailbale)" : "Manage Admins";
                                    Console.WriteLine("============================================");
                                    Console.WriteLine("             Available Operations         ");
                                    Console.WriteLine("=============================================");
                                    Console.WriteLine("1) => View all customers\n" +
                                                       "2) => Get customer by ID\n" +
                                                       "3) => View all administrators\n" +
                                                       "4) => View all bank accounts\n" +
                                                       "5) => View bank accounts by type\n" +
                                                       "6) => View all transctions\n" +
                                                       "7) => View transction by reference number\n" +
                                                       $"8) => {ManageAdmin}\n" +
                                                       $"9) => {CreateAdmin}\n" +
                                                       "0) => Log out\n");
                                    Console.Write("Select operation: ");
                                    string UserInputForOperation = Console.ReadLine();
                                    IsValidOption = int.TryParse(UserInputForOperation, out SecondSelectedOption);
                                    if (!(IsValidOption && (0 <= SecondSelectedOption || SecondSelectedOption <= 9)))
                                    {
                                        IsValidOption = false;
                                        Console.WriteLine("Invalid option, Please select valid option\n");
                                    }
                                } while (!IsValidOption);

                                switch (SecondSelectedOption)
                                {
                                    case 1:
                                        //Get all users
                                        List<Customer> AllCustomers = BankLedger.GetAllCustomers();
                                        if (AllCustomers == null)
                                        {
                                            Console.WriteLine("No registered customer yet.\n");
                                        }
                                        else
                                        {
                                            StringBuilder ResultList = new StringBuilder();
                                            ResultList.AppendLine("| Customer ID |     Name      | Date Registered |");
                                            foreach (Customer ThisCustomer in AllCustomers)
                                            {
                                                string date = string.Format("{0: dd-MM-yyyy HH:mm:ss}", ThisCustomer.DateRegistered);
                                                ResultList.AppendLine($"| {ThisCustomer.Id} |    {ThisCustomer.Name}    | {date} |");
                                            }
                                            Console.WriteLine(ResultList.ToString());
                                        }
                                        break;
                                    case 2:
                                        //Get all customer by ID
                                        bool IsValidOption2 = false;
                                        int UserID;
                                        do
                                        {
                                            Console.Write("Enter user id: ");
                                            string UserInputForOption = Console.ReadLine();
                                            IsValidOption2 = int.TryParse(UserInputForOption, out UserID);
                                            if (!IsValidOption2)
                                            {
                                                IsValidOption2 = false;
                                                Console.WriteLine("\nEnter valid user id\n");
                                            }
                                        } while (!IsValidOption2);
                                        Customer customer = (Customer)BankLedger.GetUserByID(UserID);
                                        if (customer == null)
                                        {
                                            Console.WriteLine("User not found.\n");
                                        }
                                        else
                                        {
                                            Console.WriteLine();
                                            Console.WriteLine($"Date registered: {customer.DateRegistered}");
                                            Console.WriteLine($"Customer ID: {customer.Id}");
                                            Console.WriteLine($"Customer name: {customer.Name}");
                                            Console.WriteLine("All Account operated by this user\n");
                                            customer.GetAccountsOverview();
                                        }
                                        break;
                                    case 3:
                                        //Get all administrators
                                        List<Admin> AllAdmins = BankLedger.GetAllAdmins();
                                        if (AllAdmins == null)
                                        {
                                            Console.WriteLine("No registered customer yet.\n");
                                        }
                                        else
                                        {
                                            StringBuilder ResultList = new StringBuilder();
                                            ResultList.AppendLine("| Customer ID |     Name      | Date Registered |");
                                            foreach (Admin ThisAdmin in AllAdmins)
                                            {
                                                string date = string.Format("{0: dd-MM-yyyy HH:mm:ss}", ThisAdmin.DateRegistered);
                                                ResultList.AppendLine($"| {ThisAdmin.Id} |    {ThisAdmin.Name}    | {date} |");
                                            }
                                            Console.WriteLine(ResultList.ToString());
                                        }
                                        break;
                                    case 4:
                                        //Get all accounts
                                        List<Account> AllAccounts = BankLedger.GetAllAccounts();
                                        if (AllAccounts == null)
                                        {
                                            Console.WriteLine("\nNo Account created yet\n");
                                        }
                                        else
                                        {
                                            StringBuilder statement = new StringBuilder();
                                            statement.AppendLine();
                                            statement.AppendLine("| Account type | Account number |      Owner      | Date created |");
                                            foreach (Account account in AllAccounts)
                                            {
                                                string date = string.Format("{0: dd-MM-yyyy HH:mm:ss}", account.DateCreated);
                                                statement.AppendLine($"| {account.Type} | {account.Number} | {account.OwnerName} |   {date}   |");
                                            }
                                            statement.AppendLine();
                                            Console.WriteLine(statement.ToString());
                                            Console.WriteLine();
                                        }
                                        break;
                                    case 5:
                                        //Get all accounts by type
                                        Console.Write("Enter account type to fetch: ");
                                        string AccountType = Console.ReadLine();
                                        List<Account> AllAccountType = BankLedger.GetAllAccountsByType(AccountType);
                                        if (AllAccountType == null)
                                        {
                                            Console.WriteLine($"\nNo {AccountType} Account  created yet\n");
                                        }
                                        else
                                        {
                                            StringBuilder statement = new StringBuilder();
                                            statement.AppendLine();
                                            statement.AppendLine($"All {AccountType} Account in operation");
                                            statement.AppendLine("| Account type | Account number |      Owner      | Date created |");
                                            foreach (Account account in AllAccountType)
                                            {
                                                string date = string.Format("{0: dd-MM-yyyy HH:mm:ss}", account.DateCreated);
                                                statement.AppendLine($"| {account.Type} | {account.Number} | {account.OwnerName} |   {date}   |");
                                            }
                                            statement.AppendLine();
                                            Console.WriteLine(statement.ToString());
                                            Console.WriteLine();
                                        }

                                        break;
                                    case 6:
                                        //Get all transactions
                                        List<Transaction> AllTransactions = BankLedger.GetAllTransactions();
                                        if (AllTransactions == null)
                                        {
                                            Console.WriteLine($"\nNo recorded transaction yet\n");
                                        }
                                        else
                                        {
                                            StringBuilder statement = new StringBuilder();
                                            statement.AppendLine();
                                            statement.AppendLine("| Transaction ref num | Transaction type | Amount | Sender | Receiver | Transaction Date |");
                                            foreach (Transaction transaction in AllTransactions)
                                            {
                                                string date = string.Format("{0: dd-MM-yyyy HH:mm:ss}", transaction.DateCreated);
                                                statement.AppendLine($"| {transaction.Id} | {transaction.Type} | {transaction.Amount} | {transaction.Sender} | {transaction.Receiver} | {date} |");
                                            }
                                            statement.AppendLine();
                                            Console.WriteLine(statement.ToString());
                                            Console.WriteLine();
                                        }
                                        break;
                                    case 7:
                                        //Get transction by reference number
                                        Console.Write("Enter transction reference number: ");
                                        string RefNumber = Console.ReadLine();
                                        Transaction Result = null;
                                        foreach (Transaction transaction in Transaction.AllTransactions)
                                        {
                                            if (transaction.Id == RefNumber)
                                            {
                                                Result = transaction;
                                                break;
                                            }
                                        }
                                        if (Result == null)
                                        {
                                            Console.WriteLine("\nTransaction not found\n");
                                        }
                                        else
                                        {
                                            StringBuilder statement = new StringBuilder();
                                            statement.AppendLine();
                                            statement.AppendLine("| Transaction ref num | Transaction type | Amount | Sender | Receiver | Transaction Date |");
                                            string date = string.Format("{0: dd-MM-yyyy HH:mm:ss}", Result.DateCreated);
                                            statement.AppendLine($"| {Result.Id} | {Result.Type} | {Result.Amount} | {Result.Sender} | {Result.Receiver} | {date} |");
                                            Console.WriteLine();
                                            Console.WriteLine(statement.ToString());
                                        }
                                        break;
                                    case 8:
                                        //Manage Admins (Available to administrator with level 3)
                                        if (LoggedInUser.Level > 2)
                                        {
                                            List<Admin> AdminsToManage = BankLedger.GetAllAdmins();
                                            //Excluding super admin from the management
                                            if (AdminsToManage == null || AdminsToManage.Count == 1)
                                            {
                                                Console.WriteLine("No Admins to manage.\n");
                                            }
                                            else
                                            {
                                                StringBuilder ResultList = new StringBuilder();
                                                int i = 0;
                                                foreach (Admin ThisAdmin in AdminsToManage)
                                                {
                                                    //Excluding the super admin from the list of admins to be displayed.
                                                    if (i != 0)
                                                    {
                                                        string AdminType = ThisAdmin.Level < 3 ? "Ordinary Administrator" : "Super Administrator"; 
                                                        ResultList.AppendLine($"{i})  =>  {ThisAdmin.Name}[{AdminType}]");
                                                    }
                                                    i++;
                                                }
                                                //Prompt admin to for admin selection
                                                bool IsValidAminSelection = false;
                                                int SelectedAdminIndex;
                                                Admin SelectedAdmin;
                                                do
                                                {
                                                    Console.WriteLine(ResultList.ToString());
                                                    string AdminInputForOption = Console.ReadLine();
                                                        IsValidAminSelection = int.TryParse(AdminInputForOption, out SelectedAdminIndex);
                                                    if (!(IsValidAminSelection && (1 <= SelectedAdminIndex && SelectedAdminIndex <= AdminsToManage.Count - 1)))
                                                    {
                                                        IsValidAminSelection = false;
                                                        Console.WriteLine("Invalid option, Please select valid option\n");
                                                    }
                                                    //Getting the admin selected for management
                                                    SelectedAdmin = AdminsToManage[SelectedAdminIndex];
                                                } while (!IsValidAminSelection);

                                                //Prompt admin to for action
                                                bool IsValidManagementOption = false;
                                                int PromptSelectedOption;
                                                do
                                                {
                                                    Console.WriteLine("1)  =>  Promote to super admin\n" +
                                                                      "2)  =>  Demote to ordinary admin\n" +
                                                                      "3)  =>   Suspend admin\n");
                                                    string UserInputForOption = Console.ReadLine();
                                                    IsValidManagementOption = int.TryParse(UserInputForOption, out PromptSelectedOption);
                                                    if (!(IsValidManagementOption && (1 <= PromptSelectedOption && PromptSelectedOption <= 2)))
                                                    {
                                                        IsValidManagementOption = false;
                                                        Console.WriteLine("Invalid option, Please select valid option\n");
                                                    }
                                                } while (!IsValidManagementOption);

                                                //Perform operation based on the prompt selected
                                                if (PromptSelectedOption == 1)
                                                {
                                                    SelectedAdmin.ManageAdmin(3);
                                                    Console.WriteLine($"{SelectedAdmin.Name} has been promoted to super admin");
                                                }
                                                else if (PromptSelectedOption == 2)
                                                {
                                                    SelectedAdmin.ManageAdmin(1);
                                                    Console.WriteLine($"{SelectedAdmin.Name} has been demoted to ordinary admin");
                                                }
                                                else
                                                {
                                                    SelectedAdmin.ManageAdmin(0);
                                                    Console.WriteLine($"{SelectedAdmin.Name} has been suspended");
                                                }
                                            } 
                                        }
                                        else
                                        {
                                            Console.WriteLine("\nOperation require higher adminstrative level\n");
                                        }
                                        break;
                                    case 9:
                                        //Create Admins (Available to administrator with level 3)
                                        if (LoggedInUser.Level > 2)
                                        {
                                            bool IsAllValid = false;
                                            bool IsContinue = true;
                                            string name = "";
                                            string email = "";
                                            do
                                            {
                                                bool IsValidName = false;

                                                do
                                                {
                                                    Console.WriteLine("================================================================");
                                                    Console.WriteLine("                     Fill Form to create a new admin                    ");
                                                    Console.WriteLine("================================================================");

                                                    Console.Write("Enter First name and Last name: ");
                                                    name = Console.ReadLine();
                                                    IsValidName = Regex.IsMatch(name, @"^[A-Za-z\s.\'\-]+$", RegexOptions.IgnoreCase);
                                                    if (!IsValidName)
                                                    {
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
                                                    else if (BankLedger.AllUsers.Count != 0)
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
                                                                    Console.WriteLine("\nEnter 1 to try another email\nEnter 2 to exit admin registration process");
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
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                    }
                                                } while (!IsValidEmail);

                                                IsContinue = IsValidEmail && IsValidName;
                                                IsAllValid = true;
                                            } while (!IsAllValid);

                                            if (IsContinue)
                                            {
                                                BankLedger.CreateAdmin(name, email);
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Operation require higher adminstrative level");
                                        }
                                        break;
                                    default:
                                        Console.WriteLine();
                                        Console.WriteLine("====================================================");
                                        Console.WriteLine($"Admin {LoggedInUser.Name}, you are now logged out");
                                        Console.WriteLine("====================================================");
                                        LoggedInUser = null;
                                        IsUserSessionOn = false;
                                        break;
                                }
                            } while (IsUserSessionOn);
                            }
                            else
                            {
                                Console.WriteLine("\nYour account has been suspended\n");
                                IsUserSessionOn = false;
                            }

                        }
                        else
                        {
                            Customer LoggedInUser = (Customer)AuthenticatedUser;
                            Console.WriteLine();
                            Console.Write($"Welcome {LoggedInUser.Name} ");
                            if (LoggedInUser.Accounts.Count == 0)
                            {
                                Console.WriteLine("We are happy to have you on board.\n");
                                Console.WriteLine("Please open your first account\n");
                                LoggedInUser.CreateAccount();
                            }

                            Console.WriteLine("What would you like to do?\n");
                            do
                            {
                                int SecondSelectedOption;
                                bool IsValidOption = false;
                                do
                                {
                                    Console.WriteLine("========================================");
                                    Console.WriteLine("           Available Operations         ");
                                    Console.WriteLine("=========================================");
                                    Console.WriteLine("1) => Check account balance\n"+
                                                       "2) => Get account overview\n" +
                                                       "3) => Deposit\n" +
                                                       "4) => Withdraw\n" +
                                                       "5) => Transfer\n" +
                                                       "6) => Get account statement\n" +
                                                       "7) => Create account\n" +
                                                       "8) => Log out\n");
                                    Console.WriteLine();
                                    Console.Write("Select operation: ");
                                    string UserInputForOperation = Console.ReadLine();
                                    IsValidOption = int.TryParse(UserInputForOperation, out SecondSelectedOption);
                                    if (!(IsValidOption && (1 <= SecondSelectedOption|| SecondSelectedOption <= 8)))
                                    {
                                        IsValidOption = false;
                                        Console.WriteLine("Invalid option, Please select valid option\n");
                                    }
                                } while (!IsValidOption);

                                switch (SecondSelectedOption)
                                {
                                    case 1:
                                        Account SelectedAccount;
                                        string balance;
                                        if (LoggedInUser.Accounts.Count == 1)
                                        {
                                            SelectedAccount = LoggedInUser.Accounts[0];
                                        }
                                        else
                                        {
                                            SelectedAccount = LoggedInUser.SelectAccount();
                                        }
                                        balance = SelectedAccount.GetBalance();
                                        Console.WriteLine($"Your Account balance is: {balance}\n");
                                        break;
                                    case 2:
                                        LoggedInUser.GetAccountsOverview();
                                        break;
                                    case 3:
                                        SelectedAccount = LoggedInUser.Accounts.Count == 1 ? LoggedInUser.Accounts[0] : LoggedInUser.SelectAccount();
                                        bool IsValidAmount = false;
                                        double amount;
                                        do
                                        {
                                            Console.Write("Enter amount you want to deposit: ");
                                            string UserInput = Console.ReadLine();
                                            IsValidAmount = double.TryParse(UserInput, out amount);
                                            if (!IsValidAmount)
                                            {
                                                Console.WriteLine("Invalid amount, Please enter valid amount");
                                            }
                                        } while (!IsValidAmount);
                                        Console.WriteLine(SelectedAccount.Deposit(amount));
                                        break;
                                    case 4:
                                        SelectedAccount = LoggedInUser.Accounts.Count == 1 ? LoggedInUser.Accounts[0] : LoggedInUser.SelectAccount();
                                        IsValidAmount = false;
                                        amount = 0;
                                        do
                                        {
                                            Console.Write("Enter amount you want to withdraw: ");
                                            string UserInput = Console.ReadLine();
                                            IsValidAmount = double.TryParse(UserInput, out amount);
                                            if (!IsValidAmount)
                                            {
                                                Console.WriteLine("Invalid amount, Please enter valid amount\n");
                                            }
                                        } while (!IsValidAmount);
                                        Console.WriteLine(SelectedAccount.Withdraw(amount));
                                        break;
                                    case 5:
                                        SelectedAccount = LoggedInUser.Accounts.Count == 1 ? LoggedInUser.Accounts[0] : LoggedInUser.SelectAccount();
                                        IsValidAmount = false;
                                        bool IsValidAccount = false;
                                        bool IsContinue = true;
                                        amount = 0;
                                        Account RecipientAccount = null;

                                        do
                                        {
                                            Console.Write("Enter amount: ");
                                            string UserInput = Console.ReadLine();
                                            IsValidAmount = double.TryParse(UserInput, out amount);
                                            if (!IsValidAmount)
                                            {
                                                Console.WriteLine("Invalid amount, Please enter valid amount\n");
                                            }
                                        } while (!IsValidAmount);

                                        do
                                        {
                                            Console.Write("Enter recipient account number: ");
                                            string UserInput = Console.ReadLine();
                                            int Account;
                                            IsValidAccount = int.TryParse(UserInput, out Account);
                                            if (!IsValidAccount)
                                            {
                                                Console.WriteLine("Account provided does not exist\n");
                                                bool IsValidOption2 = false;
                                                int PromptSelectedOption;
                                                do
                                                {
                                                    Console.WriteLine("Enter 1 to re-enter\nEnter 2 to exit transfer process\n");
                                                    string UserInputForOption = Console.ReadLine();
                                                    IsValidOption2 = int.TryParse(UserInputForOption, out PromptSelectedOption);
                                                    if (!(IsValidOption2 && (PromptSelectedOption == 1 || PromptSelectedOption == 2)))
                                                    {
                                                        IsValidOption2 = false;
                                                        Console.WriteLine("Invalid option, Please select valid option\n");
                                                    }
                                                } while (!IsValidOption2);
                                                if (PromptSelectedOption == 1)
                                                {
                                                    IsValidAccount = false;
                                                }
                                                else
                                                {
                                                    IsValidAccount = true;
                                                    IsContinue = false;
                                                }
                                            }
                                            else
                                            {
                                                string EnteredAccount = Convert.ToString(Account).Trim();
                                                RecipientAccount = BankLedger.GetAccountByNumber(EnteredAccount);
                                                if (RecipientAccount == null)
                                                {
                                                    Console.WriteLine("Account provided does not exist\n");
                                                    bool IsValidOption2 = false;
                                                    int PromptSelectedOption;
                                                    do
                                                    {
                                                        Console.WriteLine("1)  =>  Re-enter account number\n" +
                                                                          "2)  =>  Exit transfer process\n");
                                                        string UserInputForOption = Console.ReadLine();
                                                        IsValidOption2 = int.TryParse(UserInputForOption, out PromptSelectedOption);
                                                        if (!(IsValidOption2 && (PromptSelectedOption == 1 || PromptSelectedOption == 2)))
                                                        {
                                                            IsValidOption2 = false;
                                                            Console.WriteLine("Invalid option, Please select valid option\n");
                                                        }
                                                    } while (!IsValidOption2);
                                                    if (PromptSelectedOption == 1)
                                                    {
                                                        IsValidAccount = false;
                                                    }
                                                    else
                                                    {
                                                        IsValidAccount = true;
                                                        IsContinue = false;
                                                    }
                                                }
                                            }
                                        } while (!IsValidAccount);

                                        if (IsContinue)
                                        {
                                            Console.WriteLine(SelectedAccount.Transfer(amount, RecipientAccount));
                                        }
                                        break;
                                    case 6:
                                        SelectedAccount = LoggedInUser.Accounts.Count == 1 ? LoggedInUser.Accounts[0] : LoggedInUser.SelectAccount();
                                        SelectedAccount.GetAccountStatement();
                                        break;
                                    case 7:
                                        LoggedInUser.CreateAccount();
                                        break;
                                    default:
                                        Console.WriteLine();
                                        Console.WriteLine("====================================================");
                                        Console.WriteLine($"{LoggedInUser.Name} thanks for banking with us");
                                        Console.WriteLine("====================================================");
                                        LoggedInUser = null;
                                        IsUserSessionOn = false;
                                        break;
                                }
                            } while (IsUserSessionOn);
                        }
                    }
                }
                else
                {
                    IsUserSessionOn = true;
                }
            } while (!IsUserSessionOn);
        }
    }
}
