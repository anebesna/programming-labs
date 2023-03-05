using System;
using static System.Console;

namespace builder
{
    public class RAM : Component
    {
        int capacity;
        double clockSpeed;

        public RAM(int capacity, double clockSpeed, double price)
        {
            this.capacity = capacity;
            this.clockSpeed = clockSpeed;
            Price = price;
        }

        public override void Details()
        {
            WriteLine("RAM:");
            WriteLine($"\tstorage capacity: {capacity}Gb" +
            $"\n\tclock speed: {clockSpeed}GHz\n\tprice: {Price}₴");
        }
    }
}
