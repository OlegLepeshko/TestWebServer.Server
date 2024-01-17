using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using TestWebServer.Server.Data;
using TestWebServer.Shared;

namespace TestWebServer.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly DataContext _context;  

        public OrderController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            var orders = await _context.Orders.ToListAsync();

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order is null)
                return NotFound("Order not found");

            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<List<Order>>> AddOrder([FromBody]Order order)
        {
            var newOrder = await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return Ok(newOrder);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<List<Order>>> UpdateOrder(int Id, Order updatedOrder)
        {
            var dbOrder = await _context.Orders.FindAsync(Id);

            if (dbOrder is null)
            {
                return NotFound("Order not found");
            }

            dbOrder.Name = updatedOrder.Name;
            dbOrder.State = updatedOrder.State;

            var order = await _context.SaveChangesAsync();

            return Ok(dbOrder);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<List<Order>>> DeleteOrder(int id)
        {
            var dbOrder = await _context.Orders.FindAsync(id);
            if (dbOrder is null)
                return NotFound("Order not found");

            var deletedOrder = _context.Orders.Remove(dbOrder);

            await _context.SaveChangesAsync();

            return Ok(deletedOrder);
        }
    }
}
