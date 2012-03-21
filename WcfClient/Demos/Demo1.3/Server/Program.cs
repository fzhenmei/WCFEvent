using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Server";

            var host1 = new ServiceHost(typeof(Service1));
            host1.Open();

            var host2 = new ServiceHost(typeof(Service2));
            host2.Open();

            Console.ReadKey();
        }
    }
}
