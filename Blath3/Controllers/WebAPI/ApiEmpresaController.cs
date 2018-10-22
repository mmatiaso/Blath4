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
        CoreSubcategoria coreSub = new CoreSubcategoria();
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

        [HttpGet]
        [Route("api/Empresa/retornarcard/{dominio}")]
        public Report_CardEmpresa RetornarCard(String dominio)
        {
            Report_CardEmpresa R = new Report_CardEmpresa();
            Empresa emp = new Empresa();
            emp = cc.Retorna(dominio);
            List<string> ls = coreSub.ListaSubcategoriasPorEmpresa(emp.EmpresaId);
            decimal? nt = emp.Avaliacaos.Average(x => x.Nota);
            CardEmpresa ce = new CardEmpresa
            {
                EmpresaId = emp.EmpresaId,
                NomeEmpresa = emp.Nome,
                ImagemUrl = emp.Anuncios.First().ImgUrl ?? "cadastro/icon-user.png",
                Nota = (nt == null ? "5.0" : nt.ToString()),
                AvaliacaoLabel = "A Definir",
                QtdAvaliacoes = emp.Avaliacaos.Count(),
                Dominio = emp.Dominio,
                Texto1 = emp.Anuncios.First().Frase1,
                Texto2 = emp.Anuncios.First().Frase2,
                Uf = emp.UF,
                Cidade = emp.Cidade,
                EmpSubcategorias = string.Join(", ", ls.ToArray()),
                DescricaoEmpresa = emp.Descricao,
                UFAtendimento = emp.AreaAtuacaoUF,
                CidadeAtendimento = emp.AreaAtuacaoCidade
            };
            R.shows = ce;
            return R;
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
