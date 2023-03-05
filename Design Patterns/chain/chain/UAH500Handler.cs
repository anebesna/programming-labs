using System;
namespace chain
{
    public class UAH500Handler : MoneyHandler
    {
        public override string withdraw(int amount)
        {
            result = null;
            if (amount >= 500)
            {
                int num = amount / 500;
                int rest = amount % 500;
                result = $"Issuing {num} of 500 uah bill(s)\n";
                if (successor != null && rest != 0)
                {
                    result += successor.withdraw(rest);
                    return result;
                }
                else if (rest == 0) return result;
            }
            else if (successor != null)
            {
                result += successor.withdraw(amount);
                return result;
            }
            throw new CombinationException();
        }
    }
}
