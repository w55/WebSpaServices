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
    /// Извлечение данных из справочника ВИД ЖИВОТНОГО
    /// </summary>
    public class KindsController : ApiController
    {
        IRepository<Kind> repo;

        public KindsController(IRepository<Kind> db)
        {
            repo = db;
        }

        //
        //---------------------------    GET: api/kinds       --------------------------------
        //
        // [Route("api/animals/kinds")]
        public IHttpActionResult GetKinds()
        {
            Trace.WriteLine("--- GetKinds() ---");
            return Ok(repo.GetAll());
        }

        //
        //---------------------------    Dispose()        --------------------------------
        //
        protected override void Dispose(bool disposing)
        {
            Trace.WriteLine("--- Kinds: Dispose(" + disposing + ") ---");
            if (disposing) { }
            base.Dispose(disposing);
        }
    }
}
