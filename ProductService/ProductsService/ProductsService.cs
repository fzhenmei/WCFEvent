using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ProductsEntityModel;
using System.Threading;
using System.Windows;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;

namespace Products
{
    // WCF Service that implements the service contract
    // This implementation performs minimal error checking and exception handling
    public class ProductsServiceImpl : IProductsServiceV3
    {
        static List<IProductsServiceV3Callback> subscribers = 
            new List<IProductsServiceV3Callback>();

        public bool ProductExists(string productNumber, AdventureWorksEntities database)
        {
            // Check to see whether the specified product exists in the database
            int numProducts = (from p in database.Products
                               where string.Equals(p.ProductNumber, productNumber)
                               select p).Count();

            return numProducts > 0;

        }

        public List<string> ListProducts()
        {
            // Create a list for holding product numbers
            List<string> productsList = new List<string>();

            try
            {
                // Connect to the AdventureWorks database by using the Entity Framework
                using (AdventureWorksEntities database = new AdventureWorksEntities())
                {
                    // Fetch the product number of every product in the database
                    var products = from product in database.Products
                                   select product.ProductNumber;
                    
                    productsList = products.ToList();
                }
            }
            catch (Exception e)
            {
                // Ignore exceptions in this implementation
            }

            // Return the list of product numbers
            return productsList;
        }

        public ProductData GetProduct(string productNumber)
        {
            // Create a reference to a ProductData object
            // This object will be instantiated if a matching product is found
            ProductData productData = null;

            try
            {
                // Connect to the AdventureWorks database by using the Entity Framework
                using (AdventureWorksEntities database = new AdventureWorksEntities())
                {
                    // Check that the specified product exists
                    if (ProductExists(productNumber, database))
                    {
                        // Find the first product that matches the specified product number
                        Product matchingProduct = database.Products.First(
                            p => String.Compare(p.ProductNumber, productNumber) == 0);

                        productData = new ProductData()
                        {
                            Name = matchingProduct.Name,
                            ProductNumber = matchingProduct.ProductNumber,
                            Color = matchingProduct.Color,
                            ListPrice = matchingProduct.ListPrice
                        };
                    }
                }
            }
            catch
            {
                // Ignore exceptions in this implementation
            }

            // Return the product
            return productData;
        }

        public int CurrentStockLevel(string productNumber)
        {
            // Obtain the total stock level for the specified product.
            // The stock level is calculated by summing the quantity of the product
            // available in all the bins in the ProductInventory table.

            // The Product and ProductInventory tables are joined over the 
            // ProductID column.

            int stockLevel = 0;

            try
            {
                // Connect to the AdventureWorks database by using the Entity Framework
                using (AdventureWorksEntities database = new AdventureWorksEntities())
                {
                    // Check that the specified product exists
                    if (ProductExists(productNumber, database))
                    {
                        // Calculate the sum of all quantities for the specified product
                        stockLevel = (from pi in database.ProductInventories
                                      join p in database.Products
                                      on pi.ProductID equals p.ProductID
                                      where String.Compare(p.ProductNumber, productNumber) == 0
                                      select (int)pi.Quantity).Sum();
                    }
                }
            }
            catch
            {
                // Ignore exceptions in this implementation
            }

            // Return the stock level
            return stockLevel;
        }

        public bool ChangeStockLevel(string productNumber, short newStockLevel, 
                                     string shelf, int bin)
        {
            // Modify the current stock level of the selected product 
            // in the ProductInventory table.
            // If the update is successful then return true, otherwise return false.

            // The Product and ProductInventory tables are joined over the 
            // ProductID column.

            try
            {
                // Connect to the AdventureWorks database by using the Entity Framework
                using (AdventureWorksEntities database = new AdventureWorksEntities())
                {
                    if (!ProductExists(productNumber, database))
                        return false;
                    else
                    {
                        // Find the ProductID for the specified product
                        int productID =
                            (from p in database.Products
                             where String.Compare(p.ProductNumber, productNumber) == 0
                             select p.ProductID).First();

                        // Find the ProductInventory object that matches the parameters passed
                        // in to the operation
                        ProductInventory productInventory = database.ProductInventories.First(
                            pi => String.Compare(pi.Shelf, shelf) == 0 &&
                                  pi.Bin == bin &&
                                  pi.ProductID == productID);

                        // Update the stock level for the ProductInventory object
                        productInventory.Quantity += newStockLevel;

                        // Save the change back to the database
                        database.SaveChanges();
                    }
                }
            }
            catch
            {
                // If an exception occurs, return false to indicate failure
                return false;
            }

            // Return true to indicate success
            return true;
        }

        public void ChangePrice(string productNumber, decimal price)
        {
            // Modify the price of the selected product 
            // If the update is successful then return true, otherwise return false.
            Product product = null;

            try
            {
                // Connect to the AdventureWorks database by using the Entity Framework
                using (AdventureWorksEntities database = new AdventureWorksEntities())
                {
                    if (!ProductExists(productNumber, database))
                        return;
                    else
                    {
                        // Find the specified product
                        product = (from p in database.Products
                                   where String.Compare(p.ProductNumber, productNumber) == 0
                                   select p).First();

                        // Change the price for the product
                        product.ListPrice = price;

                        // Save the change back to the database
                        database.SaveChanges();
                    }
                }
            }
            catch
            {
                // If an exception occurs, return false to indicate failure
                return;
            }

            // Notify the client that the price has been changed successfully
            ProductData productData = new ProductData()
                {
                    ProductNumber = product.ProductNumber,
                    Name = product.Name,
                    ListPrice = product.ListPrice,
                    Color = product.Color
                };

            raisePriceChangedEvent(productData);
        }

        public bool SubscribeToPriceChangedEvent()
        {
            try
            {
                IProductsServiceV3Callback callback = 
                    OperationContext.Current.GetCallbackChannel<IProductsServiceV3Callback>();
                if (!subscribers.Contains(callback))
                {
                    subscribers.Add(callback);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UnsubscribeFromPriceChangedEvent()
        {
            try
            {
                IProductsServiceV3Callback callback =
                    OperationContext.Current.GetCallbackChannel<IProductsServiceV3Callback>();
                subscribers.Remove(callback);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void raisePriceChangedEvent(ProductData product)
        {
            subscribers.AsParallel().ForAll(callback =>
                {
                    if (((ICommunicationObject)callback).State == CommunicationState.Opened)
                    {
                        callback.OnPriceChanged(product);
                    }
                    else
                    {
                        subscribers.Remove(callback);
                    }
                }
            );
        }
    }   
}
