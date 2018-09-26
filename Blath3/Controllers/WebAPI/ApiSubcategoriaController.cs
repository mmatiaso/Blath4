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
    public class ApiSubSubcategoriaController : ApiController
    {
        CoreSubcategoria cc = new CoreSubcategoria();
        Generico g = new Generico();
        Retorno r = new Retorno();

        [HttpGet]
        [Route("api/Subcategoria/listarTodos")]
        public List<Subcategoria> Listar()
        {
            return cc.Lista();
        }

        [HttpGet]
        [Route("api/Subcategoria/listar")]
        public List<Subcategoria> ListarAtivos()
        {
            return cc.ListaAtivos();
        }

        [HttpGet]
        [Route("api/Subcategoria/retornar/{id}")]
        public Subcategoria Retornar(int id)
        {
            return cc.Retorna(id);
        }

        [HttpPost]
        [Route("api/Subcategoria/inserir")]
        public Retorno Inserir(Subcategoria c)
        {
            return cc.Nova(c);
        }

        [HttpPost]
        [Route("api/Subcategoria/editar")]
        public Retorno Editar(Subcategoria c)
        {
            //if (!g.SegurancaAPI(Request))
            //{
            //    r.Sucesso = false;
            //    r.MsgExibir = "Acesso não permitido";
            //    return r;
            //}

            return cc.Edita(c);
        }

        [HttpPost]
        [Route("api/Subcategoria/remover")]
        public Retorno Remover(int oid)
        {

            if (!g.SegurancaAPI(Request))
            {
                r.Sucesso = false;
                r.MsgExibir = "Acesso não permitido";
                return r;
            }


            return cc.Remove(oid);
        }
    }
}
