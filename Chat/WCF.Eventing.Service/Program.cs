using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace WCF.Eventing.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            using( ServiceHost myHost = new ServiceHost(typeof(MyService)))
            {
                myHost.Open();
                Console.WriteLine("The WCF Duplex service is hosted and running.");
                Console.WriteLine("Press any key to exit... ");
                Console.ReadKey();
                myHost.Close();
            }
        }
    }
}
