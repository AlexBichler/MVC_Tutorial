using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAcess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {

        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;

        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> categoryContext)
        {

            context = productContext;
            productCategories = categoryContext;

        }

        // GET: ProductManager
        public ActionResult Index()
        {

            List<Product> products = context.Collection().ToList();

            return View(products);
        }

        public ActionResult Create()
        {

            ProductManagerViewModel viewModel = new ProductManagerViewModel();

            viewModel.product = new Product();
            viewModel.ProductCategories = productCategories.Collection();

            Product product = new Product();

            return View(viewModel);

        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {

            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {

                if (file != null)
                {

                    product.Image = product.ID + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//" + product.Image));

                }

                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");

            }

        }

        public ActionResult Edit(string ID)
        {

            Product product = context.Find(ID);

            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {

                ProductManagerViewModel viewModel = new ProductManagerViewModel();

                viewModel.product = product;
                viewModel.ProductCategories = productCategories.Collection();

                return View(viewModel);
            }

        }

        [HttpPost]
        public ActionResult Edit(Product product, string ID, HttpPostedFileBase file)
        {

            Product editProduct = context.Find(ID);

            if (editProduct == null)
            {
                return HttpNotFound();
            }
            else
            {

                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                if (file != null)
                {

                    product.Image = product.ID + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Context//ProductImages//" + product.Image));

                }

                editProduct.Category = product.Category;
                editProduct.Description = product.Description;
                editProduct.Name = product.Name;
                editProduct.Price = product.Price;

                context.Commit();

                return RedirectToAction("Index");

            }

        }

        public ActionResult Delete(string ID)
        {

            Product deletedProduct = context.Find(ID);

            if (deletedProduct == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(deletedProduct);
            }

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string ID)
        {

            Product deletedProduct = context.Find(ID);

            if (deletedProduct == null)
            {
                return HttpNotFound();
            }
            else
            {

                context.Delete(ID);
                context.Commit();

                return RedirectToAction("Index");

            }

        }

    }
}