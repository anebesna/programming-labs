using System;
using System.IO;

namespace bridge
{
    class Program
    {
        static void Main(string[] args)
        {
            User u = new User("Anna");
            SystemMessage sm = new ErrorWarning(u.language);
            sm.ShowMessage();
        }
    }
}
