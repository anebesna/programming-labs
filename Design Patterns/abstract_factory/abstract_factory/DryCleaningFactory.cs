using System;
namespace abstract_factory
{
    public class DryCleaningFactory : CleaningFactory
    {
        public DryCleaningFactory()
        {
            Console.WriteLine("\nProduction line has been changed to dry cleaning.");
        }

        public ManualCleaningVacuum createManualCleaningVacuum()
        {
            return new DryCleaningManualVacuum();
        }

        public PortableCarVacuum createPortableCarVacuum()
        {
            return new DryCleaningPortableCarVacuum();
        }

        public RobotVacuum createRobotVacuum()
        {
            return new DryCleaningRobotVacuum();
        }
    }
}
