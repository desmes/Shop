using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop.Core.Logic;
using Shop.Core.Models;
using Shop.Core.ViewModels;
using Shop.WebUi.Controllers;
using Shop.WebUi.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Shop.WebUi.Tests.Controllers
{
    [TestClass]
    public class ProductManagerControllerTests
    {
        IRepository<Product> context;
        IRepository<ProductCategory> CategoryContext;

        [TestInitialize]
        public void Setup()
        {
            context = new MockContext<Product>();
            CategoryContext = new MockContext<ProductCategory>();
        }

        [TestMethod]
        [TestCategory("Product Manager Controller")]
        public void IndexAction_DoesReturn_ListOfProduct()
        {
            //Arrange
            context.Insert(new Product());
            context.Insert(new Product());
            
            //Act
            ProductManagerController controller = new ProductManagerController(context, CategoryContext);
            var result = controller.Index() as ViewResult;
            var viewModel = (List<Product>)result.ViewData.Model;

            //Assert
            Assert.AreEqual(2, viewModel.Count); //En cas d'echec utiliser Count() du System.Linq
            
        }

        [TestMethod]
        [TestCategory("Product Manager Controller")]
        public void CreateAction_DoesReturn_ProductAndListOfCategory()
        {
            //Arrange
            context.Insert(new Product());
            CategoryContext.Insert(new ProductCategory());
            CategoryContext.Insert(new ProductCategory());

            //Act
            ProductManagerController controller = new ProductManagerController(context, CategoryContext);
            var result = controller.Create() as ViewResult;
            var viewModel = (ProductCategoryViewModel)result.ViewData.Model;

            //Assert
            Assert.IsNotNull(viewModel.Product);
            Assert.AreEqual(2, viewModel.ProductCategories.Count());
        }

        [TestMethod]
        [TestCategory("Product Manager Controller")]
        public void CreateWithHttpPostedFile_DoesInsertProductAndImage()
        {
            //Arrange
            string filePath = Path.GetFullPath(@"C:\test\image.jpg");
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            MyFileBase fileImage = new MyFileBase(fileStream, "image/jpeg", "image.jpg");

            //Act
            ProductManagerController controller = new ProductManagerController(context, CategoryContext);
            var result = controller.Create(new Product { Id = 1}, fileImage) as ViewResult;

            //Assert
            Assert.IsNotNull(context.FindById(1));
        }

    }
}
