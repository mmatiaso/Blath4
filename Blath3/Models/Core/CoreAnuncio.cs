using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Blath3.Models.Utils;

namespace Blath3.Models.Core
{
    public class CoreAnuncio
    {

        private Blath3Entities db = new Blath3Entities();
        public Retorno r = new Retorno();
        public Generico g = new Generico();
        public CoreEmpresa coreEmp = new CoreEmpresa();
        public CoreSubcategoria coreSub = new CoreSubcategoria();

        //list Anuncios
        public List<Anuncio> Lista()
        {
            List<Anuncio> lc = new List<Anuncio>();
            lc = db.Anuncios.ToList();
            return lc;
        }

        //list Anuncios
        //t => take, s = skip
        public Report_ListaCardEmpresa ListaCard(ListaCardReq lcr)
        {
            Report_ListaCardEmpresa RCE = new Report_ListaCardEmpresa();
            
            //var result = from a in db.Anuncios

            List<CardEmpresa> Lce = new List<CardEmpresa>();
            List<Empresa> LE = new List<Empresa>();


            if (lcr.subcatId > 0)
            {
                LE = coreEmp.ListaEmpresasPorSubcategoria(lcr.subcatId);
            }
            else if(lcr.catId > 0)
            {
                LE = coreEmp.ListaEmpresasPorCategoria(lcr.catId);
            }

            foreach(var emp in LE)
            {
                if (emp.Anuncios.Any())
                {
                    List<string> ls = coreSub.ListaSubcategoriasPorEmpresa(emp.EmpresaId);
                    decimal? nt = emp.Avaliacaos.Average(x => x.Nota);
                    CardEmpresa ce = new CardEmpresa
                    {
                        EmpresaId = emp.EmpresaId,
                        NomeEmpresa = emp.Nome,
                        ImagemUrl = emp.Anuncios.First().ImgUrl ?? "cadastro/icon-user.png",
                        Nota = (nt == null ? "5.0":nt.ToString()),
                        AvaliacaoLabel = "A Definir",
                        QtdAvaliacoes = emp.Avaliacaos.Count(),
                        Dominio = emp.Dominio,
                        Texto1 = emp.Anuncios.First().Frase1,
                        Texto2 = emp.Anuncios.First().Frase2,
                        Uf = emp.UF,
                        Cidade = emp.Cidade,
                        EmpSubcategorias = string.Join(", ", ls.ToArray())
                    };
                    Lce.Add(ce);
                }
                    
            }

            
            

            if (!string.IsNullOrEmpty(lcr.uf))
            {
                Lce = Lce.Where(x => x.Uf == lcr.uf).ToList();
            }

            if (!string.IsNullOrEmpty(lcr.cid))
            {
                Lce = Lce.Where(x => x.Cidade == lcr.cid).ToList();
            }

            RCE.numRegistros = Lce.Count();

            if (lcr.s > 0)
            {
                Lce = Lce.Skip(lcr.s).ToList();
            }

            if (lcr.t > 0)
            {
                Lce = Lce.Take(lcr.t).ToList();
            }

            
            RCE.cardEmpresaShow = Lce;

            return RCE;

        }

        //list Anuncios Ativas
        public List<Anuncio> ListaAtivos()
        {
            List<Anuncio> lc = new List<Anuncio>();
            lc = db.Anuncios.Where(x => x.Ativo == true).ToList();
            return lc;
        }

        //Achar Anuncio
        public Anuncio Retorna(int _id)
        {
            return db.Anuncios.Find(_id);
        }

        //Achar Anuncio
        public Anuncio Retorna(Guid _id)
        {
            return db.Anuncios.Where(x => x.AnuncioCode == _id).FirstOrDefault();
        }

        //existe Anuncio
        public bool Existe(Anuncio c)
        {
            if (db.Anuncios.Where(x => x.AnuncioId == c.AnuncioId).Any())
            {
                return true;
            }
            if (db.Anuncios.Where(x => x.EmpresaId == c.EmpresaId && x.Frase1 == c.Frase1).Any())
            {
                return true;
            }
            

            return false;

        }

        //new Anuncio
        public Retorno Nova(Anuncio c)
        {

            if (Existe(c))
            {
                r.MsgExibir = "Anuncio Já existe";
                r.Sucesso = false;
                r.CodigoStatus = 200;
            }
            else
            {
                try
                {
                    c.AnuncioCode = Guid.NewGuid();
                    c.CriadoEm = DateTime.Now;
                    c.AtualizadoEm = DateTime.Now;
                    //c.Status = c.Status ?? "LEAD";
                    //c.Senha = g.gerarSenha(8);
                    c.Ativo = true;

                    db.Anuncios.Add(c);
                    db.SaveChanges();
                    r.MsgExibir = "Anuncio inserido com sucesso";
                    r.Sucesso = true;
                    r.CodigoStatus = 200;
                }
                catch (Exception e)
                {
                    r.MsgExibir = e.Message;
                    r.Sucesso = false;
                    r.CodigoStatus = 500;
                    g.LogExcecao(r);
                }

            }

            return r;
        }

        //Edita Anuncio
        public Retorno Edita(Anuncio c)
        {
            Anuncio cOriginal = new Anuncio();

            if (c.AnuncioId > 0)
            {
                cOriginal = Retorna(c.AnuncioId);
            }
            else
            {
                cOriginal = Retorna(c.AnuncioCode);
            }

            if (cOriginal == null)
            {
                r.MsgExibir = "Anuncio não existente";
                r.Sucesso = false;
                r.CodigoStatus = 200;
            }
            else
            {
                try
                {
                    if (c.Frase1 != null)
                        cOriginal.Frase1 = c.Frase1;

                    if (c.Frase2 != null)
                        cOriginal.Frase2 = c.Frase2;

                    if (c.EmpresaId != null)
                        cOriginal.EmpresaId = c.EmpresaId;


                    if (c.Ativo != null)
                        cOriginal.Ativo = c.Ativo;

                    if (cOriginal.AnuncioCode == null)
                        cOriginal.AnuncioCode = Guid.NewGuid();

                    cOriginal.AtualizadoEm = DateTime.Now;

                    db.Entry(cOriginal).State = EntityState.Modified;
                    db.SaveChanges();

                    r.MsgExibir = "Anuncio atualizado com sucesso";
                    r.Sucesso = true;
                    r.CodigoStatus = 200;
                }
                catch (Exception e)
                {
                    r.MsgExibir = "Erro:" + e.Message + ", InnerException:" + e.InnerException;
                    r.Sucesso = false;
                    r.CodigoStatus = 500;
                    g.LogExcecao(r);
                }

            }

            return r;
        }

        //Remove Categoriga
        public Retorno Remove(int _id)
        {
            Anuncio cOriginal = Retorna(_id);

            if (cOriginal == null)
            {
                r.MsgExibir = "Anuncio não existente";
                r.Sucesso = false;
                r.CodigoStatus = 200;
            }
            else
            {
                try
                {


                    db.Anuncios.Remove(cOriginal);
                    db.SaveChanges();

                    r.MsgExibir = "Anuncio excluído com sucesso";
                    r.Sucesso = true;
                    r.CodigoStatus = 200;
                }
                catch (Exception e)
                {
                    r.MsgExibir = e.Message;
                    r.Sucesso = false;
                    r.CodigoStatus = 500;
                    g.LogExcecao(r);
                }

            }

            return r;
        }

    }
}