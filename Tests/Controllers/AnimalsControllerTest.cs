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
using System.Net;
using System.Net.Http;
using Moq;
using Ninject;

#endregion using

namespace WebSpaServices.Tests.Controllers
{
    [TestClass]
    public class AnimalsControllerTest
    {
        #region class members

        AnimalsController controller;

        List<Skin> skins;
        List<Kind> kinds;
        List<Location> locations;
        List<Region> regions;
        List<Animal> animals;

        Mock<IRepository<Skin>> mockSkin;
        Mock<IRepository<Kind>> mockKind;
        Mock<IRepository<Location>> mockLocation;
        Mock<IRepository<Region>> mockRegion;
        Mock<IRepository<Animal>> mockAnimal;

        Mock<IUnitOfWork> mockUoW;

        #endregion class members

        #region TestInitialize for test methods

        [TestInitialize]
        public void SetupContext()
        {
            #region population of animals & its props

            skins = new List<Skin>() { 
                    new Skin() { Id = 1, SkinColor = "red" }, 
                    new Skin() { Id = 2, SkinColor = "blue" },
                    new Skin() { Id = 3, SkinColor = "white" },
                    new Skin() { Id = 4, SkinColor = "black" }
                };

            kinds = new List<Kind>() { 
                    new Kind() { Id = 1, AnimalKind = "fish" }, 
                    new Kind() { Id = 2, AnimalKind = "pet" }, 
                    new Kind() { Id = 3, AnimalKind = "bird" }, 
                    new Kind() { Id = 4, AnimalKind = "reptile" } 
                };

            locations = new List<Location>() { 
                    new Location() { Id = 1, LocationName = "park" }, 
                    new Location() { Id = 2, LocationName = "home" }, 
                    new Location() { Id = 3, LocationName = "farm" }, 
                    new Location() { Id = 4, LocationName = "lake" } 
                };

            regions = new List<Region>() { 
                    new Region() { Id = 1, RegionName = "Asia" }, 
                    new Region() { Id = 2, RegionName = "America" }, 
                    new Region() { Id = 3, RegionName = "Europa" }, 
                    new Region() { Id = 4, RegionName = "Australia" }, 
                    new Region() { Id = 5, RegionName = "Africa" } 
                };

            animals = new List<Animal>() { 

                    new Animal() { AnimalId = 1, 
                        AnimalName = "pig", 
                        SkinId = skins[0].Id, Skin = skins[0],
                        KindId = kinds[2].Id, Kind = kinds[2],
                        LocationId = locations[1].Id,  Location = locations[1],
                        Regions=new List<Region>{regions[0], regions[3]}
                    },
                    new Animal() { AnimalId = 2, 
                        AnimalName = "crocodile", 
                        SkinId = skins[3].Id, Skin = skins[3],
                        KindId = kinds[0].Id, Kind = kinds[0],
                        LocationId = locations[2].Id,  Location = locations[2],
                        Regions=new List<Region>{regions[2], regions[3], regions[4]}
                    },
                    new Animal() { AnimalId = 3, 
                        AnimalName = "bear", 
                        SkinId = skins[1].Id, Skin = skins[1],
                        KindId = kinds[0].Id, Kind = kinds[0],
                        LocationId = locations[3].Id,  Location = locations[3],
                        Regions=new List<Region>{regions[1], regions[4]}
                    },
                    new Animal() { AnimalId = 4, 
                        AnimalName = "horse", 
                        SkinId = skins[3].Id, Skin = skins[3],
                        KindId = kinds[3].Id, Kind = kinds[3],
                        LocationId = locations[2].Id,  Location = locations[2],
                        Regions=new List<Region>{regions[0], regions[1], regions[4]}
                    },                    
                    new Animal() { AnimalId = 5, 
                        AnimalName = "cat", 
                        SkinId = skins[0].Id, Skin = skins[0],
                        KindId = kinds[2].Id, Kind = kinds[2],
                        LocationId = locations[0].Id,  Location = locations[0],
                        Regions=new List<Region>{regions[0], regions[3], regions[4]}
                    },
                    new Animal() { AnimalId = 6, 
                        AnimalName = "swallow", 
                        SkinId = skins[0].Id, Skin = skins[0],
                        KindId = kinds[2].Id, Kind = kinds[2],
                        LocationId = locations[0].Id,  Location = locations[0],
                        Regions=new List<Region>{regions[0], regions[2], regions[3], regions[4]}
                    },
                    new Animal() { AnimalId = 7, 
                        AnimalName = "fox", 
                        SkinId = skins[3].Id, Skin = skins[3],
                        KindId = kinds[3].Id, Kind = kinds[3],
                        LocationId = locations[2].Id,  Location = locations[2],
                        Regions=new List<Region>{regions[0], regions[1], regions[4]}
                    },                    
                    new Animal() { AnimalId = 8, 
                        AnimalName = "monkey", 
                        SkinId = skins[0].Id, Skin = skins[0],
                        KindId = kinds[2].Id, Kind = kinds[2],
                        LocationId = locations[0].Id,  Location = locations[0],
                        Regions=new List<Region>{regions[0], regions[1], regions[3], regions[4]}
                    },
                    new Animal() { AnimalId = 9, 
                        AnimalName = "kengoo", 
                        SkinId = skins[0].Id, Skin = skins[0],
                        KindId = kinds[2].Id, Kind = kinds[2],
                        LocationId = locations[0].Id,  Location = locations[0],
                        Regions=new List<Region>{regions[0], regions[3], regions[4]}
                    },                    
                    new Animal() { AnimalId = 10, 
                        AnimalName = "hawk", 
                        SkinId = skins[2].Id, Skin = skins[2],
                        KindId = kinds[3].Id, Kind = kinds[3],
                        LocationId = locations[0].Id,  Location = locations[0],
                        Regions=new List<Region>{regions[1], regions[2], regions[3]}
                    }
                };

            #endregion population of animals & its props

            #region Setup MOQs

            mockSkin = new Mock<IRepository<Skin>>();
            mockSkin.Setup(a => a.GetAll())
                .Returns(skins.AsQueryable<Skin>);

            mockKind = new Mock<IRepository<Kind>>();
            mockKind.Setup(a => a.GetAll())
                .Returns(kinds.AsQueryable<Kind>);

            mockLocation = new Mock<IRepository<Location>>();
            mockLocation.Setup(a => a.GetAll())
                .Returns(locations.AsQueryable<Location>);

            mockRegion = new Mock<IRepository<Region>>();
            mockRegion.Setup(a => a.GetAll())
                .Returns(regions.AsQueryable<Region>);

            mockAnimal = new Mock<IRepository<Animal>>();
            mockAnimal.Setup(a => a.GetAll())
                .Returns(animals.AsQueryable<Animal>);

            #endregion Setup MOQs

            #region Setup mockUoW

            mockUoW = new Mock<IUnitOfWork>();

            mockUoW.Setup<IRepository<Skin>>(m => m.Skins).Returns(mockSkin.Object);
            mockUoW.Setup<IRepository<Kind>>(m => m.Kinds).Returns(mockKind.Object);
            mockUoW.Setup<IRepository<Location>>(m => m.Locations).Returns(mockLocation.Object);
            mockUoW.Setup<IRepository<Region>>(m => m.Regions).Returns(mockRegion.Object);
            mockUoW.Setup<IRepository<Animal>>(m => m.Animals).Returns(mockAnimal.Object);

            #endregion Setup mockUoW

            controller = new AnimalsController(mockUoW.Object);
        }

