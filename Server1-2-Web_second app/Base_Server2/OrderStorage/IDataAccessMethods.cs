using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderStorage
{
    interface IDataAccessMethods
    {
        IEnumerable<Order> GetOrders();
        Task<Order> GetOrder(int id);
        Task UpdateOrder(int id, Order order);
        Task<Order> AddOrder(Order order);
        Task<Order> DeleteOrder(int id);
    }
}
