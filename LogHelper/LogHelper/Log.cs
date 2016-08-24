using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LogHelper
{
    public class Log
    {
        private static string location
        {
            get
            {
                return File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Log/Log.txt")
                    ? AppDomain.CurrentDomain.BaseDirectory + "/Log/Log.txt"
                    : Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Log").FullName + "/Log.txt";
            }
        }

        private static string headerformation
        {
            get
            {
                return DateTime.Now.ToString() + " : ";
            }
        }

        public static void Write(string log_content)
        {
            FileStream stream = new FileStream(location, FileMode.Append);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(headerformation);
            writer.WriteLine(log_content);
            writer.WriteLine("========================================================================\r");

            writer.Flush();
            writer.Close();
            stream.Close();
        }
    }
}