        #endregion TestInitialize for test methods

        //
        //-----------------------------------     Tests for GetAnimals()   ----------------------------------------
        //
        #region Tests for GetAnimals()

        [TestMethod]
        public void GetAnimals_Return_NotNullResult()
        {
            // Act
            var actionResult = controller.GetAnimals() as IHttpActionResult;

            // Assert
            Assert.IsNotNull(actionResult);
        }

        [TestMethod]
        public void GetAnimals_Return_CorrectDomainModel()
        {
            // Act
            IHttpActionResult actionResult = controller.GetAnimals();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<AnimalLight>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }
        [TestMethod]

        public void GetAnimals_Invoke_Animals_GetAll()
        {
            // Act
            IHttpActionResult actionResult = controller.GetAnimals();

            // Assert
            mockAnimal.Verify(m => m.GetAll());
        }


        [TestMethod]
        public void GetAnimals_Return_AllAnimals()
        {
            // Act
            IHttpActionResult actionResult = controller.GetAnimals();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<AnimalLight>>;

            // Assert
            Assert.AreEqual(animals.Count, contentResult.Content.Count());
        }

        [TestMethod]
        public void GetAnimals_Return_SameFirstAnimal()
        {
            // Act
            IHttpActionResult actionResult = controller.GetAnimals();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<AnimalLight>>;

            Animal animal = animals.OrderBy(a => a.AnimalName).FirstOrDefault();

            // Assert
            Assert.AreEqual(animal.AnimalId, contentResult.Content.Select(s => s.AnimalId).FirstOrDefault());
            Assert.AreEqual(animal.AnimalName, contentResult.Content.Select(s => s.AnimalName).FirstOrDefault());
            Assert.AreEqual(animal.SkinId, contentResult.Content.Select(s => s.SkinId).FirstOrDefault());
            Assert.AreEqual(animal.KindId, contentResult.Content.Select(s => s.KindId).FirstOrDefault());
            Assert.AreEqual(animal.LocationId, contentResult.Content.Select(s => s.LocationId).FirstOrDefault());
            Assert.AreEqual(animal.Regions.Count, contentResult.Content.Select(s => s.RegIds).FirstOrDefault().Count());

            foreach (var id in animal.Regions.Select(m => m.Id))
            {
                int count = contentResult.Content.Where(m => m.RegIds.Contains(id)).Count();
                Assert.IsTrue(count > 0);
            }
        }

        #endregion Tests for GetAnimals()

        //
        //-----------------------------------     Tests for GetAnimal()   ----------------------------------------
        //
        #region Tests for GetAnimal()

        [TestMethod]
        public void GetAnimal_Return_NotNullResult()
        {
            // Arrange
            Animal animal = animals.OrderBy(a => a.AnimalName).FirstOrDefault();

            // Act
            var actionResult = controller.GetAnimal(animal.AnimalId) as IHttpActionResult;

            // Assert
            Assert.IsNotNull(actionResult);
        }

