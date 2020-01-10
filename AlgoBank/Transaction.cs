using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoBank
{
    class Transaction
    {
        private string _type;
        private double _amount;
        private DateTime _DateCreated = new DateTime();
        private string _SourceAccount;
        private string _SourceAccountType;
        private string _DestinationAccount;
        private string _DestinationAccountType;
        private string _TransactionID;
        public static int TransactionCounter = 10260187;

        public Transaction(string __type, double __amount, string __SourceAccount, string __SourceAccountType, string __DestinationAccount, string __DestinationAccountType)
        {
            _type = __type;
            _amount = __amount;
            _SourceAccount = __SourceAccount;
            _SourceAccountType = __SourceAccountType;
            _DestinationAccount = __DestinationAccount;
            _DestinationAccountType = __DestinationAccount;

            string TransactionIDPrefix = __type == "deposit" ? "DP" :
                                                __type == "withdrawal" ? "WD" : "TR";
            _TransactionID = $"{TransactionIDPrefix}-{++TransactionCounter}";
        }

        public DateTime DateCreated { get => DateCreated;}
        public string Type { get => _type; }
        public double Amount { get => _amount; }
        public string SourceAccount { get => _SourceAccount; }
        public string SourceAccountType { get => _SourceAccountType; }
        public string DestinationAccount { get => _DestinationAccount; }
        public string DestinationAccountType { get => _DestinationAccountType; }
        public string TransactionID { get => _TransactionID; }
    }
}
