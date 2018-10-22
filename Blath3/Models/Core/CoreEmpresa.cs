using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Blath3.Models.Utils;

namespace Blath3.Models.Core
{
    public class CoreEmpresa
    {

        private Blath3Entities db = new Blath3Entities();
        public Retorno r = new Retorno();
        public Generico g = new Generico();
        public CoreSubcategoria coreSub = new CoreSubcategoria();

        //list Empresas
        public List<Empresa> Lista()
        {
            List<Empresa> lc = new List<Empresa>();
            lc = db.Empresas.ToList();
            return lc;
        }

        //list Empresas Ativas
        public List<Empresa> ListaAtivos()
        {
            List<Empresa> lc = new List<Empresa>();
            lc = db.Empresas.Where(x => x.Ativo == true).ToList();
            return lc;
        }

        //ListaEmpresasPorSubcategoria
        public List<Empresa> ListaEmpresasPorSubcategoria(int subcatid)
        {
            List<Empresa> lc = new List<Empresa>();
            var emps = db.EmpSubs.Where(x => x.SubcategoriaId == subcatid);
            foreach (var es in emps)
            {
                if(!lc.Where(x => x.EmpresaId == es.EmpresaId).Any())
                {
                    lc.Add(es.Empresa);
                }
                
            }
            
            return lc;
        }

        //ListaEmpresasPorCategoria
        public List<Empresa> ListaEmpresasPorCategoria(int catid)
        {
            List<Empresa> lc = new List<Empresa>();
            List<Subcategoria> ls = new List<Subcategoria>();

            ls = coreSub.ListaSubcategoriasPorCategoria(catid);
            foreach(Subcategoria s in ls)
            {
                var emps = db.EmpSubs.Where(x => x.SubcategoriaId == s.SubcategoriaId);
                foreach (var es in emps)
                {
                    if (!lc.Where(x => x.EmpresaId == es.EmpresaId).Any())
                    {
                        lc.Add(es.Empresa);
                    }

                }
            }
            

            return lc;
        }

        //Achar Empresa
        public Empresa Retorna(int _id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Empresas.Find(_id);
        }

        //Achar Empresa
        public Empresa Retorna(Guid _id)
        {
            return db.Empresas.Where(x => x.EmpresaCode == _id).FirstOrDefault();
        }

        //Achar Empresa
        public Empresa Retorna(string _id)
        {
            return db.Empresas.Where(x => x.Dominio == _id).FirstOrDefault();
        }

        //existe Empresa
        public bool Existe(Empresa c)
        {
            if (db.Empresas.Where(x => x.EmpresaId == c.EmpresaId).Any())
            {
                return true;
            }
            if (db.Empresas.Where(x => x.Email == c.Email).Any() && !string.IsNullOrEmpty(c.Email))
            {
                return true;
            }
            if (db.Empresas.Where(x => x.CNPJ == c.CNPJ).Any() && !string.IsNullOrEmpty(c.CNPJ))
            {
                return true;
            }
            if (db.Empresas.Where(x => x.Dominio == c.Dominio).Any() && !string.IsNullOrEmpty(c.Dominio))
            {
                return true;
            }

            return false;

        }

        //ChecarObrigatoriedade Empresa
        public string ChecarObrigatoriedade(Empresa c)
        {
            if (string.IsNullOrEmpty(c.Nome))
            {
                return "O campo Nome é obrigatório";
            }
            if (string.IsNullOrEmpty(c.Email))
            {
                return "O campo Email é obrigatório";
            }
            if (string.IsNullOrEmpty(c.Dominio))
            {
                return "O campo Dominio é obrigatório";
            }
            if (string.IsNullOrEmpty(c.Status))
            {
                return "O campo Status é obrigatório";
            }

            return "OK";

        }