        [TestMethod]
        public void GetAnimal_Return_CorrectDomainModel()
        {
            // Arrange
            Animal animal = animals.OrderBy(a => a.AnimalName).FirstOrDefault();

            // Act
            IHttpActionResult actionResult = controller.GetAnimal(animal.AnimalId);
            var contentResult = actionResult as OkNegotiatedContentResult<AnimalLight>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }
        [TestMethod]

        public void GetAnimal_Invoke_Animals_GetAll()
        {
            // Arrange
            Animal animal = animals.OrderBy(a => a.AnimalName).FirstOrDefault();

            // Act
            IHttpActionResult actionResult = controller.GetAnimal(animal.AnimalId);

            // Assert
            mockAnimal.Verify(m => m.GetAll());
        }


        [TestMethod]
        public void GetAnimal_Return_NotFindAnimal()
        {
            // Arrange
            int id = animals.Max(m => m.AnimalId) + 1;


            // Act
            IHttpActionResult actionResult = controller.GetAnimal(id);
            var contentResult = actionResult as NotFoundResult;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetAnimal_Return_SameAnimal()
        {
            // Arrange
            Animal animal = animals.OrderBy(a => a.AnimalName).FirstOrDefault();

            // Act
            IHttpActionResult actionResult = controller.GetAnimal(animal.AnimalId);
            var contentResult = actionResult as OkNegotiatedContentResult<AnimalLight>;

            // Assert
            Assert.AreEqual(animal.AnimalId, contentResult.Content.AnimalId);
            Assert.AreEqual(animal.AnimalName, contentResult.Content.AnimalName);
            Assert.AreEqual(animal.SkinId, contentResult.Content.SkinId);
            Assert.AreEqual(animal.KindId, contentResult.Content.KindId);
            Assert.AreEqual(animal.LocationId, contentResult.Content.LocationId);
            Assert.AreEqual(animal.Regions.Count, contentResult.Content.RegIds.Count());

            foreach (var id in animal.Regions.Select(m => m.Id))
            {
                Assert.IsTrue(contentResult.Content.RegIds.Contains(id));
            }
        }

        #endregion Tests for GetAnimal()


        //
        //-----------------------------------     Tests for CreateAnimal()   ----------------------------------------
        //
        #region Tests for CreateAnimal()

        AnimalLight NewAnimalLight()
        {
            Animal animal = animals.OrderBy(a => a.AnimalName).FirstOrDefault();

            AnimalLight animalLight = new AnimalLight()
            {
                AnimalId = 0,
                AnimalName = "new animal",
                SkinId = animal.SkinId,
                KindId = animal.KindId,
                LocationId = animal.LocationId,
                RegIds = animal.Regions.Select(m => m.Id).ToArray<int>()
            };
            return animalLight;
        }

        [TestMethod]
        public void CreateAnimal_Return_NotNullResult()
        {
            // Arrange
            AnimalLight animalLight = NewAnimalLight();

            // Act
            var actionResult = controller.CreateAnimal(animalLight) as IHttpActionResult;

            // Assert
            Assert.IsNotNull(actionResult);
        }

        [TestMethod]
        public void CreateAnimal_Return_CorrectDomainModel()
        {
            // Arrange
            AnimalLight animalLight = NewAnimalLight();

            // Act
            var actionResult = controller.CreateAnimal(animalLight) as IHttpActionResult;
            var contentResult = actionResult as CreatedAtRouteNegotiatedContentResult<AnimalLight>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void CreateAnimal_Return_CorrectRoute()
        {
            // Arrange
            AnimalLight animalLight = NewAnimalLight();

            // Act
            var actionResult = controller.CreateAnimal(animalLight) as IHttpActionResult;
            var contentResult = actionResult as CreatedAtRouteNegotiatedContentResult<AnimalLight>;

            // Assert
            Assert.AreEqual("DefaultApi", contentResult.RouteName);
            Assert.AreEqual(animalLight.AnimalId, contentResult.RouteValues["id"]);
        }


        [TestMethod]
        public void CreateAnimal_Invoke_Animals_GetAll()
        {
            // Arrange
            AnimalLight animalLight = NewAnimalLight();

            // Act
            var actionResult = controller.CreateAnimal(animalLight) as IHttpActionResult;

            // Assert
            mockAnimal.Verify(m => m.GetAll());
        }

        [TestMethod]
        public void CreateAnimal_Invoke_Regions_GetAll()
        {
            // Arrange
            AnimalLight animalLight = NewAnimalLight();

            // Act
            var actionResult = controller.CreateAnimal(animalLight) as IHttpActionResult;

            // Assert
            mockRegion.Verify(m => m.GetAll());
        }

        [TestMethod]
        public void CreateAnimal_Invoke_Create()
        {
            // Arrange
            AnimalLight animalLight = NewAnimalLight();

            // Act
            var actionResult = controller.CreateAnimal(animalLight) as IHttpActionResult;

            // Assert
            mockAnimal.Verify(m => m.Create(It.Is<Animal>(a => a.AnimalId == animalLight.AnimalId 
                && a.AnimalName == animalLight.AnimalName
                && a.KindId == animalLight.KindId
                && a.LocationId == animalLight.LocationId
                && a.SkinId == animalLight.SkinId
                && a.Regions.Count() == animalLight.RegIds.Count)));

            mockUoW.Verify(m => m.Save());
        }


        [TestMethod]
        public void CreateAnimal_Return_BadRequest()
        {
            // Act
            var actionResult = controller.CreateAnimal(null) as IHttpActionResult;
            var contentResult = actionResult as BadRequestResult;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void CreateAnimal_Fail_DublicatedName()
        {
            // Arrange
            AnimalLight animalLight = NewAnimalLight();
            Animal animal = animals.OrderBy(a => a.AnimalName).FirstOrDefault();
            animalLight.AnimalName = animal.AnimalName;

            // ModelState.AddModelError("animalLight", "Ошибка: животное с таким названием - уже существует в списке животных!");
            //
            string key = "animalLight";
            string msg = "Ошибка: животное с таким названием - уже существует в списке животных!";

            // Act
            var actionResult = controller.CreateAnimal(animalLight) as IHttpActionResult;
            var contentResult = actionResult as InvalidModelStateResult;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsInstanceOfType(actionResult, typeof(InvalidModelStateResult));
            Assert.IsNotNull(contentResult.ModelState);
            Assert.IsNotNull(contentResult.ModelState[key]);
            Assert.IsNotNull(contentResult.ModelState[key].Errors);
            Assert.IsNotNull(contentResult.ModelState[key].Errors[0]);
            Assert.AreEqual(msg, contentResult.ModelState[key].Errors[0].ErrorMessage);
        }
   
        [TestMethod]
        public void CreateAnimal_Fail_ShortName()
        {
            // Arrange
            AnimalLight animalLight = NewAnimalLight();
            animalLight.AnimalName = "A";

            // ModelState.AddModelError("animalLight", "Ошибка: название животного должно содержать от 2-х до 100 символов!");
            //
            string key = "animalLight";
            string msg = "Ошибка: название животного должно содержать от 2-х до 100 символов!";

            // Act
            var actionResult = controller.CreateAnimal(animalLight) as IHttpActionResult;
            var contentResult = actionResult as InvalidModelStateResult;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsInstanceOfType(actionResult, typeof(InvalidModelStateResult));
            Assert.IsNotNull(contentResult.ModelState);
            Assert.IsNotNull(contentResult.ModelState[key]);
            Assert.IsNotNull(contentResult.ModelState[key].Errors);
            Assert.IsNotNull(contentResult.ModelState[key].Errors[0]);
            Assert.AreEqual(msg, contentResult.ModelState[key].Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void CreateAnimal_Fail_LongName()
        {
            // Arrange
            AnimalLight animalLight = NewAnimalLight();
            animalLight.AnimalName = new String('S', 101);

            // ModelState.AddModelError("animalLight", "Ошибка: название животного должно содержать от 2-х до 100 символов!");
            //
            string key = "animalLight";
            string msg = "Ошибка: название животного должно содержать от 2-х до 100 символов!";

            // Act
            var actionResult = controller.CreateAnimal(animalLight) as IHttpActionResult;
            var contentResult = actionResult as InvalidModelStateResult;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsInstanceOfType(actionResult, typeof(InvalidModelStateResult));
            Assert.IsNotNull(contentResult.ModelState);
            Assert.IsNotNull(contentResult.ModelState[key]);
            Assert.IsNotNull(contentResult.ModelState[key].Errors);
            Assert.IsNotNull(contentResult.ModelState[key].Errors[0]);
            Assert.AreEqual(msg, contentResult.ModelState[key].Errors[0].ErrorMessage);
        }


        [TestMethod]
        public void CreateAnimal_Return_NewAnimal()
        {
            // Arrange
            AnimalLight animalLight = NewAnimalLight();

            // Act
            var actionResult = controller.CreateAnimal(animalLight) as IHttpActionResult;
            var contentResult = actionResult as CreatedAtRouteNegotiatedContentResult<AnimalLight>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);

            // Assert
            Assert.AreEqual(animalLight.AnimalName, contentResult.Content.AnimalName);
            Assert.AreEqual(animalLight.SkinId, contentResult.Content.SkinId);
            Assert.AreEqual(animalLight.KindId, contentResult.Content.KindId);
            Assert.AreEqual(animalLight.LocationId, contentResult.Content.LocationId);
            Assert.AreEqual(animalLight.RegIds.Count(), contentResult.Content.RegIds.Count());

            foreach(var id in animalLight.RegIds)
            {
                Assert.IsTrue(contentResult.Content.RegIds.Contains(id));
            }
        }

        #endregion Tests for CreateAnimal()

        //
        //-----------------------------------     Tests for EditAnimal()   ----------------------------------------
        //
        #region Tests for EditAnimal()

        AnimalLight EditedAnimalLight()
        {
            Animal animal = animals.OrderBy(a => a.AnimalName).LastOrDefault();
            Animal animal_first = animals.OrderBy(a => a.AnimalName).FirstOrDefault();

            AnimalLight animalLight = new AnimalLight()
            {
                AnimalId = animal.AnimalId,
                AnimalName = "swan",
                SkinId = animal_first.SkinId,
                KindId = animal_first.KindId,
                LocationId = animal_first.LocationId,
                RegIds = animal_first.Regions.Select(m => m.Id).ToArray<int>()
            };

            return animalLight;
        }

        [TestMethod]
        public void EditAnimal_Return_NotNullResult()
        {
            // Arrange
            AnimalLight animalLight = EditedAnimalLight();

            // Act
            var actionResult = controller.EditAnimal(animalLight.AnimalId, animalLight) as IHttpActionResult;

            // Assert
            Assert.IsNotNull(actionResult);
        }

        [TestMethod]
        public void EditAnimal_Return_CorrectDomainModel()
        {
            // Arrange
            AnimalLight animalLight = EditedAnimalLight();

            // Act
            var actionResult = controller.EditAnimal(animalLight.AnimalId, animalLight) as IHttpActionResult;
            var contentResult = actionResult as NegotiatedContentResult<AnimalLight>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void EditAnimal_Return_CorrectContentResult()
        {
            // Arrange
            AnimalLight animalLight = EditedAnimalLight();

            // Act
            var actionResult = controller.EditAnimal(animalLight.AnimalId, animalLight) as IHttpActionResult;
            var contentResult = actionResult as NegotiatedContentResult<AnimalLight>;

            // Assert
            Assert.AreEqual(HttpStatusCode.Accepted, contentResult.StatusCode);
            Assert.AreEqual(animalLight.AnimalId, contentResult.Content.AnimalId);
        }


        [TestMethod]
        public void EditAnimal_Invoke_Animals_GetAll()
        {
            // Arrange
            AnimalLight animalLight = EditedAnimalLight();

            // Act
            var actionResult = controller.EditAnimal(animalLight.AnimalId, animalLight) as IHttpActionResult;

            // Assert
            mockAnimal.Verify(m => m.GetAll());
        }

        [TestMethod]
        public void EditAnimal_Invoke_Regions_GetAll()
        {
            // Arrange
            AnimalLight animalLight = EditedAnimalLight();

            // Act
            var actionResult = controller.EditAnimal(animalLight.AnimalId, animalLight) as IHttpActionResult;

            // Assert
            mockRegion.Verify(m => m.GetAll());
        }

        [TestMethod]
        public void EditAnimal_Invoke_Skins_GetAll()
        {
            // Arrange
            AnimalLight animalLight = EditedAnimalLight();

            // Act
            var actionResult = controller.EditAnimal(animalLight.AnimalId, animalLight) as IHttpActionResult;

            // Assert
            mockSkin.Verify(m => m.GetAll());
        }

        [TestMethod]
        public void EditAnimal_Invoke_Kinds_GetAll()
        {
            // Arrange
            AnimalLight animalLight = EditedAnimalLight();

            // Act
            var actionResult = controller.EditAnimal(animalLight.AnimalId, animalLight) as IHttpActionResult;

            // Assert
            mockKind.Verify(m => m.GetAll());
        }

        [TestMethod]
        public void EditAnimal_Invoke_Locations_GetAll()
        {
            // Arrange
            AnimalLight animalLight = EditedAnimalLight();

            // Act
            var actionResult = controller.EditAnimal(animalLight.AnimalId, animalLight) as IHttpActionResult;

            // Assert
            mockLocation.Verify(m => m.GetAll());
        }

        [TestMethod]
        public void EditAnimal_Invoke_Update()
        {
            // Arrange
            AnimalLight animalLight = EditedAnimalLight();

            // Act
            var actionResult = controller.EditAnimal(animalLight.AnimalId, animalLight) as IHttpActionResult;

            // Assert
            mockAnimal.Verify(m => m.Update(It.Is<Animal>(a => a.AnimalId == animalLight.AnimalId)));
            mockUoW.Verify(m => m.Save());
        }


        [TestMethod]
        public void EditAnimal_Return_BadRequest()
        {
            // Arrange
            AnimalLight animalLight = EditedAnimalLight();

            // Act
            var actionResult = controller.EditAnimal(animalLight.AnimalId, null) as IHttpActionResult;
            var contentResult = actionResult as BadRequestResult;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void EditAnimal_Fail_DublicatedName()
        {
            // Arrange
            AnimalLight animalLight = EditedAnimalLight();
            Animal animal = animals.OrderBy(a => a.AnimalName).FirstOrDefault();
            animalLight.AnimalName = animal.AnimalName; //  "Bear";

            // ModelState.AddModelError("animalLight", "Ошибка: другое животное с таким названием - уже существует в списке животных!");
            //
            string key = "animalLight";
            string msg = "Ошибка: другое животное с таким названием - уже существует в списке животных!";

            // Act
            var actionResult = controller.EditAnimal(animalLight.AnimalId, animalLight) as IHttpActionResult;
            var contentResult = actionResult as InvalidModelStateResult;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsInstanceOfType(actionResult, typeof(InvalidModelStateResult));
            Assert.IsNotNull(contentResult.ModelState);
            Assert.IsNotNull(contentResult.ModelState[key]);
            Assert.IsNotNull(contentResult.ModelState[key].Errors);
            Assert.IsNotNull(contentResult.ModelState[key].Errors[0]);
            Assert.AreEqual(msg, contentResult.ModelState[key].Errors[0].ErrorMessage);
        }
   
        [TestMethod]
        public void EditAnimal_Fail_ShortName()
        {
            // Arrange
            AnimalLight animalLight = EditedAnimalLight();
            animalLight.AnimalName = "A";

            // ModelState.AddModelError("animalLight", "Ошибка: название животного должно содержать от 2-х до 100 символов!");
            //
            string key = "animalLight";
            string msg = "Ошибка: название животного должно содержать от 2-х до 100 символов!";

            // Act
            var actionResult = controller.EditAnimal(animalLight.AnimalId, animalLight) as IHttpActionResult;
            var contentResult = actionResult as InvalidModelStateResult;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsInstanceOfType(actionResult, typeof(InvalidModelStateResult));
            Assert.IsNotNull(contentResult.ModelState);
            Assert.IsNotNull(contentResult.ModelState[key]);
            Assert.IsNotNull(contentResult.ModelState[key].Errors);
            Assert.IsNotNull(contentResult.ModelState[key].Errors[0]);
            Assert.AreEqual(msg, contentResult.ModelState[key].Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void EditAnimal_Fail_LongName()
        {
            // Arrange
            AnimalLight animalLight = EditedAnimalLight();
            animalLight.AnimalName = new String('s', 101);

            // ModelState.AddModelError("animalLight", "Ошибка: название животного должно содержать от 2-х до 100 символов!");
            //
            string key = "animalLight";
            string msg = "Ошибка: название животного должно содержать от 2-х до 100 символов!";

            // Act
            var actionResult = controller.EditAnimal(animalLight.AnimalId, animalLight) as IHttpActionResult;
            var contentResult = actionResult as InvalidModelStateResult;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsInstanceOfType(actionResult, typeof(InvalidModelStateResult));
            Assert.IsNotNull(contentResult.ModelState);
            Assert.IsNotNull(contentResult.ModelState[key]);
            Assert.IsNotNull(contentResult.ModelState[key].Errors);
            Assert.IsNotNull(contentResult.ModelState[key].Errors[0]);
            Assert.AreEqual(msg, contentResult.ModelState[key].Errors[0].ErrorMessage);
        }


        [TestMethod]
        public void EditAnimal_Return_CorrectUpdatedAnimal()
        {
            // Arrange
            AnimalLight animalLight = EditedAnimalLight();

            // Act
            var actionResult = controller.EditAnimal(animalLight.AnimalId, animalLight) as IHttpActionResult;
            var contentResult = actionResult as NegotiatedContentResult<AnimalLight>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);

            // Assert
            Assert.AreEqual(animalLight.AnimalId, contentResult.Content.AnimalId);
            Assert.AreEqual(animalLight.AnimalName, contentResult.Content.AnimalName);
            Assert.AreEqual(animalLight.SkinId, contentResult.Content.SkinId);
            Assert.AreEqual(animalLight.KindId, contentResult.Content.KindId);
            Assert.AreEqual(animalLight.LocationId, contentResult.Content.LocationId);
            Assert.AreEqual(animalLight.RegIds.Count(), contentResult.Content.RegIds.Count());

            foreach(var id in animalLight.RegIds)
            {
                Assert.IsTrue(contentResult.Content.RegIds.Contains(id));
            }
        }

        #endregion Tests for EditAnimal()

        //
        //-----------------------------------     Tests for DeleteAnimal()   ----------------------------------------
        //
        #region Tests for DeleteAnimal()

        AnimalLight DeletedAnimalLight()
        {
            Animal animal = animals.OrderBy(a => a.AnimalName).FirstOrDefault();

            AnimalLight animalLight = new AnimalLight
            {
                AnimalId = animal.AnimalId,
                AnimalName = animal.AnimalName,
                SkinId = animal.SkinId,
                KindId = animal.KindId,
                LocationId = animal.LocationId,
                RegIds = animal.Regions.Select(m => m.Id).ToArray<int>()
            };
            return animalLight;
        }

        [TestMethod]
        public void DeleteAnimal_Return_NotNullResult()
        {
            // Arrange
            int id = DeletedAnimalLight().AnimalId;

            // Act
            var actionResult = controller.DeleteAnimal(id) as IHttpActionResult;

            // Assert
            Assert.IsNotNull(actionResult);
        }

        [TestMethod]
        public void DeleteAnimal_Return_CorrectDomainModel()
        {
            // Arrange
            int id = DeletedAnimalLight().AnimalId;

            // Act
            IHttpActionResult actionResult = controller.DeleteAnimal(id);
            var contentResult = actionResult as OkNegotiatedContentResult<AnimalLight>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(id, contentResult.Content.AnimalId);
        }

        [TestMethod]
        public void DeleteAnimal_Invoke_Animals_GetAll()
        {
            // Arrange
            int id = DeletedAnimalLight().AnimalId;

            // Act
            IHttpActionResult actionResult = controller.DeleteAnimal(id);

            // Assert
            mockAnimal.Verify(m => m.GetAll());
        }


        [TestMethod]
        public void DeleteAnimal_Return_NotFoundResult()
        {
            // Arrange
            int id = animals.Max(m => m.AnimalId) + 1;

            // Act
            IHttpActionResult actionResult = controller.DeleteAnimal(id);
            var contentResult = actionResult as NotFoundResult;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
        [TestMethod]
        public void DeleteAnimal_Invoke_Delete()
        {
            // Arrange
            int id = DeletedAnimalLight().AnimalId;

            // Act
            IHttpActionResult actionResult = controller.DeleteAnimal(id);

            // Assert
            mockAnimal.Verify(m => m.Delete(DeletedAnimalLight().AnimalId));
            mockUoW.Verify(m => m.Save());
        }


        [TestMethod]
        public void DeleteAnimal_Return_CorrectDeletedAnimal()
        {
            // Arrange
            int id = DeletedAnimalLight().AnimalId;

            // Act
            IHttpActionResult actionResult = controller.DeleteAnimal(id);
            var contentResult = actionResult as OkNegotiatedContentResult<AnimalLight>;

            // Assert
            Assert.AreEqual(DeletedAnimalLight().AnimalId, contentResult.Content.AnimalId);
            Assert.AreEqual(DeletedAnimalLight().AnimalName, contentResult.Content.AnimalName);
            Assert.AreEqual(DeletedAnimalLight().SkinId, contentResult.Content.SkinId);
            Assert.AreEqual(DeletedAnimalLight().KindId, contentResult.Content.KindId);
            Assert.AreEqual(DeletedAnimalLight().LocationId, contentResult.Content.LocationId);
            Assert.AreEqual(DeletedAnimalLight().RegIds.Count, contentResult.Content.RegIds.Count());
        }

        #endregion Tests for DeleteAnimal()



        //
        //-----------------------------------     Tests for FilterAnimals()   ----------------------------------------
        //
        #region Tests for FilterAnimals()

        AnimalsFilter GetFilter()
        {
            AnimalsFilter filter = new AnimalsFilter()
            {
                KindId = kinds[0].Id,  // 1
                SkinId = skins[3].Id,  // 4
                RegIds = new int[] { regions[2].Id, regions[3].Id, regions[4].Id}  //  3, 4, 5
            };
            return filter;
        }

        [TestMethod]
        public void FilterAnimals_Return_NotNullResult()
        {
            // Arrange
            AnimalsFilter filter = GetFilter();

            // Act
            var actionResult = controller.FilterAnimals(filter) as IHttpActionResult;

            // Assert
            Assert.IsNotNull(actionResult);
        }

        [TestMethod]
        public void FilterAnimals_Return_CorrectDomainModel()
        {
            // Arrange
            AnimalsFilter filter = GetFilter();

            // Act
            var actionResult = controller.FilterAnimals(filter) as IHttpActionResult;
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<AnimalLight>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void FilterAnimals_Invoke_Animals_GetAll()
        {
            // Arrange
            AnimalsFilter filter = GetFilter();

            // Act
            var actionResult = controller.FilterAnimals(filter) as IHttpActionResult;

            // Assert
            mockAnimal.Verify(m => m.GetAll());
        }

        [TestMethod]
        public void FilterAnimals_Return_BadRequest()
        {
            // Act
            var actionResult = controller.FilterAnimals(null) as IHttpActionResult;
            var contentResult = actionResult as BadRequestResult;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void FilterAnimals_FilteredBy_NoFilter()
        {
            // Arrange
            AnimalsFilter filter = GetFilter();
            filter.KindId = 0;
            filter.SkinId = 0;
            filter.RegIds = new[] { 0 };

            // Act
            var actionResult = controller.FilterAnimals(filter) as IHttpActionResult;
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<AnimalLight>>;

            // Assert
            Assert.AreEqual(animals.Count, contentResult.Content.Count());
        }

        [TestMethod]
        public void FilterAnimals_FilteredBy_Kind()
        {
            // Arrange
            AnimalsFilter filter = GetFilter();
            filter.SkinId = 0;
            filter.RegIds = new[] { 0 };

            // Act
            var actionResult = controller.FilterAnimals(filter) as IHttpActionResult;
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<AnimalLight>>;

            // Assert
            int count = animals.Where(a => a.KindId == filter.KindId).Count();
            Assert.AreEqual(count, contentResult.Content.Count());
        }

        [TestMethod]
        public void FilterAnimals_FilteredBy_Skin()
        {
            // Arrange
            AnimalsFilter filter = GetFilter();
            filter.KindId = 0;
            filter.RegIds = new[] { 0 };

            // Act
            var actionResult = controller.FilterAnimals(filter) as IHttpActionResult;
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<AnimalLight>>;

            // Assert
            int count = animals.Where(a => a.SkinId == filter.SkinId).Count();
            Assert.AreEqual(count, contentResult.Content.Count());
        }

        [TestMethod]
        public void FilterAnimals_FilteredBy_SkinAndKind()
        {
            // Arrange
            AnimalsFilter filter = GetFilter();
            filter.RegIds = new[] { 0 }; // new[] { 1 };

            // Act
            var actionResult = controller.FilterAnimals(filter) as IHttpActionResult;
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<AnimalLight>>;

            // Assert
            var filtered = animals.Where(a => a.KindId == filter.KindId && a.SkinId == filter.SkinId);
            Assert.AreEqual(filtered.Count(), contentResult.Content.Count());
        }

        [TestMethod]
        public void FilterAnimals_FilteredBy_Region()
        {
            // Arrange
            AnimalsFilter filter = GetFilter();
            filter.KindId = 0;
            filter.SkinId = 0;
            filter.RegIds = new[] { regions[1].Id }; // new[] { 0 };

            // Act
            var actionResult = controller.FilterAnimals(filter) as IHttpActionResult;
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<AnimalLight>>;

            // Assert
            var filtered = animals.Where(a => a.Regions.Any(m => filter.RegIds.Contains(m.Id)));

            Assert.AreEqual(filtered.Count(), contentResult.Content.Count());
        }

        [TestMethod]
        public void FilterAnimals_FilteredBy_ManyRegions()
        {
            // Arrange
            AnimalsFilter filter = GetFilter();
            filter.KindId = 0;
            filter.SkinId = 0;
            filter.RegIds = new[] { regions[1].Id, regions[2].Id }; // new[] { 0 };

            // Act
            var actionResult = controller.FilterAnimals(filter) as IHttpActionResult;
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<AnimalLight>>;

            // Assert
            var filtered = animals.Where(a => a.Regions.Any(m => filter.RegIds.Contains(m.Id)));

            Assert.AreEqual(filtered.Count(), contentResult.Content.Count());
        }

        [TestMethod]
        public void FilterAnimals_FilteredBy_KindAndRegion()
        {
            // Arrange
            AnimalsFilter filter = GetFilter();
            filter.SkinId = 0;
            filter.RegIds = new[] { regions[4].Id };  // new[] { 0 };

            // Act
            var actionResult = controller.FilterAnimals(filter) as IHttpActionResult;
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<AnimalLight>>;

            // Assert
            var filtered = animals.Where(a => a.KindId == filter.KindId && a.Regions.Any(m => filter.RegIds.Contains(m.Id)));

            Assert.AreEqual(filtered.Count(), contentResult.Content.Count());
        }

        [TestMethod]
        public void FilterAnimals_FilteredBy_KindAndManyRegions()
        {
            // Arrange
            AnimalsFilter filter = GetFilter();
            filter.SkinId = 0;
            filter.RegIds = new[] { regions[1].Id, regions[4].Id }; // new[] { 0 };

            // Act
            var actionResult = controller.FilterAnimals(filter) as IHttpActionResult;
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<AnimalLight>>;

            // Assert
            var filtered = animals.Where(a => a.KindId == filter.KindId && a.Regions.Any(m => filter.RegIds.Contains(m.Id)));

            Assert.AreEqual(filtered.Count(), contentResult.Content.Count());
        }

        [TestMethod]
        public void FilterAnimals_FilteredBy_SkinAndRegion()
        {
            // Arrange
            AnimalsFilter filter = GetFilter();
            filter.SkinId = 0;
            filter.RegIds = new[] { regions[1].Id }; // new[] { 0 };

            // Act
            var actionResult = controller.FilterAnimals(filter) as IHttpActionResult;
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<AnimalLight>>;

            // Assert
            var filtered = animals.Where(a => a.KindId == filter.KindId && a.Regions.Any(m => filter.RegIds.Contains(m.Id)));

            Assert.AreEqual(filtered.Count(), contentResult.Content.Count());
        }

        [TestMethod]
        public void FilterAnimals_FilteredBy_SkinAndManyRegions()
        {
            // Arrange
            AnimalsFilter filter = GetFilter();
            filter.KindId = 0;
            filter.RegIds = new[] { regions[1].Id, regions[4].Id }; // new[] { 0 };

            // Act
            var actionResult = controller.FilterAnimals(filter) as IHttpActionResult;
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<AnimalLight>>;

            // Assert
            var filtered = animals.Where(a => a.SkinId == filter.SkinId && a.Regions.Any(m => filter.RegIds.Contains(m.Id)));

            Assert.AreEqual(filtered.Count(), contentResult.Content.Count());
        }


        [TestMethod]
        public void FilterAnimals_FilteredBy_KindAndSkinAndRegion()
        {
            // Arrange
            AnimalsFilter filter = GetFilter();
            filter.KindId = kinds[0].Id;
            filter.SkinId = skins[3].Id;
            filter.RegIds = new[] { regions[2].Id }; // new[] { 0 };

            // Act
            var actionResult = controller.FilterAnimals(filter) as IHttpActionResult;
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<AnimalLight>>;

            // Assert
            var filtered = animals.Where(a => a.KindId == filter.KindId && a.SkinId == filter.SkinId && a.Regions.Any(m => filter.RegIds.Contains(m.Id)));

            Assert.AreEqual(filtered.Count(), contentResult.Content.Count());
        }

        [TestMethod]
        public void FilterAnimals_FilteredBy_KindAndSkinAndManyRegions()
        {
            // Arrange
            AnimalsFilter filter = GetFilter();
            filter.KindId = kinds[0].Id;
            filter.SkinId = skins[1].Id;
            filter.RegIds = new[] { regions[1].Id, regions[4].Id }; // new[] { 0 };

            // Act
            var actionResult = controller.FilterAnimals(filter) as IHttpActionResult;
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<AnimalLight>>;

            // Assert
            var filtered = animals.Where(a => a.KindId == filter.KindId && a.SkinId == filter.SkinId && a.Regions.Any(m => filter.RegIds.Contains(m.Id)));

            Assert.AreEqual(filtered.Count(), contentResult.Content.Count());
        }

        #endregion Tests for FilterAnimals()

    }
}
