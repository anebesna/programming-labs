using System;
using static System.Console;

namespace builder
{
    public class PSU : Component
    {
        string name;
        int power;

        public PSU(string name, int power, double price)
        {
            this.name = name;
            this.power = power;
            Price = price;
        }

        public override void Details()
        {
            WriteLine("PSU:");
            WriteLine($"\tbrand name: {name}" +
            $"\n\tpower: {power} Watt\n\tprice: {Price}₴");
        }
    }
}
