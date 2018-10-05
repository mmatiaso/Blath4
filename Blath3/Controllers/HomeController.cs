using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blath3.Models;
using Blath3.Models.Core;
using Blath3.Models.Utils;

namespace Blath3.Controllers
{
    public class HomeController : Controller
    {
        public CoreCategoria coreCat = new CoreCategoria();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [Route("{lnknome}")]
        public ActionResult Categoria(string lnknome)
        {
            Categoria cat = new Categoria();
            cat = coreCat.Retorna(lnknome);
            ViewBag.Title = cat.Nome;
            ViewData["categoryId"] = cat.CategoriaId;

            return View();
        }


    }
}
