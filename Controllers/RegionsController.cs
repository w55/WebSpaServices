using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebSpaServices.Models;
using WebSpaServices.Utils;

namespace WebSpaServices.Controllers
{
    /// <summary>
    /// Извлечение данных из справочника РЕГИОНЫ
    /// </summary>
    public class RegionsController : ApiController
    {
        IRepository<Region> repo;

        public RegionsController(IRepository<Region> db)
        {
            repo = db;
        }

        //
        //---------------------------    GET: api/regions       --------------------------------
        //
        [CacheFilter(TimeDuration = 600)]
        public IHttpActionResult GetRegions()
        {
            Trace.WriteLine("--- GetRegions() ---");
            return Ok(repo.GetAll());
        }

        //
        //---------------------------    Dispose()        --------------------------------
        //
        protected override void Dispose(bool disposing)
        {
            Trace.WriteLine("--- Regions: Dispose(" + disposing + ") ---");
            if (disposing)
            {
                repo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
