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
    ///  Извлечение данных из справочника ЦВЕТ ШКУРЫ
    /// </summary>
    public class SkinsController : ApiController
    {
        IRepository<Skin> repo;

        public SkinsController(IRepository<Skin> skin)
        {
            repo = skin;
        }

        //
        //---------------------------    GET: api/skins       --------------------------------
        //
        [CacheFilter(TimeDuration = 600)]
        public IHttpActionResult GetSkins()
        {
            Trace.WriteLine("--- GetSkins() ---");
            return Ok(repo.GetAll());
        }

        //
        //---------------------------    Dispose()        --------------------------------
        //
        protected override void Dispose(bool disposing)
        {
            Trace.WriteLine("--- Skins: Dispose(" + disposing + ") ---");
            if (disposing)
            {
                repo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
