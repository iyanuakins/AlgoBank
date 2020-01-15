using System;
using System.Collections.Generic;
using System.Text;
using TransactionApi;

namespace AccountApi
{
    public class Account
    {
        private string _type;
        private string _number;
        private string _currency;
        private double _balance = 0.0;
        private DateTime _DateCreated = DateTime.Now;
        private int _Owner;
        private string _OwnerName;
        private static double totalAmountInBank = 0.0;
        private static int totalSavingsAccount = 0;
        private static int totalCurrentAccount = 0;
        private static int totalDomiciliaryAccount = 0;
        private static double _USDToNaira = 362.5;
        private static double _EURToNaira = 403.14;
        private static double _GBPToNaira = 473.5;
        private static List<Account> allAccounts = new List<Account>();
        private List<Transaction> _transactions = new List<Transaction>();
        private int _MinimumBalance = 0;
        public static int AccountPrefix = 10;

        public Account(int owner, string OwnerName)
        {
            Owner = owner;
            this.OwnerName = OwnerName;
            string[] options = SelectOptions();
            Type = options[0];
            Currency = options[1];
            MinimumBalance = Convert.ToInt32(options[2]);
            Number = GenerateAccountNumber();
        }
        public string Type { get => _type; set => _type = value; }
        public string Number { get => _number; set => _number = value; }
        public string Currency { get => _currency; set => _currency = value; }
        public double Balance { get => _balance; set => _balance = value; }
        public DateTime DateCreated { get => _DateCreated; }
        public int Owner { get => _Owner; set => _Owner = value; }
        public string OwnerName { get => _OwnerName; set => _OwnerName = value; }
        internal List<Transaction> Transactions { get => _transactions; set => _transactions.AddRange(value); }
        public int MinimumBalance { get => _MinimumBalance; set => _MinimumBalance = value; }
        public static double TotalAmountInBank { get => totalAmountInBank; set => totalAmountInBank = value; }
        public static int TotalSavingsAccount { get => totalSavingsAccount; set => totalSavingsAccount = value; }
        public static int TotalCurrentAccount { get => totalCurrentAccount; set => totalCurrentAccount = value; }
        public static int TotalDomiciliaryAccount { get => totalDomiciliaryAccount; set => totalDomiciliaryAccount = value; }
        public static double USDToNaira { get => _USDToNaira; }
        public static double EURToNaira { get => _EURToNaira; }
        public static double GBPToNaira { get => _GBPToNaira; }
        public static List<Account> AllAccounts { get => allAccounts; }

        public string[] SelectOptions()
        {
            bool IsValid = false;
            string[] options = new string[3];
            options[1] = "NGN";
            options[2] = "0";
            do
            {
                Console.WriteLine("1)  =>  Savings\n" +
                                  "2)  =>  Current\n" +
                                  "3)  =>  Domicilary");
                Console.Write("Select account type: ");
                string FirstUserInput = Console.ReadLine();
                int SelectedOption;
                IsValid = int.TryParse(FirstUserInput, out SelectedOption);

                if (IsValid && (1 <= SelectedOption && SelectedOption <= 3))
                {
                    if (SelectedOption == 3)
                    {
                        bool IsSecondInputValid = false;
                        do
                        {
                            Console.WriteLine();
                            Console.WriteLine("1)  =>  Dollar\n" +
                                              "2)  =>  Euros\n" +
                                              "3)  =>  Pound");
                            Console.Write("Select account currency: ");
                            string SecondUserInput = Console.ReadLine();
                            int SecondSelectedOption;
                            IsSecondInputValid = int.TryParse(SecondUserInput, out SecondSelectedOption);
                            if (IsSecondInputValid && (1 <= SecondSelectedOption && SecondSelectedOption <= 3))
                            {
                                options[0] = "domiciliary";
                                if (SecondSelectedOption == 1)
                                {
                                    options[1] = "USD";
                                }
                                else if (SecondSelectedOption == 2)
                                {
                                    options[1] = "EUR";
                                }
                                else
                                {
                                    options[1] = "GBP";
                                }
                                TotalDomiciliaryAccount++;
                                return options;
                            }
                            else
                            {
                                IsSecondInputValid = false;
                                Console.WriteLine("Please enter correct input\n");
                            }
                        } while (!IsSecondInputValid);
                    }

                    if (SelectedOption == 1)
                    {
                        TotalSavingsAccount++;
                    }
                    else
                    {
                        TotalCurrentAccount++;
                    }
                    options[0] = SelectedOption == 1 ? "savings" : "current";
                    options[2] = SelectedOption == 1 ? "1000" : "0";
                    return options;
                }
                else
                {
                    IsValid = false;
                    Console.WriteLine("Please enter correct input\n");
                }
            } while (!IsValid);

            return options;
        }

