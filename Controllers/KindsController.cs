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
        [CacheFilter(TimeDuration = 600)]
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
            if (disposing)
            {
                repo.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
