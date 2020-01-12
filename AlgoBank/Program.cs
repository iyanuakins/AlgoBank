using System;

namespace AlgoBank
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Algo Bank\n");
            bool IsSessionOn = false;
            do
            {
                int FirstSelectedOption;
                bool IsValid = false;
                do
                {
                    Console.WriteLine("Enter 1 register\nEnter 2 to login");
                    string UserInput = Console.ReadLine();
                    IsValid = int.TryParse(UserInput, out FirstSelectedOption);
                    if (!(IsValid && (FirstSelectedOption == 1 || FirstSelectedOption == 2)))
                    {
                        IsValid = false;
                        Console.WriteLine("Invalid option, Please select valid option");
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
                else
                {
                    Customer LoggedInUser = BankLedger.AuthenticateCustomer();
                    if (LoggedInUser != null)
                    {
                        IsSessionOn = true;
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
                                                   "Enter 5 to get accoiunt statement\n"+
                                                   "Enter 6 to a new create account\n"+
                                                   "Enter 7 to log out\n");
                                string UserInputForOperation = Console.ReadLine();
                                IsValidOption = int.TryParse(UserInputForOperation, out SecondSelectedOption);
                                if (!(IsValidOption && (1 <= SecondSelectedOption|| SecondSelectedOption <= 7)))
                                {
                                    IsValidOption = false;
                                    Console.WriteLine("Invalid option, Please select valid option");
                                }
                            } while (IsValidOption);

                            switch (SecondSelectedOption)
                            {
                                case 1:
                                    if (LoggedInUser.Accounts.Count == 0)
                                    {
                                        Console.WriteLine("Create a bank account first");
                                    }
                                    else
                                    {
                                        Account SelectedAccount;
                                        string balance;
                                        if (LoggedInUser.Accounts.Count == 1)
                                        {
                                            SelectedAccount = LoggedInUser.Accounts[0];
                                            balance = SelectedAccount.GetBalance();
                                            Console.WriteLine($"{balance}");
                                        }
                                        else
                                        {
                                            SelectedAccount = LoggedInUser.SelectAccount();
                                            balance = SelectedAccount.GetBalance();
                                            Console.WriteLine($"{balance}");
                                        }
                                    }
                                    break;
                                case 2:
                                    if (LoggedInUser.Accounts.Count == 0)
                                    {
                                        Console.WriteLine("Create a bank account first");
                                        goto case 6;
                                    }
                                    else
                                    {
                                        Account SelectedAccount;
                                        string balance;
                                        if (LoggedInUser.Accounts.Count == 1)
                                        {
                                            SelectedAccount = LoggedInUser.Accounts[0];
                                            balance = SelectedAccount.GetBalance();
                                            Console.WriteLine($"{balance}");
                                        }
                                        else
                                        {
                                            SelectedAccount = LoggedInUser.SelectAccount();
                                            balance = SelectedAccount.GetBalance();
                                            Console.WriteLine($"{balance}");
                                        }
                                    }
                                    break;
                                case 3:
                                    break;
                                case 4:
                                    break;
                                case 5:
                                    break;
                                case 6:
                                    break;
                                default:
                                    break;
                            }
                        } while (IsSessionOn);
                    }
                }
            } while (!IsSessionOn);
        }
    }
}
