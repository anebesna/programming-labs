using System;
namespace abstract_factory
{
    public class DryWetCleaningPortableCarVacuum : PortableCarVacuum
    {
        public DryWetCleaningPortableCarVacuum()
        {
            Console.WriteLine("Portable car vacuum for dry and wet cleaning has been created.");
        }
    }
}
