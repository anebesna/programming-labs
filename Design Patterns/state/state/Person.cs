using System;

namespace state
{
    class Person
    {
        static void Main(string[] args)
        {
            Product tomato = new Product("Tomato", 3);
            Product egg = new Product("Egg", 2);
            Product milk = new Product("Milk", 3);
            egg.Consume();
            tomato.Consume();
            milk.Consume();
            egg.Consume();
            egg.Consume();
            egg.Consume();
        }
    }
}
