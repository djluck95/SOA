using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
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
     
        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "/Stock")]
        public void GetOptions3(){}

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [OperationContract]
        [WebInvoke(UriTemplate = "/Stock")]
        public async Task<Order> CheckStock(Order order)
        {
            var result = _dataAccessMethods.CheckStockForOrder(order);
            return result == false ? null : order;
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/UpdateStock")]
        public async Task<Order> UpdateStock(Order order)
        {
            return await _dataAccessMethods.UpdateStock(order);
        }
    }
}
