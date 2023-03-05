using System;
using static System.Console;

namespace builder
{
    public class HDD : Component
    {
        private int capacity;
        private int bufferSize;

        public HDD(int capacity, int bufferSize, double price)
        {
            this.capacity = capacity;
            this.bufferSize = bufferSize;
            Price = price;
        }

        public override void Details()
        {
            WriteLine("HDD:");
            WriteLine($"\tstorage capacity: {capacity}Gb" +
            $"\n\tbuffer size: {bufferSize}Mb\n\tprice: {Price}₴");
        }
    }
}
