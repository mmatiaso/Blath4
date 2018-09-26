using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Blath3.Models.Utils;

namespace Blath3.Models.Core
{
    public class CoreUsuario
    {
        private Blath3Entities db = new Blath3Entities();
        public Retorno r = new Retorno();
        public Generico g = new Generico();

        //list Usuarios
        public List<Usuario> Lista()
        {
            List<Usuario> lc = new List<Usuario>();
            lc = db.Usuarios.ToList();
            return lc;
        }

        //list Usuarios Ativas
        public List<Usuario> ListaAtivos()
        {
            List<Usuario> lc = new List<Usuario>();
            lc = db.Usuarios.Where(x => x.Ativo == true).ToList();
            return lc;
        }

        //Achar Usuario
        public Usuario Retorna(int _id)
        {
            return db.Usuarios.Find(_id);
        }

        //Achar Usuario
        public Usuario Retorna(Guid _id)
        {
            return db.Usuarios.Where(x => x.UsuarioCode == _id).FirstOrDefault();
        }

        //existe Usuario
        public bool Existe(Usuario c)
        {
            if (db.Usuarios.Where(x => x.UsuarioId == c.UsuarioId).Any())
            {
                return true;
            }
            if (db.Usuarios.Where(x => x.Email == c.Email).Any())
            {
                return true;
            }
            if (db.Usuarios.Where(x => x.CPF == c.CPF).Any())
            {
                return true;
            }

            return false;

        }

        //new Usuario
        public Retorno Nova(Usuario c)
        {

            if (Existe(c))
            {
                r.MsgExibir = "Usuario Já existe";
                r.Sucesso = false;
                r.CodigoStatus = 200;
            }
            else
            {
                try
                {
                    c.UsuarioCode = Guid.NewGuid();
                    c.CriadoEm = DateTime.Now;
                    c.AtualizadoEm = DateTime.Now;
                    c.Status = c.Status ?? "LEAD";
                    c.Senha = g.gerarSenha(8);
                    c.Ativo = true;

                    db.Usuarios.Add(c);
                    db.SaveChanges();
                    r.MsgExibir = "Usuario inserido com sucesso";
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

        //Edita Usuario
        public Retorno Edita(Usuario c)
        {
            Usuario cOriginal = Retorna(c.UsuarioId);

            if (cOriginal == null)
            {
                r.MsgExibir = "Usuario não existente";
                r.Sucesso = false;
                r.CodigoStatus = 200;
            }
            else
            {
                try
                {
                    if (c.Nome != null)
                        cOriginal.Nome = c.Nome;

                    if (c.Email != null)
                        cOriginal.Email = c.Email;

                    if (c.CPF != null)
                        cOriginal.CPF = c.CPF;

                    if (c.Telefone != null)
                        cOriginal.Telefone = c.Telefone;

                    if (c.Senha != null)
                        cOriginal.Senha = c.Senha;

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

                    if (c.Ativo != null)
                        cOriginal.Ativo = c.Ativo;

                    if (cOriginal.UsuarioCode == null)
                        cOriginal.UsuarioCode = Guid.NewGuid();

                    cOriginal.AtualizadoEm = DateTime.Now;

                    db.Entry(cOriginal).State = EntityState.Modified;
                    db.SaveChanges();

                    r.MsgExibir = "Usuario atualizado com sucesso";
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
            Usuario cOriginal = Retorna(_id);

            if (cOriginal == null)
            {
                r.MsgExibir = "Usuario não existente";
                r.Sucesso = false;
                r.CodigoStatus = 200;
            }
            else
            {
                try
                {


                    db.Usuarios.Remove(cOriginal);
                    db.SaveChanges();

                    r.MsgExibir = "Usuario excluído com sucesso";
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