using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebSpaServices.Models
{
    /// <summary>
    /// Реализация IRepository для сущности ЖИВОТНОЕ
    /// </summary>
    public class AnimalRepository : IRepository<Animal>
    {
        private AnimalsContext db;
        public AnimalRepository(AnimalsContext context)
        {
            db = context;
        }

        public IQueryable<Animal> GetAll()
        {
            return db.Animals;
        }

        public Animal Get(int id)
        {
            return db.Animals.Find(id);
        }

        public void Create(Animal animal)
        {
            db.Animals.Add(animal);
        }

        public void Update(Animal animal)
        {
            db.Entry(animal).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Animal animal = db.Animals.Find(id);
            if (animal != null)
            {
                db.Animals.Remove(animal);
            }
        }

        //
        //---------------------------    After adding Ninject Factory: Save method        --------------------------------
        //
        /// <summary>
        /// Метод сохраниния
        /// </summary>
        public void Save()
        {
            db.SaveChanges();
        }

        //
        //---------------------------    After adding Ninject Factory: Dispose  pattern        --------------------------------
        //
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            Trace.WriteLine("--- AnimalRepository.Dispose(" + disposing + ") ---");

            if (!this.disposed)
            {
                if (disposing)
                {
                    // db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }


    /// <summary>
    /// Реализация IRepository для сущности ЦВЕТ ШКУРЫ
    /// </summary>
    public class SkinRepository : IRepository<Skin>
    {
        private AnimalsContext db;
        public SkinRepository(AnimalsContext context)
        {
            db = context;
        }
        public IQueryable<Skin> GetAll()
        {
            return db.Skins;
        }

        public Skin Get(int id)
        {
            return db.Skins.Find(id);
        }


        public void Create(Skin skin)
        {
            db.Skins.Add(skin);
        }

        public void Update(Skin skin)
        {
            db.Entry(skin).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Skin skin = db.Skins.Find(id);
            if (skin != null)
                db.Skins.Remove(skin);
        }

        //
        //---------------------------    After adding Ninject Factory: Save method        --------------------------------
        //
        /// <summary>
        /// Метод сохраниния
        /// </summary>
        public void Save()
        {
            db.SaveChanges();
        }

        //
        //---------------------------    After adding Ninject Factory: Dispose  pattern        --------------------------------
        //
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            Trace.WriteLine("--- SkinRepository.Dispose(" + disposing + ") ---");

            if (!this.disposed)
            {
                if (disposing)
                {
                    // db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }


    /// <summary>
    /// Реализация IRepository для сущности ВИД ЖИВОТНОГО
    /// </summary>
    public class KindRepository : IRepository<Kind>
    {
        private AnimalsContext db;
        public KindRepository(AnimalsContext context)
        {
            db = context;
        }
        public IQueryable<Kind> GetAll()
        {
            return db.Kinds;
        }

        public Kind Get(int id)
        {
            return db.Kinds.Find(id);
        }


        public void Create(Kind kind)
        {
            db.Kinds.Add(kind);
        }

        public void Update(Kind kind)
        {
            db.Entry(kind).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Kind kind = db.Kinds.Find(id);
            if (kind != null)
                db.Kinds.Remove(kind);
        }

        //
        //---------------------------    After adding Ninject Factory: Save method        --------------------------------
        //
        /// <summary>
        /// Метод сохраниния
        /// </summary>
        public void Save()
        {
            db.SaveChanges();
        }

        //
        //---------------------------    After adding Ninject Factory: Dispose  pattern        --------------------------------
        //
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            Trace.WriteLine("--- KindRepository.Dispose(" + disposing + ") ---");

            if (!this.disposed)
            {
                if (disposing)
                {
                    // db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }


    /// <summary>
    /// Реализация IRepository для сущности МЕСТОНАХОЖДЕНИЕ
    /// </summary>
    public class LocationRepository : IRepository<Location>
    {
        private AnimalsContext db;
        public LocationRepository(AnimalsContext context)
        {
            db = context;
        }
        public IQueryable<Location> GetAll()
        {
            return db.Locations;
        }

        public Location Get(int id)
        {
            return db.Locations.Find(id);
        }


        public void Create(Location location)
        {
            db.Locations.Add(location);
        }

        public void Update(Location location)
        {
            db.Entry(location).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Location location = db.Locations.Find(id);
            if (location != null)
                db.Locations.Remove(location);
        }

        //
        //---------------------------    After adding Ninject Factory: Save method        --------------------------------
        //
        /// <summary>
        /// Метод сохраниния
        /// </summary>
        public void Save()
        {
            db.SaveChanges();
        }

        //
        //---------------------------    After adding Ninject Factory: Dispose  pattern        --------------------------------
        //
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            Trace.WriteLine("--- LocationRepository.Dispose(" + disposing + ") ---");

            if (!this.disposed)
            {
                if (disposing)
                {
                    // db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }


    /// <summary>
    /// Реализация IRepository для сущности РЕГИОН
    /// </summary>
    public class RegionRepository : IRepository<Region>
    {
        private AnimalsContext db;
        public RegionRepository(AnimalsContext context)
        {
            db = context;
        }
        public IQueryable<Region> GetAll()
        {
            return db.Regions;
        }

        public Region Get(int id)
        {
            return db.Regions.Find(id);
        }


        public void Create(Region region)
        {
            db.Regions.Add(region);
        }

        public void Update(Region region)
        {
            db.Entry(region).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Region region = db.Regions.Find(id);
            if (region != null)
                db.Regions.Remove(region);
        }

        //
        //---------------------------    After adding Ninject Factory: Save method        --------------------------------
        //
        /// <summary>
        /// Метод сохраниния
        /// </summary>
        public void Save()
        {
            db.SaveChanges();
        }

        //
        //---------------------------    After adding Ninject Factory: Dispose  pattern        --------------------------------
        //
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            Trace.WriteLine("--- RegionRepository.Dispose(" + disposing + ") ---");

            if (!this.disposed)
            {
                if (disposing)
                {
                    // db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}