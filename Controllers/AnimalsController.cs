using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebSpaServices.Models;
using System.Data.Entity;
using System.Web.Http.Description;
using AutoMapper;
using Ninject;
using WebSpaServices.Utils;

namespace WebSpaServices.Controllers
{
    public class AnimalsController : ApiController
    {
        private readonly IRepositoryFactory _repoFactory;

        // Конструкторы
        //
        #region AnimalsController()

        public AnimalsController(IRepositoryFactory repositoryFactory)
        {
            _repoFactory = repositoryFactory;
        }

        static AnimalsController()
        {
            //
            //----  настройка профиля для методов автомаппера  -------
            //
            Mapper.Initialize(cfg => cfg.AddProfile(new AutomapperProfile()));
        }

        #endregion AnimalsController()


        //
        //-------------------  CRUD-операции над животными   ----------------------------
        //
        #region GetAnimals()

        // GET api/animals
        //        
        public IHttpActionResult GetAnimals()
        {
            Trace.WriteLine("--- GetAnimals() ---");

            try
            {
                var animals = _repoFactory.CreateAnimalRepository().GetAll().Include(a => a.Regions).OrderBy(a => a.AnimalName).ToList();

                // сопоставление
                var animalsLight = Mapper.Map<List<Animal>, IEnumerable<AnimalLight>>(animals);

                return Ok(animalsLight);
            }
            catch (Exception x)
            {
                Trace.WriteLine("--- GetAnimals() Exception: " + x.Message);

                ModelState.AddModelError("animal", x.Message);
                return BadRequest(ModelState);
            }
        }

        #endregion GetAnimals()

        #region GetAnimal()

        // GET api/animals/3
        //
        [ResponseType(typeof(AnimalLight))]
        public IHttpActionResult GetAnimal(int id)
        {
            Trace.WriteLine("--- GetAnimal() ---");

            Animal animal = _repoFactory.CreateAnimalRepository().GetAll().Include(a => a.Regions).FirstOrDefault(b => b.AnimalId == id);

            if (animal == null)
            {
                return NotFound();
            }

            try
            {
                // сопоставление
                var animalLight = Mapper.Map<Animal, AnimalLight>(animal);

                return Ok(animalLight);
            }
            catch (Exception x)
            {
                Trace.WriteLine("GetAnimal() Exception: " + x.Message);

                ModelState.AddModelError("animal", x.Message);
                return BadRequest(ModelState);
            }
        }

        #endregion GetAnimal()

        #region CreateAnimal()

        // POST api/animals
        //
        [HttpPost]
        public IHttpActionResult CreateAnimal([FromBody]AnimalLight animalLight)
        {
            Trace.WriteLine("--- CreateAnimal() ---");

            if (animalLight == null)
            {
                return BadRequest();
            }

            if (_repoFactory.CreateAnimalRepository().GetAll().Count(a => a.AnimalName.Equals(animalLight.AnimalName, StringComparison.InvariantCultureIgnoreCase)) > 0)
            {
                ModelState.AddModelError("animalLight", "Ошибка: животное с таким названием - уже существует в списке животных!");
            }

            // Проверка на допустимое кол-во символов в имени нового животного (ставим условие: от 2-х до 100 символов)
            //
            if (string.IsNullOrEmpty(animalLight.AnimalName) || animalLight.AnimalName.Length < 2 || animalLight.AnimalName.Length > 100)
            {
                ModelState.AddModelError("animalLight", "Ошибка: название животного должно содержать от 2-х до 100 символов!");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // сопоставление
                Animal animal = Mapper.Map<AnimalLight, Animal>(animalLight);

                var regions = _repoFactory.CreateRegionRepository().GetAll().Where(r => animalLight.RegIds.Contains(r.Id)).ToList<Region>();

                animal.Regions = regions;

                _repoFactory.CreateAnimalRepository().Create(animal);
                _repoFactory.CreateAnimalRepository().Save();

                animalLight.AnimalId = animal.AnimalId;

                // если запрос без ошибок
                return CreatedAtRoute("DefaultApi", new { id = animalLight.AnimalId }, animalLight);
            }
            catch (Exception x)
            {
                Trace.WriteLine("CreateAnimal() Exception: " + x.Message);

                ModelState.AddModelError("animal", x.Message);
                return BadRequest(ModelState);
            }
        }

        #endregion CreateAnimal()

        #region EditAnimal()

