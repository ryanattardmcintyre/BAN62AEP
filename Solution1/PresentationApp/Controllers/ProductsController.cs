using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
        private IWebHostEnvironment _env;
        public ProductsController(IProductsService prodService, ICategoriesService categoryService, IWebHostEnvironment env)
        {
            _prodService = prodService;
            _catService = categoryService;
            _env = env;
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
                if (data.File != null)
                {
                    if(data.File.Length > 0)
                    {
                        string newFilename = @"/Images/" + Guid.NewGuid() + System.IO.Path.GetExtension(data.File.FileName);
                        string absolutePath = _env.WebRootPath;

                        //C:\Users\Ryan\source\repos\BAN62AEP\BAN62AEP\Solution1\PresentationApp\wwwroot\

                        using (var stream = System.IO.File.Create(absolutePath + newFilename))
                        {
                            data.File.CopyTo(stream);
                        }

                        data.Product.ImageUrl = newFilename;
                    }
                }

                _prodService.AddProduct(data.Product);

                TempData["feedback"] = "Product added successfully";
                ModelState.Clear();
            }
            catch(Exception ex)
            {
                TempData["warning"] = "Product was not added successfully. Check your inputs";
            }
            
            var list = _catService.GetCategories();
            data.Categories = list.ToList();

            return View(data);
        }


        public IActionResult Delete(Guid id)
        {
            _prodService.DeleteProduct(id);

            TempData["feedback"] = "product deleted successfully";  //ViewData should be changed to TempData
           
            return RedirectToAction("Index");
        }




    }
}
