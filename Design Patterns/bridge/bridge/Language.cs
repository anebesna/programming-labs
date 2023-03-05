using System;
using System.IO;

namespace bridge
{
    public interface Language
    {
        public void translate(string messageType);
         
        public string GetResourceTextFile(string filename)
        {
            string result = string.Empty;

            using (Stream stream = GetType().Assembly.
                       GetManifestResourceStream("bridge." + filename))
            {
                using StreamReader sr = new StreamReader(stream);
                result = sr.ReadToEnd();
            }
            return result;
        }
    }
}
