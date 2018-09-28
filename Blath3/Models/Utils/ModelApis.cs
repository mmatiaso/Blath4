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

}