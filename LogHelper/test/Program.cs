using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogHelper;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Write("hehehehehe");
            Log.Write("asdhfasdjfksdjfks\r\njafsldkfjlasdfjlsadfjlas\r\n");
            Console.WriteLine("OK");
            Console.ReadKey();
        }
    }
}
