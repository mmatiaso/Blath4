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
    public class ApiEmpresaController : ApiController
    {

        CoreEmpresa cc = new CoreEmpresa();
        Generico g = new Generico();
        Retorno r = new Retorno();

        [HttpGet]
        [Route("api/Empresa/listarTodos")]
        public List<Empresa> Listar()
        {
            return cc.Lista();
        }

        [HttpGet]
        [Route("api/Empresa/listar")]
        public List<Empresa> ListarAtivos()
        {
            return cc.ListaAtivos();
        }

        [HttpGet]
        [Route("api/Empresa/retornarId/{id}")]
        public Empresa Retornar(int id)
        {
            return cc.Retorna(id);
        }

        [HttpGet]
        [Route("api/Empresa/retornar/{id}")]
        public Empresa Retornar(Guid id)
        {
            return cc.Retorna(id);
        }

        [HttpPost]
        [Route("api/Empresa/inserir")]
        public Retorno Inserir(Empresa c)
        {
            return cc.Nova(c);
        }

        [HttpPost]
        [Route("api/Empresa/editar")]
        public Retorno Editar(Empresa c)
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
        [Route("api/Empresa/remover")]
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
