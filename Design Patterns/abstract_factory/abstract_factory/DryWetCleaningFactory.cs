using System;
namespace abstract_factory
{
    public class DryWetCleaningFactory : CleaningFactory
    {
        public DryWetCleaningFactory()
        {
            Console.WriteLine("\nProduction line has been changed to dry and wet cleaning.");
        }

        public ManualCleaningVacuum createManualCleaningVacuum()
        {
            return new DryWetCleaningManualVacuum();
        }

        public PortableCarVacuum createPortableCarVacuum()
        {
            return new DryWetCleaningPortableCarVacuum();
        }

        public RobotVacuum createRobotVacuum()
        {
            return new DryWetCleaningRobotVacuum();
        }
    }
}
