//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Blath3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Anuncio
    {
        public int AnuncioId { get; set; }
        public System.Guid AnuncioCode { get; set; }
        public Nullable<int> EmpresaId { get; set; }
        public string Frase1 { get; set; }
        public string Frase2 { get; set; }
        public Nullable<int> ViewsAnuncio { get; set; }
        public Nullable<int> ViewsPagina { get; set; }
        public Nullable<bool> Ativo { get; set; }
        public Nullable<System.DateTime> CriadoEm { get; set; }
        public Nullable<System.DateTime> AtualizadoEm { get; set; }
    
        public virtual Empresa Empresa { get; set; }
    }
}