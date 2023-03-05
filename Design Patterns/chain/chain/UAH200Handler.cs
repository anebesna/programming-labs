using System;
namespace chain
{
    public class UAH200Handler : MoneyHandler
    {
        public override string withdraw(int amount)
        {
            result = null;
            if (amount >= 200)
            {
                int num = amount / 200;
                int rest = amount % 200;
                result = $"Issuing {num} of 200 uah bill(s)\n";
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
