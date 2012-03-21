using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.ServiceReference1;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Service1Client();

            while (true)
            {
                var input = Console.ReadLine();
                var i = 0;
                int.TryParse(input, out i);
                var result = client.GetData(i);
                Console.WriteLine(result);

            }
        }
    }
}
