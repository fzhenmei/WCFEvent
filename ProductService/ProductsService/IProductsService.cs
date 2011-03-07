using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Net.Security;


namespace Products
{
    // Data contract describing the details of a product passed to client applications
    [DataContract]
    public class ProductData
    {
        [DataMember]
        public string Name;

        [DataMember]
        public string ProductNumber;

        [DataMember]
        public string Color;

        [DataMember]
        public decimal ListPrice;
    }


    // Callback interface for notifying the client that the price has changed
    public interface IProductsServiceV3Callback
    {
        [OperationContract(IsOneWay=true)]
        void OnPriceChanged(ProductData product);
    }

    // Version 3 of the service contract
    [ServiceContract(Namespace = "http://adventure-works.com/2010/07/22",
                     Name = "ProductsService",
                     CallbackContract = typeof(IProductsServiceV3Callback))]
    public interface IProductsServiceV3
    {
        // Get the product number of every product
        [OperationContract]
        List<string> ListProducts();

        // Get the details of a single product
        [OperationContract]
        ProductData GetProduct(string productNumber);

        // Get the current stock level for a product
        [OperationContract]
        int CurrentStockLevel(string productNumber);

        // Change the stock level for a product
        [OperationContract]
        bool ChangeStockLevel(string productNumber, short newStockLevel, string shelf, int bin);

        // Change the price of a product
        [OperationContract(IsOneWay = false)]
        void ChangePrice(string productNumber, decimal price);

        // Subscribe tob the "price changed" event
        [OperationContract]
        bool SubscribeToPriceChangedEvent();

        // Unsubscribe from the "price changed" event
        [OperationContract]
        bool UnsubscribeFromPriceChangedEvent();
    }
}