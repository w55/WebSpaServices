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

namespace WebSpaServices.Controllers
{
    public class AnimalsController : ApiController
    {
        IUnitOfWork uow;

        // Конструкторы
        //
        #region AnimalsController()

        public AnimalsController()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument("context", new AnimalsContext());
            uow = ninjectKernel.Get<IUnitOfWork>();
        }

        public AnimalsController(IUnitOfWork unit)
        {
            uow = unit;
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
                var animals = uow.Animals.GetAll().Include(a => a.Regions).OrderBy(a => a.AnimalName).ToList();
                
                // Настройка AutoMapper
                Mapper.Initialize(cfg =>
                    cfg.CreateMap<Animal, AnimalLight>()
                        .ForMember(dest => dest.RegIds, opt => opt.MapFrom(src => src.Regions.Select(r => r.Id))));

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

            Animal animal = uow.Animals.GetAll().Include(a => a.Regions).FirstOrDefault(b => b.AnimalId == id);

            if (animal == null)
            {
                return NotFound();
            }

            try
            {
                // Настройка AutoMapper
                Mapper.Initialize(cfg =>
                    cfg.CreateMap<Animal, AnimalLight>()
                        .ForMember(dest => dest.RegIds, opt => opt.MapFrom(src => src.Regions.Select(r => r.Id))));

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

            if (uow.Animals.GetAll().Count(a => a.AnimalName.Equals(animalLight.AnimalName, StringComparison.InvariantCultureIgnoreCase)) > 0)
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
                // Настройка AutoMapper
                Mapper.Initialize(cfg =>
                    cfg.CreateMap<AnimalLight, Animal>()
                    .ForMember(dest => dest.Regions, opt => opt.Ignore()));

                // сопоставление
                Animal animal = Mapper.Map<AnimalLight, Animal>(animalLight);

                var regions = uow.Regions.GetAll().Where(r => animalLight.RegIds.Contains(r.Id)).ToList<Region>();
                animal.Regions = regions;

                uow.Animals.Create(animal);
                uow.Save();

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

            if (uow.Animals.GetAll().Count(a => a.AnimalId != animalLight.AnimalId &&
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
                Animal animal = uow.Animals.GetAll().Include(a => a.Skin).Include(a => a.Kind).Include(a => a.Location).Include(a => a.Regions)
                    .FirstOrDefault(a => a.AnimalId == animalLight.AnimalId);

                // Изменяем поля для выбранного животного - на новые
                //
                if (id == animal.AnimalId)
                {
                    var regions = uow.Regions.GetAll().Where(r => animalLight.RegIds.Contains(r.Id)).ToList<Region>();
                    animal.Regions = regions;

                    animal.Skin = uow.Skins.GetAll().FirstOrDefault(s => s.Id == animalLight.SkinId);
                    animal.Kind = uow.Kinds.GetAll().FirstOrDefault(s => s.Id == animalLight.KindId);
                    animal.Location = uow.Locations.GetAll().FirstOrDefault(s => s.Id == animalLight.LocationId);

                    animal.AnimalName = animalLight.AnimalName;

                    uow.Animals.Update(animal);
                    uow.Save();

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

            Animal animal = uow.Animals.GetAll().Include(a => a.Regions).FirstOrDefault(a => a.AnimalId == id);

            if (animal == null)
            {
                return NotFound();
            }

            try
            {
                // Настройка AutoMapper
                Mapper.Initialize(cfg =>
                    cfg.CreateMap<Animal, AnimalLight>()
                        .ForMember(dest => dest.RegIds, opt => opt.MapFrom(src => src.Regions.Select(r => r.Id))));

                // сопоставление
                var animalLight = Mapper.Map<Animal, AnimalLight>(animal);

                uow.Animals.Delete(animal.AnimalId);
                uow.Save();

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

            var animals = uow.Animals.GetAll().Include(a => a.Skin).Include(a => a.Kind).Include(a => a.Regions);

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
                // Настройка AutoMapper
                Mapper.Initialize(cfg =>
                    cfg.CreateMap<Animal, AnimalLight>()
                        .ForMember(dest => dest.RegIds, opt => opt.MapFrom(src => src.Regions.Select(r => r.Id))));

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
                uow.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion Dispose()

    }
}
