using System;
using static System.Console;

namespace builder
{
    public class Motherboard : Component
    {
        string name;
        string chipset;
        int maxStorage;

        public Motherboard(string name, string chipset, int maxStorage, double price)
        {
            this.name = name;
            this.chipset = chipset;
            this.maxStorage = maxStorage;
            Price = price;
        }

        public override void Details()
        {
            WriteLine("Motherboard:");
            WriteLine($"\tfull name: {name}\n\tchipset: {chipset}" +
            $"\n\tmaximum storage capacity: {maxStorage}Gb\n\tprice: {Price}₴");
        }
    }
}
