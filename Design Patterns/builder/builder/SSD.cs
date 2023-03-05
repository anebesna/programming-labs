using System;
using static System.Console;

namespace builder
{
    public class SSD : Component
    {
        int capacity;
        int writeSpeed;
        int readSpeed;

        public SSD(int capacity, int writeSpeed, int readSpeed, double price)
        {
            this.capacity = capacity;
            this.writeSpeed = writeSpeed;
            this.readSpeed = readSpeed;
            Price = price;
        }

        public override void Details()
        {
            WriteLine("SSD:");
            WriteLine($"\tstorage capacity: {capacity}Gb" +
            $"\n\twrite/read speed: {writeSpeed}/{readSpeed}Mb/s\n\tprice: {Price}₴");
        }
    }
}
