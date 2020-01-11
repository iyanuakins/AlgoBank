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
        private List<Account> _accounts = null;
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
        internal List<Account> Accounts { get => _accounts; set => _accounts = value; }
        public bool IsAdmin { get => _IsAdmin; set => _IsAdmin = value; }

        public Account SelectAccount()
        {
            if (Accounts == null)
            {
                throw new Exception("Please create account first");
            }
            //else if (Accounts.Count == 1)
            //{
            //    return Accounts[0];
            //}
            else
            {
                bool IsValid = false;
                do
                {
                    StringBuilder AccountNumbers = new StringBuilder();
                    int i = 0;
                    foreach (Account account in Accounts)
                    {
                        string line = $"Enter {++i} to select {account}";
                        AccountNumbers.AppendLine(line);
                    }
                    Console.Write("Selected Account: ");
                    string UserInput = Console.ReadLine();
                    int SelectedOption;
                    IsValid = int.TryParse(UserInput, out SelectedOption);
                    try
                    {
                        if (IsValid)
                        {
                            Account SelectedAccount = Accounts[SelectedOption - 1];
                            return SelectedAccount;
                        }
                        else
                        {
                            IsValid = false;
                            throw new Exception("Please enter correct input");
                        }
                    }
                    catch (Exception)
                    {
                        IsValid = false;
                        throw new Exception("Please enter correct input");
                    }

                } while (!IsValid);
            }
        }
    }
}
