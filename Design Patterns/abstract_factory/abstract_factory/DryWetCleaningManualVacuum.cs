using System;
namespace abstract_factory
{
    public class DryWetCleaningManualVacuum : ManualCleaningVacuum
    {
        public DryWetCleaningManualVacuum()
        {
            Console.WriteLine("Manual cleaning vacuum for dry and wet cleaning has been created.");
        }
    }
}
