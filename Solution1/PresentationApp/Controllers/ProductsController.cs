using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;

namespace PresentationApp.Controllers
{
    public class ProductsController : Controller
    {
        private IProductsService _prodService;
        public ProductsController(IProductsService prodService)
        {
            _prodService = prodService;
        }

        public IActionResult Index()
        {
            var list = _prodService.GetProducts();
            return View(list);
        }

        public IActionResult Details(Guid id)
        {
            var p = _prodService.GetProduct(id);
            return View(p);
        }

        //-------------------- ADD -----------------------------
        
        

        [HttpGet]
        public IActionResult Create()
        { 
            return View();
        }

        [HttpPost] //2nd method will  be triggered when the user clicks on the submit button!
        public IActionResult Create(ProductViewModel data)
        {
            //validation
            try
            {
                _prodService.AddProduct(data);

                ViewData["feedback"] = "Product added successfully";
                ModelState.Clear();
            }
            catch(Exception ex)
            {
                ViewData["warning"] = "Product was not added successfully. Check your inputs";
            }

            return View();
        }


    }
}
