using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blath3.Models.Utils
{
    public class Retorno
    {
        public string MsgExibir { get; set; }
        public int CodigoStatus { get; set; }
        public string MsgDev { get; set; }
        public bool Sucesso { get; set; }
    }
}