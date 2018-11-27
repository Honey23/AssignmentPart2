using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookStore.Controllers;
using System.Web.Mvc;
using Moq;
using BookStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Tests.Controllers
{
    [TestClass]
    public class itemsControllerTest
    {
        // global variables for multiple test in this class
        itemsController controller;
        Mock<IitemsMock> mock;
        List<item> items;

        [TestInitialize]

        public void TestInitialize()
        {
            // this method runs automatically
            // create a new mock data object to hold fake list data
            mock = new Mock<IitemsMock>();

            //populate mock data
            items = new List<item>
            {
                new item {item_id = 100, item_name = "new book", item_price = 200, item_quantity = 2},
                new item {item_id = 101, item_name = "rent book", item_price = 100, item_quantity = 1},
                new item {item_id = 101, item_name = "old book", item_price = 100, item_quantity = 1}
            };

            //put list into the mock object and pass
            mock.Setup(m => m.items).Returns(items.AsQueryable());
            controller = new itemsController(mock.Object);
        }

        [TestMethod]
        public void IndexLoadsView()
        {
            //now moved to test initialize
            //arrange
            // itemsController controller = new itemsController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            //assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]

        public void IndexReturnitems()
        {
            //act
            var result = (List<item>)((ViewResult)controller.Index()).Model;

            //assert
            CollectionAssert.AreEqual(items, result);
        }
        //GET: Items/Details

        #region
        [TestMethod]
        public void DetailsNoIdLoadsError()
        {
            //act
            ViewResult result = (ViewResult)controller.Details(null);

            //assert
            Assert.AreEqual("Error", result.ViewName);

        }

        [TestMethod]
        public void DetailsInvalidIdLoadsError()
        {
            //act
            ViewResult result = (ViewResult)controller.Details(111);

            //assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]

        public void DetailsValidIdLoadsView()
        {
            //act
            ViewResult result = (ViewResult)controller.Details(100);
            //assert
            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]

        public void DetailsValidIdLoadsitem()
        {
            //act
            //call the method 
            //convert the action result to view result
            //get the view result model
            //cast the model to correct object type
            // this all things we are doing in just one line of code which is below
            item result = (item)((ViewResult)controller.Details(100)).Model;
            //assert
            Assert.AreEqual(items[0], result);
        }
        #endregion

        #region
        //Get: Items/Create
        [TestMethod]
         public void CreateViewLoads()
        {
            //act
            ViewResult result = (ViewResult)controller.Create();
            //assert
            Assert.AreEqual("Create", result.ViewName);
        }
        #endregion

        #region

        // #GET: Items/edit/5

        [TestMethod]
        public void EditInValidId()
        {
            //act
            var result = (ViewResult)controller.Edit(1111);
            //assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]

        public void EditValidId()
        {
            //act
            ViewResult result = (ViewResult)controller.Edit(100);
            //assert
            Assert.AreEqual("Edit", result.ViewName);
        }

        [TestMethod]

        public void EditValidIdLoadsitem()
        {
            //act
            item actual = (item)((ViewResult)controller.Edit(100)).Model;
            //assert
            Assert.AreEqual(items[0], actual);
        }

        [TestMethod]
        public void EditNoIdLoadsError()
        {
            //act
            ViewResult result = (ViewResult)controller.Edit(11);
            //assert
            Assert.AreEqual("Error", result.ViewName);
        }
        #endregion

        #region
//POST:ITEM/EDIT
[TestMethod]
public void EditPostLoadsIndex()
        {
            //act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Edit(items[1]);
            //assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
        [TestMethod]
        public void EditPostInvalidLoadView()
        {
            //arrange
            item invalid = new item { item_id = 56 };
            controller.ModelState.AddModelError("Error", "DO NOT SAVE");
            //act
            ViewResult result = (ViewResult)controller.Edit(invalid);
            //assert
            Assert.AreEqual("Edit", result.ViewName);
        }
        [TestMethod]
        public void EditPostInvalidLoaditem()
        {
            //arrange
            item invalid = new item { item_id = 101 };
            controller.ModelState.AddModelError("Error", "DO NOT SAVE");
            //act
            item result =(item)((ViewResult)controller.Edit(invalid)).Model;
            //assert
            Assert.AreEqual(invalid, result);
        }
        #endregion

        #region
        //POST:item/Create
        [TestMethod]
        public void CreateValiditem()
        {
            //arrange
            item newitem = new item
            {
                item_id = 566,
                item_name = "six",
                item_price = 3,
                item_quantity = 66
            };
            //act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Create(newitem);
            //assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
        [TestMethod]
        public void CreateInvaliditem()
        {
            //arrange
            item invalid = new item();
            //act
            controller.ModelState.AddModelError("Cannot Create", "exception");
            ViewResult result = (ViewResult)controller.Create(invalid);
            //assert
            Assert.AreEqual("Create", result.ViewName);
        }
        #endregion

        #region
        //POST:item/deleteconfirmed
        [TestMethod]
        public void DeleteConfirmedInvalidId()
        {
            //act
            ViewResult result = (ViewResult)controller.DeleteConfirmed(666);
            //assert
            Assert.AreEqual("Error", result.ViewName);
        }
        [TestMethod]
        public void DeleteConfirmedValidId()
        {
            //act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.DeleteConfirmed(100);
            //assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
        #endregion

    }
}
