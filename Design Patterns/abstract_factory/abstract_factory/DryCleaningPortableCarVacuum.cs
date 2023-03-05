using System;
namespace abstract_factory
{
    public class DryCleaningPortableCarVacuum : PortableCarVacuum
    {
        public DryCleaningPortableCarVacuum()
        {
            Console.WriteLine("Portable car vacuum for dry cleaning has been created.");
        }
    }
}
