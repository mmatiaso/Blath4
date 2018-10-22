using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blath3.Controllers
{
    public class EmpresaController : Controller
    {
        // GET: Empresa
        [Route("p/e/{empresalink}")]
        public ActionResult Detalhe(string empresalink)
        {
            ViewData["lnkEmp"] = empresalink;
            return View();
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult AreaEmpresa()
        {
            return View();
        }

        public ActionResult PedidoDetalheEmpresa()
        {
            return View();
        }
    }
}