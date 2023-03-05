using System;
using static System.Console;
namespace state
{
    public class Harmful : State
    {
        public void Consume(Product product)
        {
            WriteLine($"{product.name} has expired. If you eat, it can harm your health.\nEat it anyway?");
            if (ReadLine() == "yes")
            {
                Write("You're dead.");
                Environment.Exit(0);
            }
            else
            {
                product.fridge.Remove(product);
                WriteLine($"{product.name} has been successfully removed.");
            }
        }
    }
}
