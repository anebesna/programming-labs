using System;
using static System.Console;

namespace builder
{
    public class GraphicsCard : Component
    {
        private string name;
        private int vram;
        private double coreClock;
        private double memoryClock;
        private int busWidth;

        public GraphicsCard(string name, int vram, double coreClock, double memoryClock, int busWidth, double price)
        {
            this.name = name;
            this.vram = vram;
            this.coreClock = coreClock;
            this.memoryClock = memoryClock;
            this.busWidth = busWidth;
            Price = price;
        }
        public override void Details()
        {
            WriteLine("Graphics card:");
            WriteLine($"\tfull name: {name}\n\tVRAM: {vram}Mb" +
            $"\n\tcore clock speed: {coreClock}Ghz\n\tmemory clock speed: {memoryClock}Ghz\n" +
            $"\tbus width: {busWidth}bit\n\tprice: {Price}₴");
        }
    }
}
