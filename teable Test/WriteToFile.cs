using System;
using System.IO;
using System.Text;

namespace Table
{
    public class WriteToFile
    {
        public static void PrintLine(string source)
        {
            using (StreamWriter streamWriter = new StreamWriter("table.txt", true, System.Text.Encoding.Default))
            {
                streamWriter.WriteLine(source);
                streamWriter.Flush();
            }
        }
    }
}
