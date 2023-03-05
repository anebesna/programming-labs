using System;
using System.Collections.Generic;

namespace state
{
    public class Product
    {
        public string name;
        public int daysToExpire;
        public State state = new Healthy();
        public List<Product> fridge = new List<Product>();
        public Product(string name, int daysToExpire)
        {
            this.name = name;
            this.daysToExpire = daysToExpire;
            fridge.Add(this);
            Console.WriteLine($"{name} has been successfully added.");
        }
        public void Consume()
        {
            if (fridge.Contains(this)) state.Consume(this);
            else Console.WriteLine($"There is no {name} in the fridge.");
        }
    }
}
