using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using System.Xml.Serialization;
using OrderClient.Models;
using OrderStorage;
using static System.Int32;
using Order = OrderStorage.Order;

namespace OrdersRESTService
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.ServiceModel.Web;

    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OrderService
    {         
        private DataAccessMethods _dataAccessMethods;

        public OrderService()
        {
            _dataAccessMethods = new DataAccessMethods();
        }
     
        [WebGet(UriTemplate = "/GetOrders")]
        public IList<Order> GetOrders()
        {
            return _dataAccessMethods.GetOrders().ToList();
        }

        [WebGet(UriTemplate = "/GetOrder/{id}")]
        public Task<Order> GetOrderByID(string id)
        {
            TryParse(id, out var idOrder);

            return _dataAccessMethods.GetOrder(idOrder);
        }

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "/PutOrder")]
        public void GetOptions()
        { 
        }

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "/DeleteOrder")]
        public void GetOptions2()
        {
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/PutOrder")]
        public async Task<Order> CreateOrder(Order order)
        {
            if (CheckStock(order) == true)
            {
                UpdateStock(order);
                return await _dataAccessMethods.AddOrder(order);
            }
            return null;
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [WebInvoke(Method = "DELETE", UriTemplate = "/DeleteOrder")]
        public async Task<Order> DeleteOrder(DeleteOrder deleteOrderId)
        {
            if (deleteOrderId?.id == null)
                return null;

            return await _dataAccessMethods.DeleteOrder(deleteOrderId.id);
        }

        public bool CheckStock(Order order)
        {
            var r = SendDataToServer(
                "http://localhost:50020/OrderService.svc/Stock",
                "POST", order);

            if (r == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void UpdateStock(Order order)
        {
            var r = SendDataToServer(
                "http://localhost:50020/OrderService.svc/UpdateStock",
                "POST", order);
        }

        private T SendDataToServer<T>(string endpoint, string method, T order)
        {
            try
            {
                var request = (HttpWebRequest)HttpWebRequest.Create(endpoint);
                request.Accept = "application/json";
                request.ContentType = "application/json";
                request.Method = method;
                

               string a =String.Empty;

                using (MemoryStream ms = new MemoryStream())
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                    ser.WriteObject(ms, order);
                    a = Encoding.UTF8.GetString(ms.ToArray());
                }

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
                var responseObject = (T)serializer.ReadObject(responseStream);

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
