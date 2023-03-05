using System;
namespace builder
{
    public class Director
    {
        public void buildComputer(IComputerBuilder builder)
        {
            builder.setMotherboard();
            builder.setCPU();
            builder.setRAM();
            builder.setGPU();
            builder.setSSD();
            builder.setHDD();
            builder.setPSU();
            builder.getResult();
        }
    }
}
