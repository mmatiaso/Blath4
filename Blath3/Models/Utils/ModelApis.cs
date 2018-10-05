using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blath3.Models.Utils
{
    
    public class ModelApis
    {
    }

    //Menu -> Categoria >> Menu()
    public class MenuApi
    {
        public List<Categoria> menushow { get; set; }
    }

    //busca do site
    public class BuscaApi
    {

        private Blath3Entities db = new Blath3Entities();
        


        public string Nome { get; set; }
        public string Link { get; set; }

        public List<BuscaApi> RetornaBusca(string word)
        {
            
            List<BuscaApi> Lb = new List<BuscaApi>();
            

            if (string.IsNullOrEmpty(word))
            {
                //
            }
            else
            {
                //categorias
                var cats = db.Categorias.Where(x => x.Nome.Contains(word) || x.Tags.Contains(word) || x.Descricao.Contains(word));
                if(cats.Count() > 0)
                {
                    foreach (Categoria c in cats)
                    {
                        BuscaApi b = new BuscaApi();
                        b.Nome = c.Nome;
                        b.Link = "/" + c.NomeLink;
                        Lb.Add(b);
                    }
                }

                //subcategorias
                var subcats = db.Subcategorias.Where(x => x.Nome.Contains(word) || x.Tags.Contains(word) || x.Descricao.Contains(word));
                if (subcats.Count() > 0)
                {
                    foreach (Subcategoria s in subcats)
                    {
                        BuscaApi b = new BuscaApi();
                        b.Nome = s.Nome;
                        b.Link = "/s/" + s.NomeLink;
                        Lb.Add(b);
                    }
                }


            }

            return Lb;
        }
    }

    //Card Empresa
    public class CardEmpresa
    {
        public Guid CodeEmpresa { get; set; }
        public string NomeEmpresa { get; set; }
        public string ImagemUrl { get; set; }
        public string Dominio { get; set; }
        public string Nota { get; set; }
        public string AvaliacaoLabel { get; set; }
        public int QtdAvaliacoes { get; set; }
        public string Texto1 { get; set; }
        public string Texto2 { get; set; }
        public string Uf { get; set; }
        public string Cidade { get; set; }
        public string EmpSubcategorias { get; set; }

        

    } 

    public class Report_CardEmpresa
    {
        public List<CardEmpresa> cardEmpresaShow { get; set; }
        public int numRegistros { get; set; }
    }

    

    public class ListaCardReq
    {
       
        public int catId { get; set; }
        public int subcatId { get; set; }
        public string uf { get; set; }
        public string cid { get; set; }
        public int t { get; set; }
        public int s { get; set; }
    }


}