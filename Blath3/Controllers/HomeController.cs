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
        public CoreSubcategoria coreSubCat = new CoreSubcategoria();
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
            ViewData["lnknome"] = lnknome;

            return View();
        }

        [Route("{lnkcat}/{lnksubcat}")]
        public ActionResult SubCat(string lnkcat, string lnksubcat)
        {
            Subcategoria sub = new Subcategoria();
            sub = coreSubCat.Retorna(lnksubcat);
            
            Categoria cat = new Categoria();
            cat = coreCat.Retorna(lnkcat);

            ViewBag.Title = cat.Nome + ": " + sub.Nome;
            ViewData["categoryId"] = cat.CategoriaId;
            ViewData["subcategoryId"] = sub.SubcategoriaId;
            ViewData["lnknome"] = lnksubcat;
            ViewData["lnkcat"] = cat.NomeLink;

            return View();
        }


    }
}
