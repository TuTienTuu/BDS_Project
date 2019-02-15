namespace Model.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBModel : DbContext
    {
        public DBModel()
            : base("name=DBModel")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<MenuLv1> MenuLv1 { get; set; }
        public virtual DbSet<MenuLv2> MenuLv2 { get; set; }
        public virtual DbSet<MenuLv3> MenuLv3 { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsType> NewsTypes { get; set; }
        public virtual DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(e => e.News)
                .WithOptional(e => e.Account)
                .HasForeignKey(e => e.CreatedBy);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Projects)
                .WithOptional(e => e.Account)
                .HasForeignKey(e => e.CreatedBy);

            modelBuilder.Entity<NewsType>()
                .HasMany(e => e.News)
                .WithOptional(e => e.NewsType1)
                .HasForeignKey(e => e.NewsType);
        }
    }
}
