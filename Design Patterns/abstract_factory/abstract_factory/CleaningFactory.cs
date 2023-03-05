using System;
namespace abstract_factory
{
    public interface CleaningFactory
    {
        ManualCleaningVacuum createManualCleaningVacuum();
        PortableCarVacuum createPortableCarVacuum();
        RobotVacuum createRobotVacuum();
    }
}
