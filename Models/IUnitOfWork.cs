using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSpaServices.Models
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Animal> Animals { get; set; }
        IRepository<Skin> Skins { get; set; }
        IRepository<Kind> Kinds { get; set; }
        IRepository<Location> Locations { get; set; }
        IRepository<Region> Regions { get; set; }

        void Save();
    }
}