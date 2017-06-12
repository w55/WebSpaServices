using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSpaServices.Models
{
    public interface IRepositoryFactory
    {
        IRepository<Skin> CreateSkinRepository();
        IRepository<Kind> CreateKindRepository();
        IRepository<Location> CreateLocationRepository();
        IRepository<Region> CreateRegionRepository();
        IRepository<Animal> CreateAnimalRepository();
    }
}