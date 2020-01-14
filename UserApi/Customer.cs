﻿using System;
using System.Collections.Generic;
using System.Text;
using AccountApi;

namespace UserApi
{
    public class Customer : User
    {
        private List<Account> _accounts = new List<Account>();
        public static int CustomerCount = 100;
        public Customer(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
            Id = ++CustomerCount;
        }

        internal List<Account> Accounts { get => _accounts; set => _accounts.AddRange(value); }
        public static int TotalCustomer { get; set; } = 0;

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
            Account.AllAccounts.Add(NewAccount);
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