        //new Empresa
        public Retorno Nova(Empresa c)
        {

            if (ChecarObrigatoriedade(c) != "OK")
            {
                r.MsgExibir = ChecarObrigatoriedade(c);
                r.Sucesso = false;
                r.CodigoStatus = 200;
            }

            if (Existe(c))
            {
                r.MsgExibir = "Empresa Já existe";
                r.Sucesso = false;
                r.CodigoStatus = 200;
            }
            else
            {
                try
                {
                    c.EmpresaCode = Guid.NewGuid();
                    c.CriadoEm = DateTime.Now;
                    c.AtualizadoEm = DateTime.Now;
                    c.Status = c.Status ?? "LEAD";
                    //c.Senha = g.gerarSenha(8);
                    c.Ativo = true;

                    db.Empresas.Add(c);
                    db.SaveChanges();
                    r.MsgExibir = "Empresa inserido com sucesso";
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

        //Edita Empresa
        public Retorno Edita(Empresa c)
        {
            Empresa cOriginal = new Empresa();

            if (ChecarObrigatoriedade(c) != "OK")
            {
                r.MsgExibir = ChecarObrigatoriedade(c);
                r.Sucesso = false;
                r.CodigoStatus = 200;
            }

            if (c.EmpresaId > 0)
            {
                cOriginal = Retorna(c.EmpresaId);
            }
            else
            {
                cOriginal = Retorna(c.EmpresaCode);
            }

            if (cOriginal == null)
            {
                r.MsgExibir = "Empresa não existente";
                r.Sucesso = false;
                r.CodigoStatus = 200;
            }
            else
            {
                try
                {
                    if (c.Nome != null)
                        cOriginal.Nome = c.Nome;

                    if (c.UsuarioId != null)
                        cOriginal.UsuarioId = c.UsuarioId;

                    if (c.RazaoSocial != null)
                        cOriginal.RazaoSocial = c.RazaoSocial;

                    if (c.Site != null)
                        cOriginal.Site = c.Site;

                    if (c.Email != null)
                        cOriginal.Email = c.Email;

                    if (c.CNPJ != null)
                        cOriginal.CNPJ = c.CNPJ;

                    if (c.Telefone1 != null)
                        cOriginal.Telefone1 = c.Telefone1;

                    if (c.Telefone2 != null)
                        cOriginal.Telefone2 = c.Telefone2;

                    if (c.Whatsapp != null)
                        cOriginal.Whatsapp = c.Whatsapp;

                    if (c.Descricao != null)
                        cOriginal.Descricao = c.Descricao;

                    if (c.Dominio != null)
                        cOriginal.Dominio = c.Dominio;

                    if (c.CEP != null)
                        cOriginal.CEP = c.CEP;

                    if (c.Logradouro != null)
                        cOriginal.Logradouro = c.Logradouro;

                    if (c.LogrNumero != null)
                        cOriginal.LogrNumero = c.LogrNumero;

                    if (c.Complemento != null)
                        cOriginal.Complemento = c.Complemento;

                    if (c.Bairro != null)
                        cOriginal.Bairro = c.Bairro;

                    if (c.UF != null)
                        cOriginal.UF = c.UF;

                    if (c.Cidade != null)
                        cOriginal.Cidade = c.Cidade;

                    if (c.AreaAtuacaoUF != null)
                        cOriginal.AreaAtuacaoUF = c.AreaAtuacaoUF;

                    if (c.AreaAtuacaoCidade != null)
                        cOriginal.AreaAtuacaoCidade = c.AreaAtuacaoCidade;

                    if (c.Status != null)
                        cOriginal.Status = c.Status;

                    if (c.Ativo != null)
                        cOriginal.Ativo = c.Ativo;

                    if (cOriginal.EmpresaCode == null)
                        cOriginal.EmpresaCode = Guid.NewGuid();

                    cOriginal.AtualizadoEm = DateTime.Now;

                    db.Entry(cOriginal).State = EntityState.Modified;
                    db.SaveChanges();

                    r.MsgExibir = "Empresa atualizado com sucesso";
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
            Empresa cOriginal = Retorna(_id);

            if (cOriginal == null)
            {
                r.MsgExibir = "Empresa não existente";
                r.Sucesso = false;
                r.CodigoStatus = 200;
            }
            else
            {
                try
                {


                    db.Empresas.Remove(cOriginal);
                    db.SaveChanges();

                    r.MsgExibir = "Empresa excluído com sucesso";
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