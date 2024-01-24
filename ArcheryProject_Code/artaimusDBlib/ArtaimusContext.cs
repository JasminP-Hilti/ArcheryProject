using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace artaimusDBlib;

public partial class ArtaimusContext : DbContext
{
    public ArtaimusContext()
    {
    }

    public ArtaimusContext(DbContextOptions<ArtaimusContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventsHasPlayer> EventsHasPlayers { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Parcour> Parcours { get; set; }

    public virtual DbSet<Player> Players { get; set; }
     
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("events");

            entity.HasIndex(e => e.ParcoursId, "fk_events_parcours1_idx");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CountType)
                .HasColumnType("int(11)")
                .HasColumnName("countType");
            entity.Property(e => e.ParcoursId)
                .HasColumnType("int(11)")
                .HasColumnName("parcours_id");

            entity.HasOne(d => d.Parcours).WithMany(p => p.Events)
                .HasForeignKey(d => d.ParcoursId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_events_parcours1");
        });

        modelBuilder.Entity<EventsHasPlayer>(entity =>
        {
            entity.HasKey(e => new { e.EventsId, e.PlayersId }).HasName("PRIMARY");

            entity.ToTable("events_has_players");

            entity.HasIndex(e => e.EventsId, "fk_events_has_players_events1_idx");

            entity.HasIndex(e => e.PlayersId, "fk_events_has_players_players1_idx");

            entity.Property(e => e.EventsId)
                .HasColumnType("int(11)")
                .HasColumnName("events_id");
            entity.Property(e => e.PlayersId)
                .HasColumnType("int(11)")
                .HasColumnName("players_id");
            entity.Property(e => e.PointsTotal)
                .HasColumnType("int(11)")
                .HasColumnName("pointsTotal");

            entity.HasOne(d => d.Events).WithMany(p => p.EventsHasPlayers)
                .HasForeignKey(d => d.EventsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_events_has_players_events1");

            entity.HasOne(d => d.Players).WithMany(p => p.EventsHasPlayers)
                .HasForeignKey(d => d.PlayersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_events_has_players_players1");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("logins");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(45)
                .HasColumnName("password");
        });

        modelBuilder.Entity<Parcour>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("parcours");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CountAnimals)
                .HasColumnType("int(11)")
                .HasColumnName("countAnimals");
            entity.Property(e => e.Location)
                .HasMaxLength(45)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("players");

            entity.HasIndex(e => e.LoginsId, "fk_players_logins1_idx");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Admin)
                .HasColumnType("bit(1)")
                .HasColumnName("admin");
            entity.Property(e => e.FirstName)
                .HasMaxLength(45)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(45)
                .HasColumnName("lastName");
            entity.Property(e => e.LoginsId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("logins_id");
            entity.Property(e => e.Nickname)
                .HasMaxLength(45)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("nickname");

            entity.HasOne(d => d.Logins).WithMany(p => p.Players)
                .HasForeignKey(d => d.LoginsId)
                .HasConstraintName("fk_players_logins1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
