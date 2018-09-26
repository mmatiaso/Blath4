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
    
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            this.Empresas = new HashSet<Empresa>();
            this.Pedidoes = new HashSet<Pedido>();
        }
    
        public int UsuarioId { get; set; }
        public System.Guid UsuarioCode { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string Senha { get; set; }
        public string UF { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public string CEP { get; set; }
        public string Telefone { get; set; }
        public string FacebookId { get; set; }
        public Nullable<bool> Ativo { get; set; }
        public string MktCode { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> CriadoEm { get; set; }
        public Nullable<System.DateTime> AtualizadoEm { get; set; }
        public string LogrNumero { get; set; }
        public string Complemento { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Empresa> Empresas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pedido> Pedidoes { get; set; }
    }
}