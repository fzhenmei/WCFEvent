using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using MyContract;

namespace Client1
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ChannelFactory<IMyWcfService>(new NetTcpBinding(SecurityMode.Message),
                                                            new EndpointAddress("net.tcp://localhost:8730"));
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine(i);
                var channel = factory.CreateChannel();
                channel.DoWork();
            }

            Console.ReadLine();
        }
    }
}
