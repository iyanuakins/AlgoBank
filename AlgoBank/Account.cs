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
        private string _OwnerName = "";
        private List<Transaction> _transactions = null;
        private int _MinimumBalance = 0;
        public static int AccountPrefix = 10;
        public static double USDToNaira = 362.5;
        public static double EURToNaira = 403.14;
        public static double GBPToNaira = 473.5;

        public string Type { get => _type; }
        public string Number { get => _number; }
        public string Currency { get => _currency; }
        public double Balance { get => _balance; set => _balance = value; }
        public DateTime DateCreated { get => _DateCreated; }
        public int Owner { get => _owner; }
        public string OwnerName { get => _OwnerName; set => _OwnerName = value; }
        internal List<Transaction> Transactions { get => _transactions; set => _transactions.AddRange(value); }
        public int MinimumBalance { get => _MinimumBalance; }

        public Account(int owner, string OwnerName)
        {
            _owner = owner;
            _OwnerName = OwnerName;
            string[] options = SelectOptions();
            _type = options[0];
            _currency = options[1];
            _MinimumBalance = Convert.ToInt32(options[2]);
            _number = GenerateAccount();
        }

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
            if (_type == "domiciliary")
            {
                rate = _currency == "USD" ? BankLegder.USDToNaira :
                                    _currency == "EUR" ? BankLegder.EURToNaira : BankLegder.GBPToNaira;
                amount *= rate;
            }
            return $"{_currency}{(_balance / rate)}";
        }

        public object deposit(double amount, string depositor = "self")
        {

            if (amount > 0)
            {
                double rate = 1;
                if (_type == "domiciliary")
                {
                    rate = _currency == "USD" ? BankLegder.USDToNaira :
                                        _currency == "EUR" ? BankLegder.EURToNaira : BankLegder.GBPToNaira;
                    amount *= rate;
                }
                _balance += amount;
                if (depositor == "self")
                {
                    depositor = _OwnerName;
                }
                Transaction TransactionDetails = new Transaction("deposit", (amount / rate), _currency, _number, _type, "", "", depositor, _OwnerName);
                _transactions.Add(TransactionDetails);
                BankLedger.transactions.Add(TransactionDetails);
                return new
                {
                    status = true,
                    message = $"Deposit of {_currency}{amount / rate} is successful.\n New balance is: {_currency}{(_balance / rate)}"
                };
            }

            return new
            {
                status = false,
                message = $"Mininum amount to deposit is {_currency}1"
            };
        }

        public object withdraw(double amount)
        {
            if (amount > 0)
            {
                double rate = 1;
                if (_type == "domiciliary")
                {
                    rate = _currency == "USD" ? BankLegder.USDToNaira :
                                        _currency == "EUR" ? BankLegder.EURToNaira : BankLegder.GBPToNaira;
                    amount *= rate;
                }

                if (_balance - _MinimumBalance - amount >= 0)
                {
                    _balance -= amount;
                    Transaction TransactionDetails = new Transaction("withdrawal", (amount / rate), _currency, "", "", _number, _type, "", _OwnerName);
                    _transactions.Add(TransactionDetails);
                    BankLedger.transactions.Add(TransactionDetails);
                    return new
                    {
                        status = true,
                        message = $"Withdrawal of {_currency}{amount / rate} is successful.\n New balance is: {_currency}{(_balance / rate)}"
                    };
                }

                if (_balance < _MinimumBalance)
                {
                    return new
                    {
                        status = false,
                        message = $"Your account balance is below minimum balance of {_currency}{_MinimumBalance}"
                    };
                }

                return new
                {
                    status = false,
                    message = $"Insufficient balance your withdrawable balance is: {_currency}{(_balance - _MinimumBalance / rate)}"
                };
            }

            return new
            {
                status = false,
                message = $"You cannot withdraw below {_currency}1"
            };
        }

        public object transfer(double amount, string DestinationAccount)
        {
            if (amount > 0)
            {
                Account DestinationAccountDetails = null;
                foreach (Account account in BankLedger.AllAccounts)
                {
                    if (account.)
                }
                double rate = 1;
                if (_type == "domiciliary")
                {
                    rate = _currency == "USD" ? BankLegder.USDToNaira :
                                _currency == "EUR" ? BankLegder.EURToNaira : BankLegder.GBPToNaira;
                    amount *= rate;
                }

                if (_balance - _MinimumBalance - amount >= 0)
                {
                    _balance -= amount;
                    Transaction TransactionDetails = new Transaction("transfer", (amount / rate), _currency, "", "", _number, _type);
                    _transactions.Add(TransactionDetails);
                    BankLedger.transactions.Add(TransactionDetails);
                    return new
                    {
                        status = true,
                        message = $"Withdrawal of {_currency}{amount / rate} is successful.\n New balance is: {_currency}{(_balance / rate)}"
                    };
                }

                if (_balance < _MinimumBalance)
                {
                    return new
                    {
                        status = false,
                        message = $"Your account balance is below minimum balance of {_currency}{_MinimumBalance}"
                    };
                }

                return new
                {
                    status = false,
                    message = $"Insufficient balance your withdrawable balance is: {_currency}{(_balance - _MinimumBalance / rate)}"
                };
            }

            return new
            {
                status = false,
                message = $"You cannot transfer below {_currency}1"
            };
        }
    }
}
