using Luftborn.Application.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Luftborn.Application.Abstractions
{
    public interface IProductDataAccess
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<Product> AddAsync(Product product);
        Task UpdateAsync(int id, Product product);
        Task DeleteAsync(int id);
    }
}
