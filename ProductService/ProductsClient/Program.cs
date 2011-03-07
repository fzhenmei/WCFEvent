using System.ServiceModel;
using System.ServiceModel.Channels;
using System;
using System.Collections.Generic;
using System.Text;
using ProductsClient.ProductsService;

namespace ProductsClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press ENTER when the service has started");
            Console.ReadLine();

            using (Client client = new Client())
            {
                client.TestProductsService();

                Console.WriteLine("Press ENTER to finish");
                Console.ReadLine();
            }
        }
    }

    public class Client : ProductsServiceCallback, IDisposable
    {
        private ProductsServiceClient proxy = null;

        public void TestProductsService()
        {
            // Create a proxy object and connect to the service
            proxy = new ProductsServiceClient(new InstanceContext(this),
                                              "WSDualHttpBinding_IProductsServiceV3");

            // Test the operations in the service
            try
            {
                proxy.SubscribeToPriceChangedEvent();

                // Obtain a list of products
                Console.WriteLine("Test 1: List all products");
                string[] productNumbers = proxy.ListProducts();
                foreach (string productNumber in productNumbers)
                {
                    Console.WriteLine("Number: {0}", productNumber);
                }
                Console.WriteLine();

                // Fetch the details for a specific bicycle frame
                Console.WriteLine("Test 2: Display the details of a bicycle frame");
                ProductData product = proxy.GetProduct("FR-M21S-40");
                Console.WriteLine("Number: {0}", product.ProductNumber);
                Console.WriteLine("Name: {0}", product.Name);
                Console.WriteLine("Color: {0}", product.Color);
                Console.WriteLine("Price: {0:C}", product.ListPrice);
                Console.WriteLine();

                // Modify the price of this bicycle frame
                Console.WriteLine("Test 3: Modify the price of a bicycle frame");
                proxy.ChangePrice("FR-M21S-40", product.ListPrice + 10);
                Console.WriteLine();
            }

            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }
        }

        public void OnPriceChanged(ProductData product)
        {
            Console.WriteLine("\nCallback from service:\nPrice of {0} changed to {1:C}",
                product.Name, product.ListPrice);
        }

        public void Dispose()
        {
            // Disconnect from the service
            proxy.Close();
        }
    }
}
