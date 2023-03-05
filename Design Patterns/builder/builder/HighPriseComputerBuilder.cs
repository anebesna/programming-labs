using System;
namespace builder
{
    public class HighPriseComputerBuilder : IComputerBuilder
    {
        public override void setCPU()
        {
            computer.Add(new CPU("Intel Core i9 10900F", 10, 20, 2.8, 20, 14464));
        }
        public override void setMotherboard()
        {
            computer.Add(new Motherboard("Asus Prime B560M-K", "Intel B560", 64, 3645));
        }
        public override void setGPU()
        {
            computer.Add(new GraphicsCard("GeForce RTX 3060 Ti", 8192, 1.7, 14.0, 256, 20559));
        }
        public override void setHDD()
        {
            computer.Add(new HDD(2000, 64, 2230));
        }
        public override void setSSD()
        {
            computer.Add(new SSD(1000, 1700, 2100, 3312));
        }
        public override void setRAM()
        {
            computer.Add(new RAM(32, 3.0, 4568));
        }
        public override void setPSU()
        {
            computer.Add(new PSU("Deepcool", 750, 2458));
        }
    }
}
