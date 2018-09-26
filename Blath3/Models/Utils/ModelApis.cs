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
        public Retorno r = new Retorno();
        public Generico g = new Generico();


        public List<string> BuscaResulta { get; set; }
        //public string Link { get; set; }

        public BuscaApi RetornaBusca(string word)
        {
            string lurl = "";
            BuscaApi b = new BuscaApi();

            if (string.IsNullOrEmpty(word))
            {
                //
            }
            else
            {
                var cats = db.Categorias.Where(x => x.Nome.Contains(word) || x.Tags.Contains(word) || x.Descricao.Contains(word));
                if(cats.Count() > 0)
                {
                    foreach (Categoria c in cats)
                    {
                        lurl = "<a href='/" + c.NomeLink + "'>" + c.Nome + "</a>";
                        b.BuscaResulta.Add(lurl);

                    }
                }
                
                
            }

            return b;
        }
    }

}