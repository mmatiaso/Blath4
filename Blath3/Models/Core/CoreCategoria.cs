using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Blath3.Models.Utils;

namespace Blath3.Models.Core
{
    public class CoreCategoria
    {
        private Blath3Entities db = new Blath3Entities();
        public Retorno r = new Retorno();
        public Generico g = new Generico();

        //list Categories
        public List<Categoria> Lista()
        {
            List<Categoria> lc = new List<Categoria>();
            lc = db.Categorias.ToList();
            return lc;
        }

        //list Categorias Ativas
        public List<Categoria> ListaAtivos()
        {
            List<Categoria> lc = new List<Categoria>();
            lc = db.Categorias.Where(x => x.Ativo == true).ToList();
            return lc;
        }

        //Achar Categoria
        public Categoria Retorna(int _id)
        {
            return db.Categorias.Find(_id);
        }

        //existe Categoria
        public bool Existe(Categoria c)
        {
            if(db.Categorias.Where(x => x.CategoriaId == c.CategoriaId).Any())
            {
                return true;
            }
            if (db.Categorias.Where(x => x.Nome == c.Nome).Any())
            {
                return true;
            }
            if (db.Categorias.Where(x => x.NomeLink == c.NomeLink).Any())
            {
                return true;
            }

            return false;

        }

        //new Categoria
        public Retorno Nova(Categoria c)
        {
            
            if (Existe(c))
            {
                r.MsgExibir = "Categoria Já existe";
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

                    db.Categorias.Add(c);
                    db.SaveChanges();
                    r.MsgExibir = "Categoria inserida com sucesso";
                    r.Sucesso = true;
                    r.CodigoStatus = 200;
                }
                catch(Exception e)
                {
                    r.MsgExibir = e.Message;
                    r.Sucesso = false;
                    r.CodigoStatus = 500;
                    g.LogExcecao(r);
                }
                
            }
            
            return r;
        }

        //Edita Categoria
        public Retorno Edita(Categoria c)
        {
            Categoria cOriginal = Retorna(c.CategoriaId);

            if (cOriginal == null)
            {
                r.MsgExibir = "Categoria não existente";
                r.Sucesso = false;
                r.CodigoStatus = 200;
            }
            else
            {
                try
                {
                    if(c.Nome != null)
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

                    r.MsgExibir = "Categoria atualizada com sucesso";
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
            Categoria cOriginal = Retorna(_id);

            if (cOriginal == null)
            {
                r.MsgExibir = "Categoria não existente";
                r.Sucesso = false;
                r.CodigoStatus = 200;
            }
            else
            {
                try
                {


                    db.Categorias.Remove(cOriginal);
                    db.SaveChanges();

                    r.MsgExibir = "Categoria excluída com sucesso";
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