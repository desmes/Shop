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
    public class AchatController : Controller
    {
        IRepository<Product> context;
        List<Product> lstProd = new List<Product>();
        decimal total = 0;


        public AchatController()
        {
            context = new SQLRepository<Product>(new MyContext());
            //context = new InMemoryRepository<Product>();
          
        }
        public ActionResult Ajouter(int id)
        {
            Product p = context.FindById(id);
            if(Session["Products"] == null)
            {
                lstProd.Add(p);
                Session["Products"] = lstProd;
                Session["nbProd"] = 1;
                Session["total"] = p.Price;
            }
            else
            {
                lstProd = (List<Product>)Session["Products"];
                lstProd.Add(p);
                Session["Products"] = lstProd;

                //Total
                foreach (var item in lstProd)
                {
                    total += item.Price;
                }

                Session["total"] = total;
                Session["nbProd"] = lstProd.Count;

            }
            return RedirectToAction("Index", "home");

        }

        public ActionResult Panier()
        {
            lstProd = (List<Product>)Session["Products"];
            return View(lstProd);
        }
    }
}