using System;
using System.Collections.Generic;
using static System.Console;

namespace builder
{
    public class Computer
    {
        public double totalPrice;
        private List<Component> components = new List<Component>();

        public void Add(Component part)
        {
            components.Add(part);
        }

        public void ShowInternals()
        {
            WriteLine("------------ Computer Parts ------------\n");
            foreach (Component part in components)
            {
                part.Details();
                totalPrice += part.Price;
            }
            WriteLine($"\n----------------------------------------\nTotal price: {totalPrice}₴\n");
        }
    }
}
