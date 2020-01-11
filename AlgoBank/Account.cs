using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoBank
{
    class Account
    {
        private string _type;
        private string _number;
        private string _currency;
        private double _balance = 0.0;
        private DateTime _DateCreated = DateTime.Now;
        private int _owner;
        private string _OwnerName;
        private List<Transaction> _transactions = null;
        private int _MinimumBalance = 0;
        public static int AccountPrefix = 10;
        public static double USDToNaira = 362.5;
        public static double EURToNaira = 403.14;
        public static double GBPToNaira = 473.5;

        public Account(int owner, string OwnerName)
        {
            Owner = owner;
            this.OwnerName = OwnerName;
            string[] options = SelectOptions();
            Type = options[0];
            Currency = options[1];
            MinimumBalance = Convert.ToInt32(options[2]);
            Number = GenerateAccount();
        }

        public string Type { get => _type; set => _type = value; }
        public string Number { get => _number; set => _number = value; }
        public string Currency { get => _currency; set => _currency = value; }
        public double Balance { get => _balance; set => _balance = value; }
        public DateTime DateCreated { get => _DateCreated; }
        public int Owner { get => _owner; set => _owner = value; }
        public string OwnerName { get => _OwnerName; set => _OwnerName = value; }
        internal List<Transaction> Transactions { get => _transactions; set => _transactions.AddRange(value); }
        public int MinimumBalance { get => _MinimumBalance; set => _MinimumBalance = value; }

        public string[] SelectOptions()
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
                        if (SelectedOption == 3)
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

        public string GenerateAccount()
        {
            Random random = new Random();
            int RandomPartOne = random.Next(10, 99);
            int RandomPartTwo = random.Next(10, 99);
            int RandomPartThree = random.Next(10, 99);
            int prefix = (AccountPrefix % 99) < 10 ? AccountPrefix % 99 + 10 : AccountPrefix % 99;
            return $"00{prefix}{RandomPartOne}{RandomPartTwo}{RandomPartThree}";
        }

        public string GetBalance()
        {
            double rate = 1;
            if (Type == "domiciliary")
            {
                rate = Currency == "USD" ? BankLegder.USDToNaira :
                                    Currency == "EUR" ? BankLegder.EURToNaira : BankLegder.GBPToNaira;
                amount *= rate;
            }
            return $"{Currency}{(Balance / rate)}";
        }

        public object Deposit(double amount, string depositor = "self")
        {

            if (amount > 0)
            {
                double rate = 1;
                if (Type == "domiciliary")
                {
                    rate = Currency == "USD" ? BankLegder.USDToNaira :
                                        Currency == "EUR" ? BankLegder.EURToNaira : BankLegder.GBPToNaira;
                    amount *= rate;
                }
                Balance += amount;
                if (depositor == "self")
                {
                    depositor = OwnerName;
                }
                Transaction TransactionDetails = new Transaction("deposit", (amount / rate), Currency, Number, Type, "", "", depositor, OwnerName);
                Transactions.Add(TransactionDetails);
                BankLedger.transactions.Add(TransactionDetails);
                return new
                {
                    status = true,
                    message = $"Deposit of {Currency}{amount / rate} is successful.\n New balance is: {Currency}{(Balance / rate)}"
                };
            }

            return new
            {
                status = false,
                message = $"Mininum amount to deposit is {Currency}1"
            };
        }

        public object Withdraw(double amount)
        {
            if (amount > 0)
            {
                double rate = 1;
                if (Type == "domiciliary")
                {
                    rate = Currency == "USD" ? BankLegder.USDToNaira :
                                        Currency == "EUR" ? BankLegder.EURToNaira : BankLegder.GBPToNaira;
                    amount *= rate;
                }

                if (Balance - MinimumBalance - amount >= 0)
                {
                    Balance -= amount;
                    Transaction TransactionDetails = new Transaction("withdrawal", (amount / rate), Currency, "", "", Number, Type, "", OwnerName);
                    Transactions.Add(TransactionDetails);
                    BankLedger.transactions.Add(TransactionDetails);
                    return new
                    {
                        status = true,
                        message = $"Withdrawal of {Currency}{amount / rate} is successful.\n New balance is: {Currency}{(Balance / rate)}"
                    };
                }

                if (Balance < MinimumBalance)
                {
                    return new
                    {
                        status = false,
                        message = $"Your account balance ({Currency}{(Balance / rate)}) is below minimum balance of {Currency}{MinimumBalance}"
                    };
                }

                return new
                {
                    status = false,
                    message = $"Insufficient balance your withdrawable balance is: {Currency}{(Balance - MinimumBalance / rate)}"
                };
            }

            return new
            {
                status = false,
                message = $"You cannot withdraw below {Currency}1"
            };
        }

        public object Transfer(double amount, Account DestinationAccount) 
        {   
            if (amount > 0)
            {
                double rate = 1;
                if (Type == "domiciliary")
                {
                    rate = Currency == "USD" ? BankLegder.USDToNaira :
                                Currency == "EUR" ? BankLegder.EURToNaira : BankLegder.GBPToNaira;
                    amount *= rate;
                }

                if (Balance - MinimumBalance - amount >= 0)
                {
                    //Debit the sender
                    Balance -= amount;
                    Transaction TransactionDetails = new Transaction("transfer", (amount / rate), Currency, Number, Type, DestinationAccount.Number, DestinationAccount.Type, OwnerName, DestinationAccount.OwnerName);
                    Transactions.Add(TransactionDetails);
                    BankLedger.transactions.Add(TransactionDetails);
                    
                    //Credit the receiver
                    DestinationAccount.Balance += amount;
                    double ReceiverRate = 1;
                    if (DestinationAccount.Type == "domiciliary")
                    {
                        ReceiverRate = DestinationAccount.Currency == "USD" ? BankLegder.USDToNaira :
                                            DestinationAccount.Currency == "EUR" ? BankLegder.EURToNaira : BankLegder.GBPToNaira;
                        amount *= rate;
                    }
                    Transaction TransactionDetails2 = new Transaction("transfer", (amount / ReceiverRate), DestinationAccount.Currency, Number, Type, DestinationAccount.Number, DestinationAccount.Type, OwnerName, DestinationAccount.OwnerName);
                    DestinationAccount.Transactions.Add(TransactionDetails2);
                    BankLedger.transactions.Add(TransactionDetails2);
                    return new
                    {
                        status = true,
                        message = $"Transfer of {Currency}{amount / rate} to {DestinationAccount.OwnerName}:({DestinationAccount.Number}) was successful.\n New balance is: {Currency}{(Balance / rate)}"
                    };
                }

                if (Balance < MinimumBalance)
                {
                    return new
                    {
                        status = false,
                        message = $"Your account balance ({Currency}{(Balance / rate)}) is below minimum balance of {Currency}{MinimumBalance}"
                    };
                }

                return new
                {
                    status = false,
                    message = $"Insufficient balance your transferable balance is: {Currency}{(Balance - MinimumBalance / rate)}"
                };
            }

            return new
            {
                status = false,
                message = $"You cannot transfer below {Currency}1"
            };
        }
    }
}
