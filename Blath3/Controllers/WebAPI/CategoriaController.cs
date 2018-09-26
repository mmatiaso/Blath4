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
    public class CategoriaController : ApiController
    {
        CoreCategoria cc = new CoreCategoria();
        Generico g = new Generico();
        Retorno r = new Retorno();

        [HttpGet]
        [Route("api/categoria/listarTodos")]
        public List<Categoria> Listar()
        {
            return cc.Lista();
        }

        [HttpGet]
        [Route("api/categoria/listar")]
        public List<Categoria> ListarAtivos()
        {
            return cc.ListaAtivos();
        }

        [HttpGet]
        [Route("api/categoria/retornar/{id}")]
        public Categoria Retornar(int id)
        {
            return cc.Retorna(id);
        }

        [HttpPost]
        [Route("api/categoria/inserir")]
        public Retorno Inserir(Categoria c)
        {
            return cc.Nova(c);
        }

        [HttpPost]
        [Route("api/categoria/editar")]
        public Retorno Editar(Categoria c)
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
        [Route("api/categoria/remover")]
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

        [HttpGet]
        [Route("api/categoria/menu")]
        public MenuApi Menu()
        {
            MenuApi mn = new MenuApi(); 
            mn.menushow = cc.ListaAtivos();
            return mn;
        }
    }
}
