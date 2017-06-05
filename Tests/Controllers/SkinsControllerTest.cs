#region using

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSpaServices.Controllers;
using WebSpaServices.Models;
using System.Collections.Generic;
using System.Web.Http.Results;
using System.Linq;
using System.Web.Http;
using System.Diagnostics;
using System.Net.Http;
using Moq;

#endregion using

namespace WebSpaServices.Tests.Controllers
{
    [TestClass]
    public class SkinsControllerTest
    {
        #region class members

        SkinsController controller;
        Mock<IRepository<Skin>> mockSkin;
        List<Skin> skins;

        #endregion class members

        #region TestInitialize for test methods

        [TestInitialize]
        public void SetupContext()
        {
            // list of skins
            skins = new List<Skin>() { 
                    new Skin() { Id = 3, SkinColor = "green"},
                    new Skin() { Id = 1, SkinColor = "red" }, 
                    new Skin() { Id = 2, SkinColor = "blue" } 
                };

            // Moq
            mockSkin = new Mock<IRepository<Skin>>();
            mockSkin.Setup(a => a.GetAll())
                .Returns(skins.AsQueryable<Skin>);

            // controller
            controller = new SkinsController(mockSkin.Object);
        }

        #endregion TestInitialize for test methods

        [TestMethod]
        public void GetSkins_Return_NotNullResult()
        {
            // Act
            var actionResult = controller.GetSkins() as IHttpActionResult;

            // Assert
            Assert.IsNotNull(actionResult);
        }

        [TestMethod]
        public void GetSkins_Return_CorrectDomainModel()
        {
            // Act
            IHttpActionResult actionResult = controller.GetSkins();
            var contentResult = actionResult as OkNegotiatedContentResult<IQueryable<Skin>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void GetSkins_Invoke_GetAll()
        {
            // Act
            IHttpActionResult actionResult = controller.GetSkins();

            // Assert
            mockSkin.Verify(m => m.GetAll());
        }

        [TestMethod]
        public void GetSkins_Return_AllSkins()
        {
            // Act
            IHttpActionResult actionResult = controller.GetSkins();
            var contentResult = actionResult as OkNegotiatedContentResult<IQueryable<Skin>>;

            // Assert
            Assert.AreEqual(skins.Count, contentResult.Content.Count());
        }

        [TestMethod]
        public void GetSkins_Return_SameMinSkin()
        {
            // Act
            IHttpActionResult actionResult = controller.GetSkins();
            var contentResult = actionResult as OkNegotiatedContentResult<IQueryable<Skin>>;

            // Assert
            Assert.AreEqual(skins.OrderBy(s => s.SkinColor).Select(s => s.Id).FirstOrDefault(), 
                contentResult.Content.OrderBy(s => s.SkinColor).Select(s => s.Id).FirstOrDefault());

            Assert.AreEqual(skins.OrderBy(s => s.SkinColor).Select(s => s.SkinColor).FirstOrDefault(),
                contentResult.Content.OrderBy(s => s.SkinColor).Select(s => s.SkinColor).FirstOrDefault());
        }

    }
}
