using System;
namespace chain
{
    public abstract class MoneyHandler
    {
        protected MoneyHandler successor;
        protected string result;

        public void SetSuccessor(MoneyHandler successor)
        {
            this.successor = successor;
        }
        public abstract string withdraw(int amount);
    }
}
