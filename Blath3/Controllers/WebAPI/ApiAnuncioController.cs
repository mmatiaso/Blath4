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
    public class ApiAnuncioController : ApiController
    {
        CoreAnuncio cc = new CoreAnuncio();
        Generico g = new Generico();
        Retorno r = new Retorno();
        CardEmpresa ce = new CardEmpresa();

        [HttpGet]
        [Route("api/Anuncio/listarTodos")]
        public List<Anuncio> Listar()
        {
            return cc.Lista();
        }

        [HttpGet]
        [Route("api/Anuncio/listar")]
        public List<Anuncio> ListarAtivos()
        {
            return cc.ListaAtivos();
        }

        [HttpGet]
        [Route("api/Anuncio/retornarId/{id}")]
        public Anuncio Retornar(int id)
        {
            return cc.Retorna(id);
        }

        [HttpGet]
        [Route("api/Anuncio/retornar/{id}")]
        public Anuncio Retornar(Guid id)
        {
            return cc.Retorna(id);
        }

        [HttpPost]
        [Route("api/Anuncio/listaCards")]
        public Report_CardEmpresa ListaCards(ListaCardReq l)
        {
            
            Report_CardEmpresa rc = new Report_CardEmpresa();
            rc.cardEmpresaShow = cc.ListaCard(l);
            rc.numRegistros = rc.cardEmpresaShow.Count();
            return rc;
        }

        [HttpPost]
        [Route("api/Anuncio/inserir")]
        public Retorno Inserir(Anuncio c)
        {
            return cc.Nova(c);
        }

        [HttpPost]
        [Route("api/Anuncio/editar")]
        public Retorno Editar(Anuncio c)
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
        [Route("api/Anuncio/remover")]
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
