using System;
using System.ServiceModel;

namespace MyServer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var host = new ServiceHost(typeof (MyWcfService));
            host.Open();

            Console.ReadLine();
        }
    }
}