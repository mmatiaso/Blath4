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
    
    public partial class PedEmpChat
    {
        public int PedEmpChatId { get; set; }
        public Nullable<int> PedEmpId { get; set; }
        public string Emissor { get; set; }
        public Nullable<System.DateTime> CriadoEm { get; set; }
        public Nullable<System.DateTime> AtualizadoEm { get; set; }
        public string Chat { get; set; }
    
        public virtual PedEmp PedEmp { get; set; }
    }
}
