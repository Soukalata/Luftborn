using Luftborn.Application.Abstractions;
using Luftborn.Application.Products;
using Luftborn.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Luftborn.Infrastructure.Products
{
    public class ProductDataAccess : IProductDataAccess
    {
        private readonly AppDBContext context;

        public ProductDataAccess(AppDBContext context)
        {
            this.context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await context.Products
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await context.Products
                .FindAsync(id);
        }

        public async Task<Product> AddAsync(Product product)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();

            return product;
        }

        public async Task DeleteAsync(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null)
            {
                return;
            }

            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }



        public async Task UpdateAsync(int id, Product product)
        {
            var existingProduct = await context.Products.FindAsync(id);

            if (existingProduct is null)
            {
                return;
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;

            await context.SaveChangesAsync();
        }
    }
}
