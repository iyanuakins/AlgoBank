using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoBank
{
    class Account
    {
        private string type;
        private string number;
        private string currency;
        private double balance = 0.0;
        private DateTime DateCreated = new DateTime();
        private int owner;
        private List<int> transactions = null;
        private int MinimumBalance = 0;
        public static int AccountPartOne = 10;

        public Account(int _owner)
        {
            owner = _owner;
            string[] options = SelectType();
            type = options[0];
            currency = options[1];
            MinimumBalance = Convert.ToInt32(options[2]);
        }

        public string[] SelectType()
        {
            bool IsValid = false;
            string[] options = new string[3];
            options[1] = "NGN";
            options[2] = "0";
            do
            {
                Console.WriteLine("Please select account type");
                Console.WriteLine("Enter 1 to select \"Savings\"\nEnter 2 to select \"Current\"\nEnter 3 to select \"Domiciliary\"");
                string FirstUserInput = Console.ReadLine();
                int SelectedOption;
                IsValid = int.TryParse(FirstUserInput, out SelectedOption);

                try
                {
                    if (IsValid && (1 <= SelectedOption && SelectedOption <= 3))
                    {
                        if(SelectedOption == 3)
                        {
                            bool IsSecondInputValid = false;
                            do
                            {
                                Console.WriteLine("Please select domiciliary account currency");
                                Console.WriteLine("Enter 1 to select \"Dollar\"\nEnter 2 to select \"Euros\"\nEnter 3 to select \"Pound\"");
                                string SecondUserInput = Console.ReadLine();
                                int SecondSelectedOption;
                                IsSecondInputValid = int.TryParse(SecondUserInput, out SecondSelectedOption);
                                if (IsSecondInputValid && (1 <= SecondSelectedOption && SecondSelectedOption <= 3))
                                {
                                    options[0] = "domiciliary";
                                    options[1] = SecondSelectedOption == 1 ? "USD" :
                                                    SecondSelectedOption == 2 ? "EUR" : "GBP";
                                    return options;
                                }
                                else
                                {
                                    IsSecondInputValid = false;
                                    throw new Exception("Please enter correct input");
                                }
                            } while (!IsSecondInputValid);
                        }

                        options[0] = SelectedOption == 1 ? "savings" : "current";
                        options[2] = SelectedOption == 1 ? "1000" : "0";
                        return options;
                    }
                    else
                    {
                        IsValid = false; 
                        throw new Exception("Please enter correct input");
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    IsValid = false;
                    throw new Exception("Please enter correct input");
                }

            } while (!IsValid);
        }

        public string GetBalance()
        {
            return $"{currency}{balance}";
        }

        public object deposit(double amount)
        {
            if (amount > 0)
            {
                balance += amount;
                return new
                {
                    status = true,
                    message = $"Deposit of {currency}{amount} is successful.\n New balance is: {currency}{balance}"
                };
            }

            return new
            {
                status = false,
                message = $"Mininum amount to deposit is {currency}1"
            };
        }
        public object withdraw(double amount)
        {
            if (amount > 0)
            {
                if (balance - MinimumBalance - amount >= 0)
                {
                    balance -= amount;
                    return new
                    {
                        status = true,
                        message = $"Withdrawal of {currency}{amount} is successful.\n New balance is: {currency}{balance}"
                    };
                }
                return new
                {
                    status = false,
                    message = $"Insufficient balance your withdrawable balance is: {currency}{(balance - MinimumBalance)}"
                };
            }

            return new
            {
                status = false,
                message = $"You cannot withdraw below {currency}1"
            };
        }


    }
}
