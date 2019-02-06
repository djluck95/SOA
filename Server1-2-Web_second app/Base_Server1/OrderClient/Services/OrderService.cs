using System;
using OrderClient.Models;

namespace OrderClient.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Runtime.Serialization.Json;
    using System.Xml.Serialization;

    public class OrderService
    {
        public OrderService()
        {
        }

        public IList<Order> GetOrders()
        {
            var client = new WebClient();

            client.Headers.Add("Accept", "application/json");

            var result = client.DownloadString
                ("http://localhost:50021/OrderService.svc/GetOrders");            

            var serializer = new DataContractJsonSerializer(typeof(List<Order>));

            List<Order> resultObject;
            using (var stream = new MemoryStream(Encoding.ASCII.GetBytes(result)))
            {
                resultObject = (List<Order>)serializer.ReadObject(stream);
            }

            return resultObject;
        }

        public Order GetOrderByID(int orderId)
        {
            var client = new WebClient();

            var result = client.DownloadString
                ("http://localhost:50021/OrderService.svc/GetOrder/" + orderId);

            var serializer = new XmlSerializer(typeof(Order));

            Order resultObject;
            using (var stream = new 
                MemoryStream(Encoding.ASCII.GetBytes(result)))
            {
                resultObject = (Order)serializer.Deserialize(stream);
            }
            return resultObject;
        }

        public void CreateOrder(Order order)
        {
            SendDataToServer(
                "http://localhost:50021/OrderService.svc/PutOrder",
                "POST", order);
        }        

        public void UpdateOrder(Order order)
        {
            SendDataToServer(
                "http://localhost:50021/OrderService.svc/UpdateOrder",
                "PUT", order);
        }

        public void DeleteOrder(int orderId)
        {
            SendDataToServer(
                "http://localhost:50021/OrderService.svc/DeleteOrder"
                + orderId, "DELETE",
                new DeleteOrder { id = orderId });
        }


        public void UpdateStock(Order order)
        {
            SendDataToServer(
                "http://localhost:50021/OrderService.svc/UpdateOrder",
                "POST", order);
        }

        private T SendDataToServer<T>(string endpoint, string method, T order)
        {
            try
            {
                var request = (HttpWebRequest) HttpWebRequest.Create(endpoint);
                request.Accept = "application/json";
                request.ContentType = "application/json";
                request.Method = method;

                var serializer = new DataContractJsonSerializer(typeof(T));
                var requestStream = request.GetRequestStream();
                serializer.WriteObject(requestStream, order);
                requestStream.Close();

                var response = request.GetResponse();
                if (response.ContentLength == 0)
                {
                    response.Close();
                    return default(T);
                }

                var responseStream = response.GetResponseStream();
                var responseObject = (T) serializer.ReadObject(responseStream);

                responseStream.Close();

                return responseObject;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return default(T); 
            }
        }
    }
}





