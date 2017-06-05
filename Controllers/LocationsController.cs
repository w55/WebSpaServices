using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebSpaServices.Models;

namespace WebSpaServices.Controllers
{
    /// <summary>
    /// Извлечение данных из справочника МЕСТОПОЛОЖЕНИЕ
    /// </summary>
    public class LocationsController : ApiController
    {
        IRepository<Location> repo;

        public LocationsController(IRepository<Location> db)
        {
            repo = db;
        }

        //
        //---------------------------    GET: api/locations       --------------------------------
        //
        // [Route("api/animals/locations")]
        public IHttpActionResult GetLocations()
        {
            Trace.WriteLine("--- GetLocations() ---");
            return Ok(repo.GetAll());
        }

        //
        //---------------------------    Dispose()        --------------------------------
        //
        protected override void Dispose(bool disposing)
        {
            Trace.WriteLine("--- Locations: Dispose(" + disposing + ") ---");
            if (disposing) { }
            base.Dispose(disposing);
        }
    }
}
