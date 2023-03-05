using System;
using static System.Console;

namespace abstract_factory
{
    class Production
    {
        static void Main(string[] args)
        {
            WriteLine("---------------Cleaning factory production---------------");
            CleaningFactory factory = new DryCleaningFactory();
            factory.createPortableCarVacuum();
            factory = new DryWetCleaningFactory();
            factory.createRobotVacuum();
        }
    }
}
