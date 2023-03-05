using System;
namespace chain
{
    public class UAH100Handler : MoneyHandler
    {
        public override string withdraw(int amount)
        {
            result = null;
            if (amount >= 100)
            {
                int num = amount / 100;
                int rest = amount % 100;
                result = $"Issuing {num} of 100 uah bill(s)\n";
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
