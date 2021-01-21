using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop.Core.Logic;
using Shop.Core.Models;
using Shop.Core.ViewModels;
using Shop.WebUi;
using Shop.WebUi.Controllers;
using Shop.WebUi.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Shop.WebUi.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {

        [TestMethod]
        [TestCategory("Home Controller")]
        public void Index_DoesReturn_Product()
        {
            IRepository<Product> context = new MockContext<Product>();
            IRepository<ProductCategory> CategoryContext = new MockContext<ProductCategory>();
            HomeController controller = new HomeController(context, CategoryContext);

            context.Insert(new Product());

            var result = controller.Index() as ViewResult;
            var viewModel = (ProductListViewModel)result.ViewData.Model;

            Assert.AreEqual(1, viewModel.Products.Count());
            
        }

    }
        
}
