﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        public ActionResult Create(Product product)
        {

            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {

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
        public ActionResult Edit(Product product, string ID)
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

                editProduct.Category = product.Category;
                editProduct.Description = product.Description;
                editProduct.Image = product.Image;
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