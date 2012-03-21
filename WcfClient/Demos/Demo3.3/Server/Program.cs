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

            var host = new ServiceHost(typeof(Service1));
            host.Open();

            Console.ReadKey();
        }
    }
}
