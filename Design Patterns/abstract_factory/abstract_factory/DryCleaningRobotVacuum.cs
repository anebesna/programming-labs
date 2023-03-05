using System;
namespace abstract_factory
{
    public class DryCleaningRobotVacuum : RobotVacuum
    {
        public DryCleaningRobotVacuum()
        {
            Console.WriteLine("Robot vacuum for dry cleaning has been created.");
        }
    }
}
