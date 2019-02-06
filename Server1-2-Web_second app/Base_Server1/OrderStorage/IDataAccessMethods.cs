using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderStorage
{
    interface IDataAccessMethods
    {
        bool CheckStockForOrder(Order order);
        Task<Order> UpdateStock(Order order);
    }
}
