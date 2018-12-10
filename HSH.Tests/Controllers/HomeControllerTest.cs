using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HSH;
using HSH.Controllers;
using HSH.Areas.Admin.Models;
using HSH.Entities;
using HSH.Tests.TestContext;
using System.Threading.Tasks;
using HSH.Models;
using System.ComponentModel.DataAnnotations;

namespace HSH.Tests.Controllers
{
    [TestClass()]
    public class HomeControllerTest
    {
        //[TestMethod()]
        ////public async Task<ActionResult> Index()
        //public async Task Index()

        //{
        //    // Arrange
        //    var LoginModel = new ValidationContext(LoginModel. null, null);
        //   // HomeController controller = new HomeController();

        //    Validator.TryValidateObject(logonModel, validationContext, validationResults, true);
        //    foreach (var validationResult in validationResults)
        //    {
        //        controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
        //    }
        //    // Validate model state end

        //    // Act
        //    ViewResult result = controller.Index() as ViewResult;

        //    // Assert
        //   // Assert.IsNotNull(result);
        //    Assert.AreEqual("Index", result.RouteValues["action"]);
        //}
        //[TestMethod]
        //public void Index()
        //{
        //    // Arrange
        //    HomeController controller = new HomeController();

        //    // Act
        //    ViewResult result = controller.Index() as ViewResult;

        //    // Assert
        //    Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        //}


        //[TestMethod]
        //public void Search()
        //{
        //    // Arrange
        //    HomeController controller = new HomeController();

        //    // Act
        //    ViewResult result = controller.Search() as ViewResult;

        //    // Assert
        //    Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        //}


        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        //[TestMethod()]
        //public void SearchTest()
        //{
        //    //Arrange
        //    HomeController controller = new HomeController();
        //    //Act
        //    ViewResult model result = controller.Search() as ViewResult;
        //    //Assert
        //    Assert.IsNotNull(result);
        //}

        //[TestMethod()]
        //public async Task SearchIndexTest2Async()
        //{
        //    //Arrange
        //    //creating the fake database with propertys, tdb = test database
        //    TestApplicationDbContext tdb = new TestApplicationDbContext();
        //    Property prop1 = new Property
        //    {
        //        Description = "Test Property 1",
        //        Price = 100000
        //    };
        //    Property prop2 = new Property
        //    {
        //        Description = "Test Property 2",
        //        Price = 110000
        //    };
        //    Property prop3 = new Property
        //    {
        //        Description = "Test Property 3",
        //        Price = 150000
        //    };

        //    //adding the three propertys to the "fake" database
        //    tdb.Propertys.Add(prop1);
        //    tdb.Propertys.Add(prop2);
        //    tdb.Propertys.Add(prop3);

        //    PropertySearchModel priceSearch = new PropertySearchModel
        //    {
        //        PriceFrom = 140000
        //    };

        //creating a controller using the test db
        // HomeController controller = new HomeController(tdb);

        //Act
        //call controller searchIndexMethod
        // var viewResult = await controller.SearchIndex(priceSearch) as ViewResult;
        //var result = viewResult.ViewData.Model as IEnumerable<PropertyModel>;

        //Assert
        //Assert.AreEqual(2, result.Count());
    }

    //[TestMethod()]
    //public async Task SearchIndexTest1Async()
    //{
    //    //Arrange
    //    //creating the fake database with vehicles, tdb = test database
    //    TestApplicationDbContext tdb = new TestApplicationDbContext();
    //    Property prop1 = new Property
    //    {
    //        Description = "Test Property 2",
    //        PropertyTypeId = 2,

    //    };
    //    Property prop2 = new Property
    //    {
    //        Description = "Test Property 2",
    //        Price = 110000
    //    };
    //    Property prop3 = new Property
    //    {
    //        Description = "Test Property 3",
    //        Price = 150000
    //    };

    //    //adding the three vehicles to the "fake" database
    //    tdb.Propertys.Add(prop1);
    //    tdb.Propertys.Add(prop2);
    //    tdb.Propertys.Add(prop3);

    //    PropertySearchModel Search = new PropertySearchModel
    //    {
    //        PriceTo = 110000
    //    };

    //    // creating a controller using the test db
    //    HomeController controller = new HomeController(tdb);

    //    //Act
    //    //call controller searchIndexMethod
    //    var viewResult = await controller.SearchIndex(Search) as ViewResult;
    //    var result = viewResult.ViewData.Model as IEnumerable<PropertyModel>;

    //    //Assert
    //    Assert.AreEqual(2, result.Count());
    //}
}

        //[TestMethod()]
        //public void AboutTest()
        //{

        //}

        //[TestMethod()]
        //public void ContactTest()
        //{

        //}

