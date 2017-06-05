using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebSpaServices.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private AnimalsContext db; 

        public UnitOfWork(AnimalsContext context)
        {
            this.db = context;
        }

        //
        //---------------------------    Private members        --------------------------------
        //
        private IRepository<Skin> skinRepository;
        private IRepository<Kind> kindRepository;
        private IRepository<Location> locationRepository;
        private IRepository<Region> regionRepository;
        private IRepository<Animal> animalRepository;


        //
        //---------------------------    Public properties        --------------------------------
        //
        /// <summary>
        /// Обертка реализации IRepository для сущности ЖИВОТНОЕ
        /// </summary>
        public IRepository<Animal> Animals
        {
            get
            {
                if (animalRepository == null)
                    animalRepository = new AnimalRepository(db);
                return animalRepository;
            }
            set
            {
                animalRepository = value;
            }
        }

        /// <summary>
        /// Обертка реализации IRepository для сущности ЦВЕТ ШКУРЫ
        /// </summary>
        public IRepository<Skin> Skins
        {
            // SkinRepository
            get
            {
                if (skinRepository == null)
                    skinRepository = new SkinRepository(db);
                return skinRepository;
            }
            set
            {
                skinRepository = value;
            }
        }

        /// <summary>
        /// Обертка реализации IRepository для сущности ВИД ЖИВОТНОГО
        /// </summary>
        public IRepository<Kind> Kinds
        {
            get
            {
                if (kindRepository == null)
                    kindRepository = new KindRepository(db);
                return kindRepository;
            }
            set
            {
                kindRepository = value;
            }
        }

        /// <summary>
        /// Обертка реализации IRepository для сущности МЕСТОНАХОЖДЕНИЕ
        /// </summary>
        public IRepository<Location> Locations
        {
            get
            {
                if (locationRepository == null)
                    locationRepository = new LocationRepository(db);
                return locationRepository;
            }
            set
            {
                locationRepository = value;
            }
        }

        /// <summary>
        /// Обертка реализации IRepository для сущности РЕГИОН
        /// </summary>
        public IRepository<Region> Regions
        {
            get
            {
                if (regionRepository == null)
                    regionRepository = new RegionRepository(db);
                return regionRepository;
            }
            set
            {
                regionRepository = value;
            }
        }

        //
        //---------------------------    Save method        --------------------------------
        //
        /// <summary>
        /// Общий для паттерна UoW метод сохраниния
        /// </summary>
        public void Save()
        {
            db.SaveChanges();
        }

        //
        //---------------------------    Dispose  pattern        --------------------------------
        //
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            Trace.WriteLine("--- UnitOfWork.Dispose(" + disposing + ") ---");

            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
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