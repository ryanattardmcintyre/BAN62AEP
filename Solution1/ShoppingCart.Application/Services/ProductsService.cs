using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Services
{
    public class ProductsService : IProductsService
    {
        private IProductsRepository _productsRepo;
        public ProductsService(IProductsRepository productsRepo)
        {
            _productsRepo = productsRepo;
        }

        public IQueryable<ProductViewModel> GetProducts()
        {
            //this whole method will use linq to convert from Iqueryable<Product> to Iqueryable<ProductViewModel>
            var list = from p in _productsRepo.GetProducts()  
                       select new ProductViewModel()
                       {
                           Id = p.Id,
                           Description = p.Description,
                           ImageUrl = p.ImageUrl,
                           Name = p.Name,
                           Price = p.Price,
                           Category = new CategoryViewModel() { Id = p.Category.Id, Name = p.Category.Name }
                       };


            

            return list;
        }


    }
}
