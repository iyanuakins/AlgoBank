using System;

namespace AlgoBank
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Algo Bank\n");
            bool IsSessionOn = false;
            do
            {
                int FirstSelectedOption;
                bool IsValid = false;
                do
                {
                    Console.WriteLine("Enter 1 register\nEnter 2 to login");
                    string UserInput = Console.ReadLine();
                    IsValid = int.TryParse(UserInput, out FirstSelectedOption);
                    if (!(IsValid && (FirstSelectedOption == 1 || FirstSelectedOption == 2)))
                    {
                        IsValid = false;
                        Console.WriteLine("Invalid option, Please select valid option");
                    }
                } while (!IsValid);

                if (FirstSelectedOption == 1)
                {
                    bool IsSuccess = BankLedger.RegisterCustomer();
                    if (IsSuccess)
                    {
                        Console.WriteLine("Registration successful, you can now login\n");
                    }
                }
            } while (!IsSessionOn);
        }
    }
}