        // PUT: api/animals/5
        //
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult EditAnimal(int id, [FromBody]AnimalLight animalLight)
        {
            Trace.WriteLine("--- EditAnimal() ---");

            if (animalLight == null)
            {
                return BadRequest();
            }

            if (_repoFactory.CreateAnimalRepository().GetAll().Count(a => a.AnimalId != animalLight.AnimalId &&
                a.AnimalName.Equals(animalLight.AnimalName, StringComparison.InvariantCultureIgnoreCase)) > 0)
            {
                ModelState.AddModelError("animalLight", "Ошибка: другое животное с таким названием - уже существует в списке животных!");
            }

            // Проверка на допустимое кол-во символов в имени нового животного (ставим условие: от 2-х до 100 символов)
            //
            if (string.IsNullOrEmpty(animalLight.AnimalName) || animalLight.AnimalName.Length < 2 || animalLight.AnimalName.Length > 100)
            {
                ModelState.AddModelError("animalLight", "Ошибка: название животного должно содержать от 2-х до 100 символов!");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                Animal animal = _repoFactory.CreateAnimalRepository().GetAll().Include(a => a.Skin).Include(a => a.Kind).Include(a => a.Location).Include(a => a.Regions)
                    .FirstOrDefault(a => a.AnimalId == animalLight.AnimalId);

                // Изменяем поля для выбранного животного - на новые
                //
                if (id == animal.AnimalId)
                {
                    var regions = _repoFactory.CreateRegionRepository().GetAll().Where(r => animalLight.RegIds.Contains(r.Id)).ToList<Region>();
                    animal.Regions = regions;

                    animal.Skin = _repoFactory.CreateSkinRepository().GetAll().FirstOrDefault(s => s.Id == animalLight.SkinId);
                    animal.Kind = _repoFactory.CreateKindRepository().GetAll().FirstOrDefault(s => s.Id == animalLight.KindId);
                    animal.Location = _repoFactory.CreateLocationRepository().GetAll().FirstOrDefault(s => s.Id == animalLight.LocationId);

                    animal.AnimalName = animalLight.AnimalName;

                    _repoFactory.CreateAnimalRepository().Update(animal);
                    _repoFactory.CreateAnimalRepository().Save();

                    animalLight.AnimalId = animal.AnimalId;
                }
                else
                {
                    ModelState.AddModelError("animal", "Ошибка редактирования: Такого животного уже не существует в базе данных");
                }
            }
            catch (Exception x)
            {
                ModelState.AddModelError("animal", x.Message);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // если запрос без ошибок
            return Content(HttpStatusCode.Accepted, animalLight);
        }

        #endregion EditAnimal()

        #region DeleteAnimal()

        // DELETE: api/animals/5
        //
        [ResponseType(typeof(AnimalLight))]
        public IHttpActionResult DeleteAnimal(int id)
        {
            Trace.WriteLine("--- DeleteAnimal(" + id + ") ---");

            Animal animal = _repoFactory.CreateAnimalRepository().GetAll().Include(a => a.Regions).FirstOrDefault(a => a.AnimalId == id);

            if (animal == null)
            {
                return NotFound();
            }

            try
            {
                // сопоставление
                var animalLight = Mapper.Map<Animal, AnimalLight>(animal);

                _repoFactory.CreateAnimalRepository().Delete(animal.AnimalId);
                // uow.Save();
                _repoFactory.CreateAnimalRepository().Save();

                Trace.WriteLine("--- DeleteAnimal(): " + animal + " - was deleted !!! ---");

                // если запрос без ошибок
                return Ok(animalLight);
            }
            catch (Exception x)
            {
                ModelState.AddModelError("animal", x.Message);
                return BadRequest(ModelState);
            }
        }

        #endregion DeleteAnimal()


        //
        //-------------------  Извлекаем отфильтрованный список животных   ----------------------------
        //
        #region FilterAnimals()

        // POST api/animals/filter
        //
        [HttpPost]
        [Route("api/animals/filter")]
        public IHttpActionResult FilterAnimals([FromBody]AnimalsFilter filter)
        {
            Trace.WriteLine("--- FilterAnimals() ---");

            if (filter == null)
            {
                return BadRequest();
            }

            var animals = _repoFactory.CreateAnimalRepository().GetAll().Include(a => a.Skin).Include(a => a.Kind).Include(a => a.Regions);

            if (filter.SkinId > 0)
                animals = animals.Where(a => a.SkinId == filter.SkinId);
            if (filter.KindId > 0)
                animals = animals.Where(a => a.KindId == filter.KindId);

            if (filter.RegIds.Count(c => c > 0) > 0)
            {
                animals = animals.SelectMany(a => a.Regions, (a, r) => new { An = a, Re = r })
                    .Where(a => filter.RegIds.Contains(a.Re.Id))
                    .Select(a => a.An);
            }
            var animalsFiltered = animals.Include(a => a.Regions).Distinct().OrderBy(a => a.AnimalName).ToList();

            try
            {
                // сопоставление
                var animalsLight = Mapper.Map<List<Animal>, IEnumerable<AnimalLight>>(animalsFiltered);
                return Ok(animalsLight);
            }
            catch (Exception x)
            {
                Trace.WriteLine("FilterAnimals() Exception: " + x.Message);

                ModelState.AddModelError("animal", x.Message);
                return BadRequest(ModelState);
            }
        }

        #endregion FilterAnimals()


        //
        //---------------------------    Dispose()        --------------------------------
        //
        #region Dispose()

        protected override void Dispose(bool disposing)
        {
            Trace.WriteLine("--- Animals: Dispose(" + disposing + ") ---");

            if (disposing)
            {
                _repoFactory.CreateAnimalRepository().Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion Dispose()

    }
}
