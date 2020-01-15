using System;
//using TransactionApi;
using AccountApi;
using UserApi;
using BankLedgerApi;
using System.Collections.Generic;
using System.Text;

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
                                        List<Customer> AllCUstomers = BankLedger.GetAllAllUsers();
                                        if (AllCUstomers == null)
                                        {
                                            Console.WriteLine("No registered customer yet.\n");
                                        }
                                        else
                                        {
                                            StringBuilder ResultList = new StringBuilder();
                                            ResultList.AppendLine("| Customer ID |     Name      | Date Registered |");
                                            foreach (Customer customer in AllCUstomers)
                                            {
                                                string date = string.Format("{0: dd-MM-yyyy HH:mm:ss}", customer.DateRegistered);
                                                ResultList.AppendLine($"| {customer.Id} | {customer.Name} |  {date} |");
                                            }
                                            Console.WriteLine(ResultList.ToString());
                                        }
                                        break;
                                    case 2:
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
                                            Console.WriteLine($"Date registered: {customer.Id}");
                                            Console.WriteLine($"Date registered: {customer.Name}");
                                            Console.WriteLine("All Account operated by this user\n");
                                            //customer.GetAccountOverview();
                                        }
                                        break;
                                    case 3:
                                        Console.WriteLine("Todo");
                                        break;
                                    case 4:
                                        Console.WriteLine("Todo");
                                        break;
                                    case 5:
                                        Console.WriteLine("Todo");
                                        break;
                                    case 6:
                                        Console.WriteLine("Todo");
                                        break;
                                    case 7:
                                        Console.WriteLine("Todo");
                                        break;
                                    case 8:
                                        Console.WriteLine("Todo");
                                        break;
                                    case 9:
                                        Console.WriteLine("Todo");
                                        break;
                                    default:
                                        Console.WriteLine();
                                        Console.WriteLine("====================================================");
                                        Console.WriteLine($"{LoggedInUser.Name}, you are now logged out\n");
                                        Console.WriteLine("====================================================");
                                        LoggedInUser = null;
                                        IsUserSessionOn = false;
                                        break;
                                }
                            } while (IsUserSessionOn);
                        }
                        else
                        {
                            Customer LoggedInUser = (Customer)AuthenticatedUser;
                            Console.WriteLine();
                            Console.Write($"Welcome {LoggedInUser.Name} ");
                            if (LoggedInUser.Accounts.Count == 0)
                            {
                                Console.WriteLine();
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
                                        Console.WriteLine("Todo");
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
                                        Console.WriteLine($"{LoggedInUser.Name} thanks for banking with us\n");
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
