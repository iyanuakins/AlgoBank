using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoBank
{
    class BankLedger
    {
        public static List<Customer> Customers = null;
        public static List<Account> AllAccounts = new List<Account>();
        public static List<Transaction> AllTransactions = new List<Transaction>();
        public static double TotalAmountInBank = 0.0;
        public static int TotalCustomer = 0;
        public static int TotalSavingsAccount = 0;
        public static int TotalCurrentAccount = 0;
        public static int TotalDomiciliaryAccount = 0;
        public static double USDToNaira = 362.5;
        public static double EURToNaira = 403.14;
        public static double GBPToNaira = 473.5;


        public static Customer GetCustomerByID(int id)
        {
            foreach (Customer customer in Customers)
            {
                if (customer.Id == id)
                {
                    return customer;
                }
            }
            return null;
        }
        public static List<Customer> GetAllCustomers(Customer customer)
        {
            if (customer.IsAdmin)
            {
                return Customers;
            }

            throw new Exception("Unauthorize access");
        }
        public static List<Account> GetAllAccounts(Customer customer)
        {
            if (customer.IsAdmin)
            {
                return AllAccounts;
            }

            throw new Exception("Unauthorize access");
        }
        public static List<Account> GetAllAccountsByType(Customer customer, string type)
        {
            if (customer.IsAdmin)
            {
                List<Account> AllAccount = new List<Account>();
                foreach (Account account in AllAccounts)
                {
                    if (account.Type == type)
                    {
                        AllAccount.Add(account);
                    }
                }

                return AllAccount;
            }

            throw new Exception("Unauthorize access");
        }
        public static Account GetAccountByNumber(Customer customer, string number)
        {
            if (customer.IsAdmin)
            {
                foreach (Account account in AllAccounts)
                {
                    if (account.Number == number)
                    {
                        return account;
                    }
                }

                return null;
            }

            throw new Exception("Unauthorize access");
        }
        public static List<Transaction> GetAllTransactions(Customer customer)
        {
            if (customer.IsAdmin)
            {
                return AllTransactions;
            }

            throw new Exception("Unauthorize access");
        }
        public static List<Transaction> GetAllTransactionsByType(Customer customer, string type)
        {
            if (customer.IsAdmin)
            {
                List<Transaction> AllTransaction = new List<Transaction>();
                foreach (Transaction transaction in AllTransactions)
                {
                    if (transaction.Type == type)
                    {
                        AllTransaction.Add(transaction);
                    }
                }

                return AllTransaction;
            }

            throw new Exception("Unauthorize access");
        }
        public static Transaction GetTransactionByID(Customer customer, string id)
        {
            if (customer.IsAdmin)
            {
                foreach (Transaction transaction in AllTransactions)
                {
                    if (transaction.Id == id)
                    {
                        return transaction;
                    }
                }
                return null;
            }

            throw new Exception("Unauthorize access");
        }
        public static object GetTotalAccountCount(Customer customer)
        {
            return new
            {
                SavingsAccount = TotalSavingsAccount,
                CurrentAccount = TotalCurrentAccount,
                DomiciliaryAccount = TotalDomiciliaryAccount,
                TotalAccounts = TotalSavingsAccount + TotalDomiciliaryAccount + TotalCurrentAccount
            };
        }
    }
}
