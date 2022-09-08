using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace XD.Models
{
    public partial class GameOfThronesContext : DbContext
    {
        public GameOfThronesContext()
        {
        }

        public GameOfThronesContext(DbContextOptions<GameOfThronesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Angajat> Angajats { get; set; } = null!;
        public virtual DbSet<Concediu> Concedius { get; set; } = null!;
        public virtual DbSet<Echipa> Echipas { get; set; } = null!;
        public virtual DbSet<StareConcediu> StareConcedius { get; set; } = null!;
        public virtual DbSet<TipConcediu> TipConcedius { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server =ts2112\\SQLEXPRESS; Database =GameOfThrones; User Id =internship2022; Password =int; MultipleActiveResultSets = true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Angajat>(entity =>
            {
                entity.ToTable("Angajat");

                entity.HasIndex(e => e.Email, "UQ__Angajat__A9D10534B16E5FA0")
                    .IsUnique();

                entity.Property(e => e.Cnp)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("CNP");

                entity.Property(e => e.DataAngajarii).HasColumnType("date");

                entity.Property(e => e.DataNasterii).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Numartelefon)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nume)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Parola)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Prenume)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Salariu).HasColumnType("numeric(19, 2)");

                entity.Property(e => e.SeriaNumarBuletin)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEchipaNavigation)
                    .WithMany(p => p.Angajats)
                    .HasForeignKey(d => d.IdEchipa)
                    .HasConstraintName("FK__Angajat__IdEchip__3B60C8C7");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.InverseManager)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK__Angajat__Manager__3A6CA48E");
            });

            modelBuilder.Entity<Concediu>(entity =>
            {
                entity.ToTable("Concediu");

                entity.Property(e => e.DataInceput).HasColumnType("date");

                entity.Property(e => e.DataSfarsit).HasColumnType("date");

                entity.HasOne(d => d.Angajat)
                    .WithMany(p => p.ConcediuAngajats)
                    .HasForeignKey(d => d.AngajatId)
                    .HasConstraintName("FK__Concediu__Angaja__44EA3301");

                entity.HasOne(d => d.Inlocuitor)
                    .WithMany(p => p.ConcediuInlocuitors)
                    .HasForeignKey(d => d.InlocuitorId)
                    .HasConstraintName("FK__Concediu__Inlocu__4301EA8F");

                entity.HasOne(d => d.StareConcediu)
                    .WithMany(p => p.Concedius)
                    .HasForeignKey(d => d.StareConcediuId)
                    .HasConstraintName("FK__Concediu__StareC__43F60EC8");

                entity.HasOne(d => d.TipConcediu)
                    .WithMany(p => p.Concedius)
                    .HasForeignKey(d => d.TipConcediuId)
                    .HasConstraintName("FK__Concediu__TipCon__420DC656");
            });

            modelBuilder.Entity<Echipa>(entity =>
            {
                entity.ToTable("Echipa");

                entity.Property(e => e.Descriere)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nume)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Poza)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StareConcediu>(entity =>
            {
                entity.ToTable("StareConcediu");

                entity.Property(e => e.Cod)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Nume)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipConcediu>(entity =>
            {
                entity.ToTable("TipConcediu");

                entity.Property(e => e.Cod)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Nume)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NrZile);
                    
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
