﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UserLibrary
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class 분산컴퓨팅_기반_스마트_보관함_관리시스템Entities : DbContext
    {
        public 분산컴퓨팅_기반_스마트_보관함_관리시스템Entities()
            : base("name=분산컴퓨팅_기반_스마트_보관함_관리시스템Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountingSubject> AccountingSubjects { get; set; }
        public virtual DbSet<AccountType> AccountTypes { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerType> CustomerTypes { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Facility> Facilities { get; set; }
        public virtual DbSet<FakeAccountInfo> FakeAccountInfoes { get; set; }
        public virtual DbSet<Fare> Fares { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<PurchaseItem> PurchaseItems { get; set; }
        public virtual DbSet<Reciept> Reciepts { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Storage> Storages { get; set; }
        public virtual DbSet<StorageSize> StorageSizes { get; set; }
        public virtual DbSet<StorageType> StorageTypes { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<TransactionType> TransactionTypes { get; set; }
    }
}