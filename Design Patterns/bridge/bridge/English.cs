using System;
using static System.Console;
using System.Globalization;
using System.Resources;
using System.IO;

namespace bridge
{
    public class English : Language
    {
        public void translate(string messageType)
        {
            WriteLine("It works");
            using (Stream stream = typeof(English).Assembly.GetManifestResourceStream("bridge.Strings.en.xml"))
            {
                WriteLine(stream.)
            }
        }
    }
}
