using System;

namespace AlgoBank
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Algo Bank\n");
            bool IsCustomerSessionOn = false;
            do
            {
                int FirstSelectedOption;
                bool IsValid = false;
                do
                {
                    Console.WriteLine("Enter 1 register\nEnter 2 to login\nEnter 3 to exit");
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
                    Customer LoggedInUser = BankLedger.AuthenticateCustomer();
                    if (LoggedInUser != null)
                    {
                        IsCustomerSessionOn = true;
                        if (LoggedInUser.IsAdmin)
                        {
                            int[] AccountCount = BankLedger.GetTotalAccountCount(LoggedInUser);
                            Console.WriteLine($"Welcome Admin {LoggedInUser.Name}\n");
                            Console.WriteLine("Below is summary of bank ledger:");
                            Console.WriteLine($"A total of {(BankLedger.TotalCustomer - 1)} are operating a total of {AccountCount[3]}\n"+
                                                "Breakdown:" +
                                                $"Savings accounts: {AccountCount[0]}\n" +
                                                $"Current accounts: {AccountCount[1]}\n" +
                                                $"Domiciliary accounts: {AccountCount[2]}\n" +
                                                "\n" +
                                                $"Total Amount in bank: NGN{BankLedger.TotalAmountInBank}\n" +
                                                "\n");
                            //do
                            //{
                            //    int SecondSelectedOption;
                            //    bool IsValidOption = false;
                            //    do
                            //    {
                            //        Console.WriteLine("Enter 1 check account balance\n" +
                            //                           "Enter 2 to deposit\n" +
                            //                           "Enter 3 to withdraw\n" +
                            //                           "Enter 4 to transfer\n" +
                            //                           "Enter 5 to get account statement\n" +
                            //                           "Enter 6 to a new create account\n" +
                            //                           "Enter 7 to log out\n");
                            //        string UserInputForOperation = Console.ReadLine();
                            //        IsValidOption = int.TryParse(UserInputForOperation, out SecondSelectedOption);
                            //        if (!(IsValidOption && (1 <= SecondSelectedOption || SecondSelectedOption <= 7)))
                            //        {
                            //            IsValidOption = false;
                            //            Console.WriteLine("Invalid option, Please select valid option\n");
                            //        }
                            //    } while (!IsValidOption);

                            //    switch (SecondSelectedOption)
                            //    {
                            //        case 1:
                            //            if (LoggedInUser.Accounts.Count == 0)
                            //            {
                            //                Console.WriteLine("Create a bank account first\n");
                            //                goto case 6;
                            //            }
                            //            else
                            //            {
                            //                Account SelectedAccount;
                            //                string balance;
                            //                if (LoggedInUser.Accounts.Count == 1)
                            //                {
                            //                    SelectedAccount = LoggedInUser.Accounts[0];
                            //                }
                            //                else
                            //                {
                            //                    SelectedAccount = LoggedInUser.SelectAccount();
                            //                }
                            //                balance = SelectedAccount.GetBalance();
                            //                Console.WriteLine($"Your Account balance is: {balance}\n");
                            //            }
                            //            break;
                            //        case 2:
                            //            if (LoggedInUser.Accounts.Count == 0)
                            //            {
                            //                Console.WriteLine("Create a bank account first\n");
                            //                goto case 6;
                            //            }
                            //            else
                            //            {
                            //                Account SelectedAccount = LoggedInUser.Accounts.Count == 1 ? LoggedInUser.Accounts[0] : LoggedInUser.SelectAccount();
                            //                bool IsValidAmount = false;
                            //                double amount;
                            //                do
                            //                {
                            //                    Console.WriteLine("Enter amount you want to deposit");
                            //                    string UserInput = Console.ReadLine();
                            //                    IsValidAmount = double.TryParse(UserInput, out amount);
                            //                    if (!IsValidAmount)
                            //                    {
                            //                        Console.WriteLine("Invalid amount, Please enter valid amount");
                            //                    }
                            //                } while (!IsValidAmount);
                            //                Console.WriteLine(SelectedAccount.Deposit(amount));
                            //            }
                            //            break;
                            //        case 3:
                            //            if (LoggedInUser.Accounts.Count == 0)
                            //            {
                            //                Console.WriteLine("Create a bank account first\n");
                            //                goto case 6;
                            //            }
                            //            else
                            //            {
                            //                Account SelectedAccount = LoggedInUser.Accounts.Count == 1 ? LoggedInUser.Accounts[0] : LoggedInUser.SelectAccount();
                            //                bool IsValidAmount = false;
                            //                double amount;
                            //                do
                            //                {
                            //                    Console.WriteLine("Enter amount you want to withdraw\n");
                            //                    string UserInput = Console.ReadLine();
                            //                    IsValidAmount = double.TryParse(UserInput, out amount);
                            //                    if (!IsValidAmount)
                            //                    {
                            //                        Console.WriteLine("Invalid amount, Please enter valid amount\n");
                            //                    }
                            //                } while (!IsValidAmount);
                            //                Console.WriteLine(SelectedAccount.Withdraw(amount));
                            //            }
                            //            break;
                            //        case 4:
                            //            if (LoggedInUser.Accounts.Count == 0)
                            //            {
                            //                Console.WriteLine("Create a bank account first");
                            //                goto case 6;
                            //            }
                            //            else
                            //            {
                            //                Account SelectedAccount = LoggedInUser.Accounts.Count == 1 ? LoggedInUser.Accounts[0] : LoggedInUser.SelectAccount();
                            //                bool IsValidAmount = false;
                            //                bool IsValidAccount = false;
                            //                bool IsContinue = true;
                            //                double amount;
                            //                Account RecipientAccount = null;

                            //                do
                            //                {
                            //                    Console.WriteLine("Enter amount you want to transfer\n");
                            //                    string UserInput = Console.ReadLine();
                            //                    IsValidAmount = double.TryParse(UserInput, out amount);
                            //                    if (!IsValidAmount)
                            //                    {
                            //                        Console.WriteLine("Invalid amount, Please enter valid amount\n");
                            //                    }
                            //                } while (!IsValidAmount);

                            //                do
                            //                {
                            //                    Console.WriteLine("Enter recipient account number\n");
                            //                    string UserInput = Console.ReadLine();
                            //                    int Account;
                            //                    IsValidAccount = int.TryParse(UserInput, out Account);
                            //                    if (!IsValidAccount)
                            //                    {
                            //                        Console.WriteLine("Account provided does not exist\n");
                            //                        bool IsValidOption2 = false;
                            //                        int PromptSelectedOption;
                            //                        do
                            //                        {
                            //                            Console.WriteLine("Enter 1 to re-enter\nEnter 2 to exit transfer process\n");
                            //                            string UserInputForOption = Console.ReadLine();
                            //                            IsValidOption2 = int.TryParse(UserInputForOption, out PromptSelectedOption);
                            //                            if (!(IsValidOption2 && (PromptSelectedOption == 1 || PromptSelectedOption == 2)))
                            //                            {
                            //                                IsValidOption2 = false;
                            //                                Console.WriteLine("Invalid option, Please select valid option\n");
                            //                            }
                            //                        } while (!IsValidOption2);
                            //                        if (PromptSelectedOption == 1)
                            //                        {
                            //                            IsValidAccount = false;
                            //                        }
                            //                        else
                            //                        {
                            //                            IsValidAccount = true;
                            //                            IsContinue = false;
                            //                        }
                            //                    }
                            //                    else
                            //                    {
                            //                        string EnteredAccount = Convert.ToString(Account).Trim();
                            //                        RecipientAccount = BankLedger.GetAccountByNumber(EnteredAccount);
                            //                        if (RecipientAccount == null)
                            //                        {
                            //                            Console.WriteLine("Account provided does not exist\n");
                            //                            bool IsValidOption2 = false;
                            //                            int PromptSelectedOption;
                            //                            do
                            //                            {
                            //                                Console.WriteLine("Enter 1 to re-enter\nEnter 2 to exit transfer process\n");
                            //                                string UserInputForOption = Console.ReadLine();
                            //                                IsValidOption2 = int.TryParse(UserInputForOption, out PromptSelectedOption);
                            //                                if (!(IsValidOption2 && (PromptSelectedOption == 1 || PromptSelectedOption == 2)))
                            //                                {
                            //                                    IsValidOption2 = false;
                            //                                    Console.WriteLine("Invalid option, Please select valid option\n");
                            //                                }
                            //                            } while (!IsValidOption2);
                            //                            if (PromptSelectedOption == 1)
                            //                            {
                            //                                IsValidAccount = false;
                            //                            }
                            //                            else
                            //                            {
                            //                                IsValidAccount = true;
                            //                                IsContinue = false;
                            //                            }
                            //                        }
                            //                    }
                            //                } while (!IsValidAccount);

                            //                if (IsContinue)
                            //                {
                            //                    Console.WriteLine(SelectedAccount.Transfer(amount, RecipientAccount));
                            //                }
                            //            }
                            //            break;
                            //        case 5:
                            //            if (LoggedInUser.Accounts.Count == 0)
                            //            {
                            //                Console.WriteLine("Create a bank account first\n");
                            //                goto case 6;
                            //            }
                            //            else
                            //            {
                            //                Account SelectedAccount = LoggedInUser.Accounts.Count == 1 ? LoggedInUser.Accounts[0] : LoggedInUser.SelectAccount();
                            //                SelectedAccount.GetAccountStatement();
                            //            }
                            //            break;
                            //        case 6:
                            //            LoggedInUser.CreateAccount();
                            //            break;
                            //        default:
                            //            Console.WriteLine($"{LoggedInUser.Name} thanks for banking with us\n");
                            //            LoggedInUser = null;
                            //            IsCustomerSessionOn = false;
                            //            break;
                            //    }
                            //} while (IsCustomerSessionOn);
                        }
                        else
                        {
                            Console.WriteLine($"Welcome {LoggedInUser.Name}, What would you like to do?\n");
                            do
                            {
                                int SecondSelectedOption;
                                bool IsValidOption = false;
                                do
                                {
                                    Console.WriteLine("Enter 1 check account balance\n"+
                                                       "Enter 2 to deposit\n"+
                                                       "Enter 3 to withdraw\n"+
                                                       "Enter 4 to transfer\n"+
                                                       "Enter 5 to get account statement\n"+
                                                       "Enter 6 to a new create account\n"+
                                                       "Enter 7 to log out\n");
                                    string UserInputForOperation = Console.ReadLine();
                                    IsValidOption = int.TryParse(UserInputForOperation, out SecondSelectedOption);
                                    if (!(IsValidOption && (1 <= SecondSelectedOption|| SecondSelectedOption <= 7)))
                                    {
                                        IsValidOption = false;
                                        Console.WriteLine("Invalid option, Please select valid option\n");
                                    }
                                } while (!IsValidOption);

                                switch (SecondSelectedOption)
                                {
                                    case 1:
                                        if (LoggedInUser.Accounts.Count == 0)
                                        {
                                            Console.WriteLine("Create a bank account first\n");
                                            goto case 6;
                                        }
                                        else
                                        {
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
                                        }
                                        break;
                                    case 2:
                                        if (LoggedInUser.Accounts.Count == 0)
                                        {
                                            Console.WriteLine("Create a bank account first\n");
                                            goto case 6;
                                        }
                                        else
                                        {
                                            Account SelectedAccount = LoggedInUser.Accounts.Count == 1 ? LoggedInUser.Accounts[0] : LoggedInUser.SelectAccount();
                                            bool IsValidAmount = false;
                                            double amount;
                                            do
                                            {
                                                Console.WriteLine("Enter amount you want to deposit");
                                                string UserInput = Console.ReadLine();
                                                IsValidAmount = double.TryParse(UserInput, out amount);
                                                if (!IsValidAmount)
                                                {
                                                    Console.WriteLine("Invalid amount, Please enter valid amount");
                                                }
                                            } while (!IsValidAmount);
                                            Console.WriteLine(SelectedAccount.Deposit(amount));
                                        }
                                        break;
                                    case 3:
                                        if (LoggedInUser.Accounts.Count == 0)
                                        {
                                            Console.WriteLine("Create a bank account first\n");
                                            goto case 6;
                                        }
                                        else
                                        {
                                            Account SelectedAccount = LoggedInUser.Accounts.Count == 1 ? LoggedInUser.Accounts[0] : LoggedInUser.SelectAccount();
                                            bool IsValidAmount = false;
                                            double amount;
                                            do
                                            {
                                                Console.WriteLine("Enter amount you want to withdraw\n");
                                                string UserInput = Console.ReadLine();
                                                IsValidAmount = double.TryParse(UserInput, out amount);
                                                if (!IsValidAmount)
                                                {
                                                    Console.WriteLine("Invalid amount, Please enter valid amount\n");
                                                }
                                            } while (!IsValidAmount);
                                            Console.WriteLine(SelectedAccount.Withdraw(amount));
                                        }
                                        break;
                                    case 4:
                                        if (LoggedInUser.Accounts.Count == 0)
                                        {
                                            Console.WriteLine("Create a bank account first");
                                            goto case 6;
                                        }
                                        else
                                        {
                                            Account SelectedAccount = LoggedInUser.Accounts.Count == 1 ? LoggedInUser.Accounts[0] : LoggedInUser.SelectAccount();
                                            bool IsValidAmount = false;
                                            bool IsValidAccount = false;
                                            bool IsContinue = true;
                                            double amount;
                                            Account RecipientAccount = null;

                                            do
                                            {
                                                Console.WriteLine("Enter amount you want to transfer\n");
                                                string UserInput = Console.ReadLine();
                                                IsValidAmount = double.TryParse(UserInput, out amount);
                                                if (!IsValidAmount)
                                                {
                                                    Console.WriteLine("Invalid amount, Please enter valid amount\n");
                                                }
                                            } while (!IsValidAmount);

                                            do
                                            {
                                                Console.WriteLine("Enter recipient account number\n");
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
                                        }
                                        break;
                                    case 5:
                                        if (LoggedInUser.Accounts.Count == 0)
                                        {
                                            Console.WriteLine("Create a bank account first\n");
                                            goto case 6;
                                        }
                                        else
                                        {
                                            Account SelectedAccount = LoggedInUser.Accounts.Count == 1 ? LoggedInUser.Accounts[0] : LoggedInUser.SelectAccount();
                                            SelectedAccount.GetAccountStatement();
                                        }
                                        break;
                                    case 6:
                                        LoggedInUser.CreateAccount();
                                        break;
                                    default:
                                        Console.WriteLine($"{LoggedInUser.Name} thanks for banking with us\n");
                                        LoggedInUser = null;
                                        IsCustomerSessionOn = false;
                                        break;
                                }
                            } while (IsCustomerSessionOn);
                        }
                    }
                }
                else
                {
                    IsCustomerSessionOn = true;
                }
            } while (!IsCustomerSessionOn);
        }
    }
}
