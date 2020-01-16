﻿using System;
using System.Collections.Generic;

namespace TransactionApi
{
    public class Transaction
    {
        private string _type;
        private double _amount;
        private string _currency;
        private DateTime _DateCreated = DateTime.Now;
        private string _SourceAccount;
        private string _SourceAccountType;
        private string _DestinationAccount;
        private string _DestinationAccountType;
        private string _sender;
        private string _receiver;
        private string _Id;
        public static int TransactionCounter = 1000107;
        private static List<Transaction> _AllTransactions = new List<Transaction>();

        public Transaction(
            string type,
            double amount,
            string currency,
            string SourceAccount,
            string SourceAccountType,
            string DestinationAccount,
            string DestinationAccountType,
            string sender,
            string receiver
            )
        {
            _type = type;
            _amount = amount;
            _currency = currency;
            _SourceAccount = SourceAccount;
            _SourceAccountType = SourceAccountType;
            _DestinationAccount = DestinationAccount;
            _DestinationAccountType = DestinationAccountType;
            _sender = sender;
            _receiver = receiver;

            string IdPrefix;
            if(type == "deposit")
            {
                IdPrefix = "DP";
            }
            else if (type == "withdrawal")
            {
                IdPrefix = "WD";
            }
            else
            {
                IdPrefix = "TR";
            }
            _Id = $"{IdPrefix}-1{++TransactionCounter}";
        }

        public string Type { get => _type; }
        public double Amount { get => _amount; }
        public string Currency { get => _currency; }
        public DateTime DateCreated { get => _DateCreated; }
        public string SourceAccount { get => _SourceAccount; }
        public string SourceAccountType { get => _SourceAccountType; }
        public string DestinationAccount { get => _DestinationAccount; }
        public string DestinationAccountType { get => _DestinationAccountType; }
        public string Sender { get => _sender; }
        public string Receiver { get => _receiver; }
        public string Id { get => _Id; }
        public static List<Transaction> AllTransactions { get => _AllTransactions; }
    }
}
