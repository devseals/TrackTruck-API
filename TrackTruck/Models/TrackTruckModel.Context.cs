﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TrackTruckEntities : DbContext
    {
        public TrackTruckEntities()
            : base("name=TrackTruckEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<contact_petitions> contact_petitions { get; set; }
        public virtual DbSet<employees> employees { get; set; }
        public virtual DbSet<foodtrucks> foodtrucks { get; set; }
        public virtual DbSet<owners> owners { get; set; }
        public virtual DbSet<reviews> reviews { get; set; }
        public virtual DbSet<sales_records> sales_records { get; set; }
        public virtual DbSet<users> users { get; set; }
    }
}
