using System;
namespace builder
{
    abstract public class IComputerBuilder
    {
        protected Computer computer = new Computer();

        public abstract void setCPU();
        public abstract void setMotherboard();
        public abstract void setRAM();
        public virtual void setGPU() { }
        public virtual void setSSD() { }
        public virtual void setHDD() { }
        public abstract void setPSU();
        public void getResult()
        {
            computer.ShowInternals();
        }
    }
}
