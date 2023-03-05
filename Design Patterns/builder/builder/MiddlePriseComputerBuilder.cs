using System;
namespace builder
{
    public class MiddlePriseComputerBuilder : IComputerBuilder
    {
        public override void setCPU()
        {
            computer.Add(new CPU("Intel Core i3 9100F", 4, 4, 3.6, 6, 3259));
        }
        public override void setMotherboard()
        {
            computer.Add(new Motherboard("Asus Prime H310M-K", "Intel H310", 32, 1679));
        }
        public override void setGPU()
        {
            computer.Add(new GraphicsCard("GeForce GTX 1050 Ti", 4096, 1.3, 7.0, 128, 7999));
        }
        public override void setHDD()
        {
            computer.Add(new HDD(1000, 64, 1678));
        }
        public override void setSSD()
        {
            computer.Add(new SSD(480, 530, 520, 1646));
        }
        public override void setRAM()
        {
            computer.Add(new RAM(16, 2.6, 1949));
        }
        public override void setPSU()
        {
            computer.Add(new PSU("Deepcool", 500, 1441));
        }
    }
}
