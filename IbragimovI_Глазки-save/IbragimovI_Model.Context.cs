﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IbragimovI_Глазки_save
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    


    public partial class ИбрагимовИ_ГлазкиSaveEntities : DbContext
    {

        private static ИбрагимовИ_ГлазкиSaveEntities _context;

        public static ИбрагимовИ_ГлазкиSaveEntities GetContext()
        {
            if (_context == null)
                _context = new ИбрагимовИ_ГлазкиSaveEntities();

            return _context;
        }


        public ИбрагимовИ_ГлазкиSaveEntities()
            : base("name=ИбрагимовИ_ГлазкиSaveEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Agent> Agent { get; set; }
        public virtual DbSet<AgentPriorityHistory> AgentPriorityHistory { get; set; }
        public virtual DbSet<AgentType> AgentType { get; set; }
        public virtual DbSet<Material> Material { get; set; }
        public virtual DbSet<MaterialCountHistory> MaterialCountHistory { get; set; }
        public virtual DbSet<MaterialType> MaterialType { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCostHistory> ProductCostHistory { get; set; }
        public virtual DbSet<ProductMaterial> ProductMaterial { get; set; }
        public virtual DbSet<ProductSale> ProductSale { get; set; }
        public virtual DbSet<ProductType> ProductType { get; set; }
        public virtual DbSet<Shop> Shop { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
    }
}
