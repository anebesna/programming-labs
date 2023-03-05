using System;
using static System.Console;

namespace chain
{
    class ATM
    {
        static void Main(string[] args)
        {
            string result;
            UAH500Handler uah500 = new UAH500Handler();
            UAH200Handler uah200 = new UAH200Handler();
            UAH100Handler uah100 = new UAH100Handler();
            UAH50Handler uah50 = new UAH50Handler();
            uah500.SetSuccessor(uah200);
            uah200.SetSuccessor(uah100);
            uah100.SetSuccessor(uah50);
            while (true)
            {
                Write("Enter the amount you want to withdraw: ");
                if (!int.TryParse(ReadLine(), out int amount)) Environment.Exit(0);
                if (amount % 10 != 0)
                {
                    WriteLine("The amount must be a multiple of 10.");
                }
                else
                {
                    try
                    {
                        result = uah500.withdraw(amount);
                        WriteLine(result);
                    }
                    catch(CombinationException e) { WriteLine(e.Message); }
                }
            }
        }
    }
}
