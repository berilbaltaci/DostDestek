﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DostDestek.Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HayvanDestekEntities : DbContext
    {
        public HayvanDestekEntities()
            : base("name=HayvanDestekEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Animal> Animal { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Topic> Topic { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<Veteriner> Veteriner { get; set; }
    }
}
