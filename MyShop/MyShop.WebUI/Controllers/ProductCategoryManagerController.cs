using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAcess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        ProductCategoryRepository context;

        public ProductCategoryManagerController()
        {
            context = new ProductCategoryRepository();
        }

        // GET: Product Category Manager
        public ActionResult Index()
        {

            List<ProductCategory> categories = context.Collection().ToList();

            return View(categories);
        }

        public ActionResult Create()
        {

            ProductCategory category = new ProductCategory();

            return View(category);

        }

        [HttpPost]
        public ActionResult Create(ProductCategory category)
        {

            if (!ModelState.IsValid)
            {

                var errors = ModelState.Values.SelectMany(e => e.Errors);

                return View(category);
            }
            else
            {

                context.Insert(category);
                context.Commit();

                return RedirectToAction("Index");

            }

        }

        public ActionResult Edit(string ID)
        {

            ProductCategory category = context.Find(ID);

            if (category == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(category);
            }

        }

        [HttpPost]
        public ActionResult Edit(ProductCategory category, string ID)
        {

            ProductCategory editCategory = context.Find(ID);

            if (editCategory == null)
            {
                return HttpNotFound();
            }
            else
            {

                if (!ModelState.IsValid)
                {
                    return View(category);
                }

                editCategory.CategoryName = category.CategoryName;

                context.Commit();

                return RedirectToAction("Index");

            }

        }

        public ActionResult Delete(string ID)
        {

            ProductCategory deletedCategory = context.Find(ID);

            if (deletedCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(deletedCategory);
            }

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string ID)
        {

            ProductCategory deletedCategory = context.Find(ID);

            if (deletedCategory == null)
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