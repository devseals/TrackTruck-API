//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TrackTruck.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class employees
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public employees()
        {
            this.sales_records = new HashSet<sales_records>();
        }
    
        public int employee_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int owner_id { get; set; }
        public string name { get; set; }
    
        public virtual owners owners { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sales_records> sales_records { get; set; }
    }
}