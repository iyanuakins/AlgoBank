using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoBank
{
    class Transaction
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
        public static int TransactionCounter = 10000107;

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
            Type = type;
            Amount = amount;
            Currency = currency;
            this.SourceAccount = SourceAccount;
            this.SourceAccountType = SourceAccountType;
            this.DestinationAccount = DestinationAccount;
            this.DestinationAccountType = DestinationAccountType;
            Sender = sender;
            Receiver = receiver;

            string IdPrefix = type == "deposit" ? "DP" :
                                                type == "withdrawal" ? "WD" : "TR";
            Id = $"{IdPrefix}-1{++TransactionCounter}";
        }

        public string Type { get => _type; set => _type = value; }
        public double Amount { get => _amount; set => _amount = value; }
        public string Currency { get => _currency; set => _currency = value; }
        public DateTime DateCreated { get => _DateCreated; }
        public string SourceAccount { get => _SourceAccount; set => _SourceAccount = value; }
        public string SourceAccountType { get => _SourceAccountType; set => _SourceAccountType = value; }
        public string DestinationAccount { get => _DestinationAccount; set => _DestinationAccount = value; }
        public string DestinationAccountType { get => _DestinationAccountType; set => _DestinationAccountType = value; }
        public string Sender { get => _sender; set => _sender = value; }
        public string Receiver { get => _receiver; set => _receiver = value; }
        public string Id { get => _Id; set => _Id = value; }
    }
}