        public string GenerateAccountNumber()
        {
            Random random = new Random();
            int RandomPartOne = random.Next(10, 99);
            int RandomPartTwo = random.Next(10, 99);
            int RandomPartThree = random.Next(10, 99);
            int prefix = (AccountPrefix % 99) < 10 ? AccountPrefix % 99 + 10 : AccountPrefix % 99;
            AccountPrefix++;
            return $"10{prefix}{RandomPartOne}{RandomPartTwo}{RandomPartThree}";
        }

        public string GetBalance()
        {
            double rate = 1;
            if (Type == "domiciliary")
            {
                if (Currency == "USD")
                {
                    rate = USDToNaira;
                }
                else if (Currency == "EUR")
                {
                    rate = EURToNaira;
                }
                else
                {
                    rate = GBPToNaira;
                }
            }
            return $"{Currency}{(Balance / rate)}";
        }

        public string Deposit(double amount, string depositor = "self")
        {

            if (amount > 0)
            {
                double rate = 1;
                if (Type == "domiciliary")
                {
                    if (Currency == "USD")
                    {
                        rate = USDToNaira;
                    }
                    else if (Currency == "EUR")
                    {
                        rate = EURToNaira;
                    }
                    else
                    {
                        rate = GBPToNaira;
                    }
                    amount *= rate;
                }
                Balance += amount;
                TotalAmountInBank += amount;
                if (depositor == "self")
                {
                    depositor = OwnerName;
                }
                Transaction TransactionDetails = new Transaction("deposit", (amount / rate), Currency, Number, Type, "", "", depositor, OwnerName);
                Transactions.Add(TransactionDetails);
                Transaction.AllTransactions.Add(TransactionDetails);
                return $"\nDeposit of {Currency}{amount / rate} is successful.\nNew balance is: {Currency}{(Balance / rate)}\n";
            }
            return $"\nMininum amount to deposit is {Currency}1\n";
        }

        public string Withdraw(double amount)
        {
            if (amount > 0)
            {
                double rate = 1;
                if (Type == "domiciliary")
                {
                    if (Currency == "USD")
                    {
                        rate = USDToNaira;
                    }
                    else if (Currency == "EUR")
                    {
                        rate = EURToNaira;
                    }
                    else
                    {
                        rate = GBPToNaira;
                    }
                    amount *= rate;
                }

                if (Balance - MinimumBalance - amount >= 0)
                {
                    Balance -= amount;
                    TotalAmountInBank -= amount;
                    Transaction TransactionDetails = new Transaction("withdrawal", (amount / rate), Currency, "", "", Number, Type, "", OwnerName);
                    Transactions.Add(TransactionDetails);
                    Transaction.AllTransactions.Add(TransactionDetails);
                    return $"\nWithdrawal of {Currency}{amount / rate} is successful.\nNew balance is: {Currency}{(Balance / rate)}\n";
                }

                if (Balance < MinimumBalance)
                {
                    return $"\nYour account balance ({Currency}{(Balance / rate)}) is below minimum balance of {Currency}{MinimumBalance}\n";
                }

                return $"\nInsufficient balance your withdrawable balance is: {Currency}{((Balance - MinimumBalance) / rate)}\n";
            }

            return $"\nYou cannot withdraw below {Currency}1\n";
        }

