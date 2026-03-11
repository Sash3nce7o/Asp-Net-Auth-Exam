using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth_Exam.Core.Models.Product;
using Auth_Exam.Infrastructure.Data.Models;

namespace Auth_Exam.Core.Contracts
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product> GetProductById(string id);
        Task<Product> CreateProductAsync(CreateProductFormViewModel model);
        Task<bool> UpdateProductAsync(UpdateProductFormViewModel model);


    }
}