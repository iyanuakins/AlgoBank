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
        private string _TransactionID;
        public static int TransactionCounter = 10260187;

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

            string TransactionIDPrefix = type == "deposit" ? "DP" :
                                                type == "withdrawal" ? "WD" : "TR";
            _TransactionID = $"{TransactionIDPrefix}-1{++TransactionCounter}";
        }

        public DateTime DateCreated { get => _DateCreated; }
        public string Type { get => _type; }
        public double Amount { get => _amount; }
        public string SourceAccount { get => _SourceAccount; }
        public string SourceAccountType { get => _SourceAccountType; }
        public string DestinationAccount { get => _DestinationAccount; }
        public string DestinationAccountType { get => _DestinationAccountType; }
        public string TransactionID { get => _TransactionID; }
    }
}