        public string Transfer(double amount, Account DestinationAccount)
        {
            if (amount > 0)
            {
                double rate = 1;
                if (Type == "domiciliary")
                {
                    if (Currency == "USD")
                    {
                        rate = USDToNaira;
                    }
                    else if (Currency == "EUR")
                    {
                        rate = EURToNaira;
                    }
                    else
                    {
                        rate = GBPToNaira;
                    }
                    amount *= rate;
                }

                if (Balance - MinimumBalance - amount >= 0)
                {
                    //Debit the sender
                    Balance -= amount;
                    Transaction TransactionDetails = new Transaction("transfer", (amount / rate), Currency, Number, Type, DestinationAccount.Number, DestinationAccount.Type, OwnerName, DestinationAccount.OwnerName);
                    Transactions.Add(TransactionDetails);
                    Transaction.AllTransactions.Add(TransactionDetails);

                    //Credit the receiver
                    DestinationAccount.Balance += amount;
                    double ReceiverRate = 1;
                    if (DestinationAccount.Type == "domiciliary")
                    {
                        if (DestinationAccount.Currency == "USD")
                        {
                            ReceiverRate = USDToNaira;
                        }
                        else if (DestinationAccount.Currency == "EUR")
                        {
                            ReceiverRate = EURToNaira;
                        }
                        else
                        {
                            ReceiverRate = GBPToNaira;
                        }
                        amount *= rate;
                    }
                    Transaction TransactionDetails2 = new Transaction("transfer", (amount / ReceiverRate), DestinationAccount.Currency, Number, Type, DestinationAccount.Number, DestinationAccount.Type, OwnerName, DestinationAccount.OwnerName);
                    DestinationAccount.Transactions.Add(TransactionDetails2);
                    Transaction.AllTransactions.Add(TransactionDetails2);
                    return $"\nTransfer of {Currency}{amount / rate} to {DestinationAccount.OwnerName}:({DestinationAccount.Number}) was successful.\nNew balance is: {Currency}{(Balance / rate)}\n";
                }

                if (Balance < MinimumBalance)
                {
                    return $"\nYour account balance ({Currency}{(Balance / rate)}) is below minimum balance of {Currency}{MinimumBalance}\n";
                }

                return $"\nInsufficient balance your transferable balance is: {Currency}{(Balance - MinimumBalance / rate)}\n";
            }

            return $"\nYou cannot transfer below {Currency}1\n";
        }

        public void GetAccountStatement()
        {
            if (Transactions.Count > 0)
            {
                StringBuilder statement = new StringBuilder();
                statement.AppendLine();
                statement.AppendLine($"Account name: {OwnerName}");
                statement.AppendLine($"Account number: {Number}");
                statement.AppendLine($"Account Type: {Type}");
                statement.AppendLine($"Account currency: {Currency}");
                statement.AppendLine();
                statement.AppendLine("| Transaction ref num | Transaction type | Amount | Sender | Receiver | Transaction Date |");
                foreach (Transaction transaction in Transactions)
                {
                    string date = string.Format("{0: dd-MM-yyyy HH:mm:ss}", transaction.DateCreated);
                    statement.AppendLine($"| {transaction.Id} | {transaction.Type} | {transaction.Amount} | {transaction.Sender} | {transaction.Receiver} | {date} |");
                }
                statement.AppendLine();
                statement.AppendLine($"Generated on {string.Format("{0: dd-MM-yyyy HH:mm}", DateTime.Now)}");
                Console.WriteLine(statement.ToString());
                Console.WriteLine();
            }
            else
            {

                Console.WriteLine("No transaction record on this account");
            }
        }
    }
}
