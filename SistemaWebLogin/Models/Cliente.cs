//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistemaWebLogin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cliente()
        {
            this.Detalle_Cliente = new HashSet<Detalle_Cliente>();
        }
    
        public int IdCli { get; set; }
        public Nullable<int> IdUsu { get; set; }
        public string Nom { get; set; }
        public string Ape { get; set; }
        public string Edad { get; set; }
        public Nullable<System.DateTime> FecNac { get; set; }
        public string Ema { get; set; }
        public string Dir { get; set; }
        public Nullable<System.DateTime> FecReg { get; set; }
    
        public virtual Usuario Usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Detalle_Cliente> Detalle_Cliente { get; set; }
    }
}
