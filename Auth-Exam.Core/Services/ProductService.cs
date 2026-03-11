using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth_Exam.Core.Contracts;
using Auth_Exam.Core.Models.Product;
using Auth_Exam.Infrastructure.Data;
using Auth_Exam.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth_Exam.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext? _context;
        
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context!.Products.ToListAsync();
        }
        public async Task<Product> CreateProductAsync(CreateProductFormViewModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price
            };
            
            _context!.Products.Add(product);

            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> GetProductById(string id)
        {
            return await _context!.Products.FindAsync(id);
        }

        

        public async Task<bool> UpdateProductAsync(UpdateProductFormViewModel model)
        {
            var product = await _context!.Products.FindAsync(model.Id);
            if(product == null)
            {
                return false;
            }
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            await _context.SaveChangesAsync();
            return true;
        }


    }
}