using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CoreWeb7.Data // noktalı virgül koy
{

    public class AppUser:IdentityUser<int>
    {
        public string Fullname { get; set; }
    }

    public class AppRole : IdentityRole<int>
    {
        public string Title { get; set; }
    }

    public class OBSContext : IdentityDbContext<AppUser, AppRole, int> 
    {
        public OBSContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Ogrenci> Ogrenciler { get; set; }
        public DbSet<Ogretmen> Ogretmenler { get; set; }
        public DbSet<Ders> Dersler { get; set; }
        public DbSet<Bolum> Bolumler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ders>()
                .Property(col => col.Adi)
                .HasColumnType("varchar")
                .HasMaxLength(50);

            modelBuilder.Entity<Ders>()
                .HasIndex(col => col.Adi)
                .IsUnique();

            modelBuilder.Entity<Ogrenci>()
                .HasIndex(col => new { col.Adi, col.Soyadi })
                .IsUnique();

            modelBuilder.Entity<Ders>().HasData(
                new Ders { Id = 1, Adi = "JS" },
                new Ders { Id = 2, Adi = "Sql" }

                );
        }
    }

    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }

    public class PersonBase : EntityBase
    {
        [Column(TypeName = "varchar")]
        [StringLength(100)]
        public string Adi { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(100)]
        public string GobekAdi { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(100)]
        public string Soyadi { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(200)]
        public string EPosta { get; set; }
    }

    public class Ogrenci : PersonBase
    {
        // ctrl h       
        public int No { get; set; }
        public virtual List<Ders> Dersler { get; set; }
    }

    public class Ogretmen : PersonBase
    {
        public virtual List<Ders> Dersler { get; set; }

        //public int? BolumId { get; set; }    
        public virtual Bolum Bolum { get; set; }
    }

    public class Ders : EntityBase
    {
        public string Adi { get; set; }

        //public int OgretmenId { get; set; }
        //public virtual Ogretmen Ogretmen { get; set; }

        public virtual List<Ogrenci> Ogrenciler { get; set; }
        public virtual List<Ogretmen> Ogretmenler { get; set; }
    }

    public class Bolum : EntityBase
    {
        public string Adi { get; set; }

        public int OgretmenId { get; set; }
        public virtual Ogretmen Ogretmen { get; set; }
    }
}

