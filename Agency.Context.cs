﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp1
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AgencyContainer : DbContext
    {
        public AgencyContainer()
            : base("name=AgencyContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Agents> Agents { get; set; }
        public virtual DbSet<Apartments> Apartments { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Demand> Demand { get; set; }
        public virtual DbSet<DemandsApart> DemandsApart { get; set; }
        public virtual DbSet<DemandsHouse> DemandsHouse { get; set; }
        public virtual DbSet<DemandsLands> DemandsLands { get; set; }
        public virtual DbSet<Districts> Districts { get; set; }
        public virtual DbSet<Houses> Houses { get; set; }
        public virtual DbSet<Lands> Lands { get; set; }
        public virtual DbSet<Objects> Objects { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TypeDemands> TypeDemands { get; set; }
    }
}
