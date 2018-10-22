using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Blath3.Models.Utils;

namespace Blath3.Models.Core
{
    public class CoreSubcategoria
    {
        private Blath3Entities db = new Blath3Entities();
        public Retorno r = new Retorno();
        public Generico g = new Generico();

        //list Subcategorias
        public List<Subcategoria> Lista()
        {
            List<Subcategoria> lc = new List<Subcategoria>();
            lc = db.Subcategorias.ToList();
            return lc;
        }

        //list Subcategorias Ativas
        public List<Subcategoria> ListaAtivos()
        {
            List<Subcategoria> lc = new List<Subcategoria>();
            lc = db.Subcategorias.Where(x => x.Ativo == true).ToList();
            return lc;
        }

        //ListaSubcategoriasPorEmpresa
        public List<string> ListaSubcategoriasPorEmpresa(int empid)
        {
            List<string> lc = new List<string>();
            var emps = db.EmpSubs.Where(x => x.EmpresaId == empid);
            foreach (var es in emps)
            {
                lc.Add(es.Subcategoria.Nome);
            }

            return lc;
        }

        //ListaSubcategoriasPorCategoria
        public List<Subcategoria> ListaSubcategoriasPorCategoria(int catid)
        {
            
            List <Subcategoria> lc = new List<Subcategoria>();
            var catsb = db.CatSubs.Where(x => x.CategoriaId == catid);
            foreach (var es in catsb)
            {
                lc.Add(es.Subcategoria);
            }
            //db.Configuration.LazyLoadingEnabled = false;
            
            return lc;
        }

        //ListaSubcategoriasPorCategoria
        public List<SubMenu> ListaSubMenu(int catid)
        {

            List<SubMenu> lc = new List<SubMenu>();
            var catsb = db.CatSubs.Where(x => x.CategoriaId == catid);
            foreach (var es in catsb)
            {
                SubMenu s = new SubMenu();
                s.Nome = es.Subcategoria.Nome;
                s.Tema = es.Tema;
                s.NomeLink = es.Subcategoria.NomeLink;

                lc.Add(s);
            }
            //db.Configuration.LazyLoadingEnabled = false;

            return lc;
        }

        //ListaCatSubsPorCategoria
        public List<CatSub> ListaCatSubsPorCategoria(int catid)
        {
            List<CatSub> lc = new List<CatSub>();
            lc = db.CatSubs.Where(x => x.CategoriaId == catid).ToList();
            

            return lc;
        }

        //Achar Subcategoria
        public Subcategoria Retorna(int _id)
        {
            return db.Subcategorias.Find(_id);
        }

        //Achar Subcategoria
        public Subcategoria Retorna(string lnk)
        {
            return db.Subcategorias.Where(x => x.NomeLink == lnk).FirstOrDefault();
        }

        //existe Subcategoria
        public bool Existe(Subcategoria c)
        {
            if (db.Subcategorias.Where(x => x.SubcategoriaId == c.SubcategoriaId).Any())
            {
                return true;
            }
            if (db.Subcategorias.Where(x => x.Nome == c.Nome).Any())
            {
                return true;
            }
            if (db.Subcategorias.Where(x => x.NomeLink == c.NomeLink).Any())
            {
                return true;
            }

            return false;

        }

        //new Subcategoria
        public Retorno Nova(Subcategoria c)
        {

            if (Existe(c))
            {
                r.MsgExibir = "Subcategoria Já existe";
                r.Sucesso = false;
                r.CodigoStatus = 200;
            }
            else
            {
                try
                {
                    c.CriadoEm = DateTime.Now;
                    c.AtualizadoEm = DateTime.Now;
                    c.Ativo = true;

                    db.Subcategorias.Add(c);
                    db.SaveChanges();
                    r.MsgExibir = "Subcategoria inserida com sucesso";
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

        //Edita Subcategoria
        public Retorno Edita(Subcategoria c)
        {
            Subcategoria cOriginal = Retorna(c.SubcategoriaId);

            if (cOriginal == null)
            {
                r.MsgExibir = "Subcategoria não existente";
                r.Sucesso = false;
                r.CodigoStatus = 200;
            }
            else
            {
                try
                {
                    if (c.Nome != null)
                        cOriginal.Nome = c.Nome;

                    if (c.NomeLink != null)
                        cOriginal.NomeLink = c.NomeLink;

                    if (c.Descricao != null)
                        cOriginal.Descricao = c.Descricao;

                    if (c.Tags != null)
                        cOriginal.Tags = c.Tags;

                    if (c.Ativo != null)
                        cOriginal.Ativo = c.Ativo;

                    cOriginal.AtualizadoEm = DateTime.Now;

                    db.Entry(cOriginal).State = EntityState.Modified;
                    db.SaveChanges();

                    r.MsgExibir = "Subcategoria atualizada com sucesso";
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

        //Remove Categoriga
        public Retorno Remove(int _id)
        {
            Subcategoria cOriginal = Retorna(_id);

            if (cOriginal == null)
            {
                r.MsgExibir = "Subcategoria não existente";
                r.Sucesso = false;
                r.CodigoStatus = 200;
            }
            else
            {
                try
                {


                    db.Subcategorias.Remove(cOriginal);
                    db.SaveChanges();

                    r.MsgExibir = "Subcategoria excluída com sucesso";
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