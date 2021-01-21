using Shop.Core.Logic;
using Shop.Core.Models;
using Shop.DataAccess.InMemory;
using Shop.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.WebUi.Controllers
{
    public class ProductCategoryController : Controller
    {
        IRepository<ProductCategory> context;

        public ProductCategoryController()
        {
            context = new SQLRepository<ProductCategory>(new MyContext());
            //context = new InMemoryRepository<ProductCategory>();

        }
        
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();
            return View(productCategories);
        }

        public ActionResult Create()
        {
            ProductCategory p = new ProductCategory();
            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCategory product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                ProductCategory p = context.FindById(id);
                if (p == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(p);
                }
            }
            catch (Exception)
            {

                return HttpNotFound();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategory product, int id)
        {
            try
            {
                ProductCategory prodToEdit = context.FindById(id);
                if (prodToEdit == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    if (!ModelState.IsValid)
                    {
                        return View(product);
                    }
                    else
                    {
                        //context.Update(product); ce n'est un context EF
                        prodToEdit.Category = product.Category;
                        
                        context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }

            }
            catch (Exception)
            {

                return HttpNotFound();
            }

        }

        public ActionResult Delete(int id)
        {
            try
            {
                ProductCategory p = context.FindById(id);
                if (p == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(p);
                }
            }
            catch (Exception)
            {

                return HttpNotFound();
            }

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirDelete(int id)
        {
            try
            {
                ProductCategory prodToDelete = context.FindById(id);
                if (prodToDelete == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    context.Delete(id);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                return HttpNotFound();
            }

        }

    }
}
