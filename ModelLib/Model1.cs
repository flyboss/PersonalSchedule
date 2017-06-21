namespace ModelLib
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<action> action { get; set; }
        public virtual DbSet<_event> _event { get; set; }
        public virtual DbSet<follow> follow { get; set; }
        public virtual DbSet<link> link { get; set; }
        public virtual DbSet<mark> mark { get; set; }
        public virtual DbSet<tag> tag { get; set; }
        public virtual DbSet<user> user { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<action>()
                .Property(e => e.e_id)
                .IsUnicode(false);

            modelBuilder.Entity<action>()
                .Property(e => e.a_title)
                .IsUnicode(false);

            modelBuilder.Entity<action>()
                .Property(e => e.a_content)
                .IsUnicode(false);

            modelBuilder.Entity<_event>()
                .Property(e => e.e_id)
                .IsUnicode(false);

            modelBuilder.Entity<_event>()
                .Property(e => e.u_id)
                .IsUnicode(false);

            modelBuilder.Entity<_event>()
                .Property(e => e.e_title)
                .IsUnicode(false);

            modelBuilder.Entity<_event>()
                .Property(e => e.e_content)
                .IsUnicode(false);

            modelBuilder.Entity<_event>()
                .Property(e => e.e_auth)
                .IsUnicode(false);

            modelBuilder.Entity<_event>()
                .Property(e => e.e_type)
                .IsUnicode(false);

            modelBuilder.Entity<_event>()
                .HasMany(e => e.action)
                .WithRequired(e => e._event)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<_event>()
                .HasMany(e => e.link)
                .WithRequired(e => e._event)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<_event>()
                .HasMany(e => e.mark)
                .WithRequired(e => e._event)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<_event>()
                .HasMany(e => e.tag)
                .WithRequired(e => e._event)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<follow>()
                .Property(e => e.u_id)
                .IsUnicode(false);

            modelBuilder.Entity<follow>()
                .Property(e => e.f_u_id)
                .IsUnicode(false);

            modelBuilder.Entity<link>()
                .Property(e => e.e_id)
                .IsUnicode(false);

            modelBuilder.Entity<link>()
                .Property(e => e.l_title)
                .IsUnicode(false);

            modelBuilder.Entity<link>()
                .Property(e => e.l_src)
                .IsUnicode(false);

            modelBuilder.Entity<link>()
                .Property(e => e.l_content)
                .IsUnicode(false);

            modelBuilder.Entity<link>()
                .Property(e => e.l_type)
                .IsUnicode(false);

            modelBuilder.Entity<mark>()
                .Property(e => e.u_id)
                .IsUnicode(false);

            modelBuilder.Entity<mark>()
                .Property(e => e.e_id)
                .IsUnicode(false);

            modelBuilder.Entity<tag>()
                .Property(e => e.e_id)
                .IsUnicode(false);

            modelBuilder.Entity<tag>()
                .Property(e => e.t_title)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.u_id)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.u_name)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.u_pwd)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.u_auth)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.u_photo)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e._event)
                .WithRequired(e => e.user)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.follow)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.f_u_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.follow1)
                .WithRequired(e => e.user1)
                .HasForeignKey(e => e.u_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.mark)
                .WithRequired(e => e.user)
                .WillCascadeOnDelete(false);
        }
    }
}
