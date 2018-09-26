using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Blath3.Models.Utils
{
    public class Generico
    {
        public void LogExcecao(Retorno r)
        {
            //implementar
        }

        public bool SegurancaAPI(HttpRequestMessage hr)
        {
            //implementar
            //var re = Request;
            var headers = hr.Headers;
            string token = "";
            Retorno r = new Retorno();

            if (headers.Contains("Custom"))
            {
                token = headers.GetValues("Custom").First();
            }
            if (token != "xpto")
            {

                return false;
            }
            return true;

        }

        public string gerarSenha(int passwordLength)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
            char[] chars = new char[passwordLength];
            Random rd = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
    }

    
}