using System;
namespace chain
{
    public class CombinationException : Exception
    {
        public override string Message => "It is not possible to determine the combination of bills for the given amount.";
    }
}
