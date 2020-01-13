using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoBank
{
    class Customer
    {
        private int _id;
        private string _name;
        private string _email;
        private string _password;
        private bool _IsAdmin;
        private List<Account> _accounts = new List<Account>();
        public static int CustomerCount = 0;
        public Customer(string name, string email, string password, bool isAdmin)
        {
            Name = name;
            Email = email;
            Password = password;
            Id = ++CustomerCount;
            IsAdmin = isAdmin;
        }

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Email { get => _email; set => _email = value; }
        public string Password { get => _password; set => _password = value; }
        internal List<Account> Accounts { get => _accounts; set => _accounts.AddRange(value); }
        public bool IsAdmin { get => _IsAdmin; set => _IsAdmin = value; }
        public Account SelectAccount()
        {
            bool IsValid = false;
            do
            {
                StringBuilder AccountNumbers = new StringBuilder();
                int i = 0;
                foreach (Account account in Accounts)
                {
                    string line = $"Enter {++i} to select your {account.Type} with account number of {account.Number}";
                    AccountNumbers.AppendLine(line);
                }
                Console.WriteLine("Please select account: ");
                Console.Write(AccountNumbers);
                string UserInput = Console.ReadLine();
                int SelectedOption;
                IsValid = int.TryParse(UserInput, out SelectedOption);
                if (IsValid && 1 <= SelectedOption && SelectedOption <= Accounts.Count)
                {
                    Account SelectedAccount = Accounts[SelectedOption - 1];
                    return SelectedAccount;
                }
                else
                {
                    IsValid = false;
                    Console.WriteLine("\nPlease enter correct input\n");
                }

            } while (!IsValid);

            return null;
        }
        public void CreateAccount()
        {
            Account NewAccount = new Account(Id, Name);
            List<Account> AccountList = new List<Account>
            {
                NewAccount
            };
            Accounts = AccountList;
            BankLedger.AllAccounts.Add(NewAccount);
            string LastMessage = NewAccount.MinimumBalance == 0 ? "" : $"Your Minimum account balance is {NewAccount.Currency}{NewAccount.MinimumBalance}";
            Console.WriteLine();
            Console.WriteLine("Below are the details of newly created account:\n" +
                                $"Account name: {NewAccount.OwnerName}\n" +
                                $"Account number: {NewAccount.Number}\n" +
                                $"Account type: {NewAccount.Type}\n" +
                                $"Account currency: {NewAccount.Currency}\n" +
                                $"{LastMessage}");
            Console.WriteLine();
        }
    }
}
