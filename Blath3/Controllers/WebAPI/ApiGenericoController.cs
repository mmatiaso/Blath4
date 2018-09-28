using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Blath3.Models;
using Blath3.Models.Core;
using Blath3.Models.Utils;

namespace Blath3.Controllers.WebAPI
{
    public class ApiGenericoController : ApiController
    {
        BuscaApi ba = new BuscaApi();

        [HttpGet]
        [Route("api/generico/{word}")]
        public List<BuscaApi> Listar(string word)
        {
            return ba.RetornaBusca(word);
        }
    }
}
