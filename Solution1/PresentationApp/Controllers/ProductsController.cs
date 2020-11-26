using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PresentationApp.Models;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;

namespace PresentationApp.Controllers
{
    public class ProductsController : Controller
    {
        private IProductsService _prodService;
        private ICategoriesService _catService;
        public ProductsController(IProductsService prodService, ICategoriesService categoryService)
        {
            _prodService = prodService;
            _catService = categoryService;
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

        [HttpGet] //this will be called before loading the Create page
        public IActionResult Create()
        {
            CreateModel model = new CreateModel();
            
            var list = _catService.GetCategories();
            model.Categories = list.ToList();

            return View(model);
        }

        [HttpPost] //2nd method will  be triggered when the user clicks on the submit button!
        public IActionResult Create(CreateModel data)
        {
            //validation
            try
            {
                _prodService.AddProduct(data.Product);

                ViewData["feedback"] = "Product added successfully";
                ModelState.Clear();
            }
            catch(Exception ex)
            {
                ViewData["warning"] = "Product was not added successfully. Check your inputs";
            }
            
            var list = _catService.GetCategories();
            data.Categories = list.ToList();

            return View(data);
        }


        public IActionResult Delete(Guid id)
        {
            _prodService.DeleteProduct(id);

            ViewData["feedback"] = "product deleted successfully";

            var list = _prodService.GetProducts();
            return View("Index", list);

          //  return RedirectToAction("Index");
        }




    }
}
