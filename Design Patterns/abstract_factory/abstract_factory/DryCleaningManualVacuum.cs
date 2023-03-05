using System;
namespace abstract_factory
{
    public class DryCleaningManualVacuum : ManualCleaningVacuum
    {
        public DryCleaningManualVacuum()
        {
            Console.WriteLine("Manual cleaning vacuum for dry cleaning has been created.");
        }
    }
}
