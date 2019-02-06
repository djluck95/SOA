using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace OrderStorage
{
    public class DataAccessMethods : IDataAccessMethods
    {
        private readonly OrderContext _context;

        public DataAccessMethods()
        {
            _context = new OrderContext();

        }

        public  bool CheckStockForOrder(Order order)
        {
            var dborder = _context.Stock.FirstOrDefault(c => c.name == order.name);
            if (dborder == null)
                return false;
            if (Convert.ToInt32(dborder.quantity) >= Convert.ToInt32(order.quantity))
                return true;
            else
            {
                return false;
            }
        }

        public async Task<Order> UpdateStock(Order order)
        {
            var dborder = _context.Stock.FirstOrDefault(c => c.name == order.name);
            if (dborder == null)
                //return NotFound();

            dborder.name = order.name;
            dborder.id = dborder.id;
            var rest = Convert.ToInt32(dborder.quantity) - Convert.ToInt32(order.quantity);
            dborder.quantity = rest.ToString();
            try
            {
                _context.Stock.AddOrUpdate(dborder);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }
            return null;
        }
    }
}
