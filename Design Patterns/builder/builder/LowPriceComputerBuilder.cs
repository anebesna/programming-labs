using System;
namespace builder
{
    public class LowPriceComputerBuilder : IComputerBuilder
    {
        public override void setCPU()
        {
            computer.Add(new CPU("Intel Celeron G1620", 2, 2, 2.7, 2, 1515));
        }
        public override void setMotherboard()
        {
            computer.Add(new Motherboard("Asus H61M-C", "Intel H61", 16, 1492));
        }
        public override void setHDD()
        {
            computer.Add(new HDD(1000, 64, 1666));
        }
        public override void setRAM()
        {
            computer.Add(new RAM(8, 1.6, 910));
        }
        public override void setPSU()
        {
            computer.Add(new PSU("Vinga", 400, 477));
        }
    }
}
