using System;
namespace abstract_factory
{
    public class DryWetCleaningRobotVacuum : RobotVacuum
    {
        public DryWetCleaningRobotVacuum()
        {
            Console.WriteLine("Robot vacuum for dry and wet cleaning has been created.");
        }
    }
}
