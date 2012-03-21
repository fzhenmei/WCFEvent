using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.ServiceReference1;
using Client.ServiceReference2;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Client";

            var client1 = new Service1Client();
            var client2 = new Service2Client();
            while (true)
            {
                var input = Console.ReadLine();
                var i = 0;
                int.TryParse(input, out i);
                var result1 = client1.GetData(i);
                var result2 = client2.GetData(i);
                Console.WriteLine(result1);
                Console.WriteLine(result2);

            }
        }
    }
}
