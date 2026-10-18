using Luftborn.Application.Abstractions;

namespace Luftborn.Application.Products;

public class ProductService
{
    private readonly IProductDataAccess _productDataAccess;

    public ProductService(IProductDataAccess productDataAccess)
    {
        _productDataAccess = productDataAccess;
    }

    // NEW: Retrieve all products
    public Task<List<Product>> GetAllAsync()
    {
        return _productDataAccess.GetAllAsync();
    }

    // NEW: Retrieve one product
    public Task<Product?> GetByIdAsync(int id)
    {
        return _productDataAccess.GetByIdAsync(id);
    }

    // NEW: Create a product
    public Task<Product> AddAsync(Product product)
    {
        return _productDataAccess.AddAsync(product);
    }

    // NEW: Update a product
    public Task UpdateAsync(int id, Product product)
    {
        return _productDataAccess.UpdateAsync(id, product);
    }

    // NEW: Delete a product
    public Task DeleteAsync(int id)
    {
        return _productDataAccess.DeleteAsync(id);
    }
}