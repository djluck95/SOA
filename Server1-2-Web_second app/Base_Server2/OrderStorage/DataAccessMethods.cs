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

        public IEnumerable<Order> GetOrders()
        {
            return _context.Order.OrderByDescending(d => d.id);
        }

        public async Task<Order> GetOrder(int id)
        {
            return await _context.Order.SingleOrDefaultAsync(m => m.id == id);
        }

        public async Task UpdateOrder(int id, Order order)
        {
            var workoutFromDb = _context.Order.FirstOrDefault(c => c.id == id);
            if (workoutFromDb == null)
                //return NotFound();

            workoutFromDb.name = order.name;
            workoutFromDb.quantity = order.quantity;
            workoutFromDb.date = order.date;
            try
            {
                _context.Order.AddOrUpdate(workoutFromDb);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    //return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<Order> AddOrder(Order order)
        {
            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<Order> DeleteOrder(int id)
        {
            var order = await _context.Order.SingleOrDefaultAsync(m => m.id == id);
            if (order == null)
            {
                return null;
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return order;
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.id == id);
        }
    }
}
