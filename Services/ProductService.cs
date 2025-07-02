using Back_End.Data;
using Back_End.Models;
using Back_End.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Back_End.Services
{
    public class ProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            return await _context.Products
                .AsNoTracking()  // This prevents tracking issues
                .Select(p => new ProductDTO
                {
                    Name = p.Name,
                    Price = p.Price
                })
                .ToListAsync();
        }


        public async Task<ProductDTO?> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            return new ProductDTO
            {
                Name = product.Name,
                Price = product.Price
            };
        }

        public async Task<int> AddProduct(ProductDTO productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product.Id;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ProductDTO?> UpdateProduct(int Id, ProductDTO productDto)
        {
            var product =
                await _context.Products.SingleAsync(p => p.Id == Id);

            if (product == null) return null;
            product.Price = productDto.Price;
            product.Name = productDto.Name;
            await _context.SaveChangesAsync();

            return new ProductDTO
            {
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}
