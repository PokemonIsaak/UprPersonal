﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RomanovEPractice3.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RomanovAEntities1 : DbContext
    {
        public RomanovAEntities1()
            : base("name=RomanovAEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<AvtorisationP> AvtorisationP { get; set; }
        public virtual DbSet<NewsItem> NewsItem { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<ProjectItem> ProjectItem { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<ShopIdName> ShopIdName { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<WorkerBe> WorkerBe { get; set; }
        public virtual DbSet<Workers> Workers { get; set; }
        public virtual DbSet<WorkersExpenses> WorkersExpenses { get; set; }
        public virtual DbSet<WorkersPosition> WorkersPosition { get; set; }
        public virtual DbSet<Zapros> Zapros { get; set; }
    }
}