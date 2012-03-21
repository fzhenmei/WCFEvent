using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Contract;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Client";

            for (int i = 0; i < 100; i++)
            {
                var factory = new ChannelFactory<IService1>("MyServer");
                var client = factory.CreateChannel();
                var result = client.GetData(i);
                Console.WriteLine(result);
            }

            Console.ReadKey();
        }
    }
}
