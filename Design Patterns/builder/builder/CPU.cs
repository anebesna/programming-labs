using System;
using static System.Console;
namespace builder
{
    public class CPU : Component
    {
        private int cacheSize;
        private int cores;
        private int threads;
        private double clockSpeed;
        private string name;
        public CPU(string name, int cores, int threads, double clockSpeed, int cacheSize, double price)
        {
            this.cacheSize = cacheSize;
            this.clockSpeed = clockSpeed;
            this.cores = cores;
            this.threads = threads;
            this.name = name;
            Price = price;
        }

        public override void Details()
        {
            WriteLine("CPU:");
            WriteLine($"\tfull name: {name}\n" +
            $"\tnumber of cores/threads: {cores}/{threads}\n\tcore clock speed: {clockSpeed}Ghz\n" +
            $"\tl3 cache size: {cacheSize}Mb\n\tprice: {Price}₴");
        }
    }
}
