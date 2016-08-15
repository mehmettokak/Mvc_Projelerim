namespace HaberWeb.Models
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

        public virtual DbSet<aspnet_Applications> aspnet_Applications { get; set; }
        public virtual DbSet<aspnet_Membership> aspnet_Membership { get; set; }
        public virtual DbSet<aspnet_Paths> aspnet_Paths { get; set; }
        public virtual DbSet<aspnet_PersonalizationAllUsers> aspnet_PersonalizationAllUsers { get; set; }
        public virtual DbSet<aspnet_PersonalizationPerUser> aspnet_PersonalizationPerUser { get; set; }
        public virtual DbSet<aspnet_Profile> aspnet_Profile { get; set; }
        public virtual DbSet<aspnet_Roles> aspnet_Roles { get; set; }
        public virtual DbSet<aspnet_SchemaVersions> aspnet_SchemaVersions { get; set; }
        public virtual DbSet<aspnet_Users> aspnet_Users { get; set; }
        public virtual DbSet<aspnet_WebEvent_Events> aspnet_WebEvent_Events { get; set; }
        public virtual DbSet<H_Anket> H_Anket { get; set; }
        public virtual DbSet<H_Anket_Secenek> H_Anket_Secenek { get; set; }
        public virtual DbSet<H_Etiket> H_Etiket { get; set; }
        public virtual DbSet<H_Gorusler> H_Gorusler { get; set; }
        public virtual DbSet<H_Kategori> H_Kategori { get; set; }
        public virtual DbSet<H_Resim> H_Resim { get; set; }
        public virtual DbSet<H_Tip> H_Tip { get; set; }
        public virtual DbSet<H_Yazar> H_Yazar { get; set; }
        public virtual DbSet<H_Yorum> H_Yorum { get; set; }
        public virtual DbSet<Haber> Haber { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<aspnet_Applications>()
                .HasMany(e => e.aspnet_Membership)
                .WithRequired(e => e.aspnet_Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Applications>()
                .HasMany(e => e.aspnet_Paths)
                .WithRequired(e => e.aspnet_Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Applications>()
                .HasMany(e => e.aspnet_Roles)
                .WithRequired(e => e.aspnet_Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Applications>()
                .HasMany(e => e.aspnet_Users)
                .WithRequired(e => e.aspnet_Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Paths>()
                .HasOptional(e => e.aspnet_PersonalizationAllUsers)
                .WithRequired(e => e.aspnet_Paths);

            modelBuilder.Entity<aspnet_Roles>()
                .HasMany(e => e.aspnet_Users)
                .WithMany(e => e.aspnet_Roles)
                .Map(m => m.ToTable("aspnet_UsersInRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<aspnet_Users>()
                .HasOptional(e => e.aspnet_Membership)
                .WithRequired(e => e.aspnet_Users);

            modelBuilder.Entity<aspnet_Users>()
                .HasOptional(e => e.aspnet_Profile)
                .WithRequired(e => e.aspnet_Users);

            modelBuilder.Entity<aspnet_Users>()
                .HasMany(e => e.H_Gorusler)
                .WithOptional(e => e.aspnet_Users)
                .HasForeignKey(e => e.H_G_YazarID);

            modelBuilder.Entity<aspnet_Users>()
                .HasOptional(e => e.H_Yazar)
                .WithRequired(e => e.aspnet_Users);

            modelBuilder.Entity<aspnet_Users>()
                .HasMany(e => e.Haber)
                .WithOptional(e => e.aspnet_Users)
                .HasForeignKey(e => e.H_YazarID);

            modelBuilder.Entity<aspnet_WebEvent_Events>()
                .Property(e => e.EventId)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<aspnet_WebEvent_Events>()
                .Property(e => e.EventSequence)
                .HasPrecision(19, 0);

            modelBuilder.Entity<aspnet_WebEvent_Events>()
                .Property(e => e.EventOccurrence)
                .HasPrecision(19, 0);

            modelBuilder.Entity<H_Anket>()
                .HasMany(e => e.H_Anket_Secenek)
                .WithOptional(e => e.H_Anket)
                .HasForeignKey(e => e.H_A_S_AnketID);

            modelBuilder.Entity<H_Etiket>()
                .HasMany(e => e.Haber)
                .WithMany(e => e.H_Etiket)
                .Map(m => m.ToTable("Haber_Etiket").MapLeftKey("Etiket_ID").MapRightKey("Haber_ID"));

            modelBuilder.Entity<H_Gorusler>()
                .HasMany(e => e.H_Yorum)
                .WithOptional(e => e.H_Gorusler)
                .HasForeignKey(e => e.H_Y_GorusID);

            modelBuilder.Entity<H_Kategori>()
                .HasMany(e => e.H_Anket)
                .WithOptional(e => e.H_Kategori)
                .HasForeignKey(e => e.H_Anket_KategoriID);

            modelBuilder.Entity<H_Kategori>()
                .HasMany(e => e.H_Kategori1)
                .WithOptional(e => e.H_Kategori2)
                .HasForeignKey(e => e.H_K_UstkategoriID);

            modelBuilder.Entity<H_Kategori>()
                .HasMany(e => e.H_Yazar)
                .WithOptional(e => e.H_Kategori)
                .HasForeignKey(e => e.H_Y_KategoriID);

            modelBuilder.Entity<H_Kategori>()
                .HasMany(e => e.Haber)
                .WithOptional(e => e.H_Kategori)
                .HasForeignKey(e => e.H_KategoriID);

            modelBuilder.Entity<H_Tip>()
                .HasMany(e => e.Haber)
                .WithOptional(e => e.H_Tip)
                .HasForeignKey(e => e.H_TipID);

            modelBuilder.Entity<Haber>()
                .HasMany(e => e.H_Resim)
                .WithOptional(e => e.Haber)
                .HasForeignKey(e => e.H_R_HaberID);

            modelBuilder.Entity<Haber>()
                .HasMany(e => e.H_Yorum)
                .WithOptional(e => e.Haber)
                .HasForeignKey(e => e.H_Y_HaberID);
        }
    }
}
