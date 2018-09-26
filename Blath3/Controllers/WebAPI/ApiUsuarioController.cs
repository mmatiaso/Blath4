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
    public class ApiUsuarioController : ApiController
    {
        CoreUsuario cc = new CoreUsuario();
        Generico g = new Generico();
        Retorno r = new Retorno();

        [HttpGet]
        [Route("api/Usuario/listarTodos")]
        public List<Usuario> Listar()
        {
            return cc.Lista();
        }

        [HttpGet]
        [Route("api/Usuario/listar")]
        public List<Usuario> ListarAtivos()
        {
            return cc.ListaAtivos();
        }

        [HttpGet]
        [Route("api/Usuario/retornarId/{id}")]
        public Usuario Retornar(int id)
        {
            return cc.Retorna(id);
        }

        [HttpGet]
        [Route("api/Usuario/retornar/{id}")]
        public Usuario Retornar(Guid id)
        {
            return cc.Retorna(id);
        }

        [HttpPost]
        [Route("api/Usuario/inserir")]
        public Retorno Inserir(Usuario c)
        {
            return cc.Nova(c);
        }

        [HttpPost]
        [Route("api/Usuario/editar")]
        public Retorno Editar(Usuario c)
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
        [Route("api/Usuario/remover")]
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
