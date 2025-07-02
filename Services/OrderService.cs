using Back_End.Data;
using Back_End.Models;
using Back_End.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Back_End.Services
{
    public class OrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderResponseDTO>> GetAllOrders()
        {
            return await _context.Orders
                .Include(o => o.Product)
                .Select(o => new OrderResponseDTO
                {
                    Id = o.Id,
                    ProductName = o.Product!.Name,
                    Quantity = o.Quantity,
                    OrderDate = o.OrderDate
                })
                .ToListAsync();
        }

        public async Task<OrderResponseDTO?> GetOrderById(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return null;

            return new OrderResponseDTO
            {
                Id = order.Id,
                ProductName = order.Product!.Name,
                Quantity = order.Quantity,
                OrderDate = order.OrderDate
            };
        }

        public async Task<int> CreateOrder(OrderDTO orderDto)
        {
            var product = await _context.Products.FindAsync(orderDto.ProductId);
            if (product == null) throw new Exception("Product not found");

            var order = new Order
            {
                ProductId = orderDto.ProductId,
                Quantity = orderDto.Quantity,
                OrderDate = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order.Id;
        }
    }
}
