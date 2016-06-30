using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using EFCoreDbContext;

namespace EFCoreDbContext.Migrations
{
    [DbContext(typeof(SamuraiContext))]
    [Migration("20160630104746_fixtablenames")]
    partial class fixtablenames
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SamuraiTracker.Domain.Maker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Makers");
                });

            modelBuilder.Entity("SamuraiTracker.Domain.Quote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("SamuraiId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("SamuraiId");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("SamuraiTracker.Domain.Samurai", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Samurais");
                });

            modelBuilder.Entity("SamuraiTracker.Domain.SamuraiBattle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BattleId");

                    b.Property<DateTime>("DateJoined");

                    b.Property<int>("SamuraiId");

                    b.HasKey("Id");

                    b.HasIndex("SamuraiId");

                    b.ToTable("SamuraiBattles");
                });

            modelBuilder.Entity("SamuraiTracker.Domain.SecretIdentity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("RealName");

                    b.Property<int>("SamuraiId");

                    b.HasKey("Id");

                    b.HasIndex("SamuraiId")
                        .IsUnique();

                    b.ToTable("Secrets");
                });

            modelBuilder.Entity("SamuraiTracker.Domain.Sword", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MakerId");

                    b.Property<int>("SamuraId");

                    b.Property<int?>("SamuraiId");

                    b.Property<int>("WeightGrams");

                    b.HasKey("Id");

                    b.HasIndex("MakerId");

                    b.HasIndex("SamuraiId");

                    b.ToTable("Swords");
                });

            modelBuilder.Entity("SamuraiTracker.Domain.Quote", b =>
                {
                    b.HasOne("SamuraiTracker.Domain.Samurai")
                        .WithMany("Quotes")
                        .HasForeignKey("SamuraiId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SamuraiTracker.Domain.SamuraiBattle", b =>
                {
                    b.HasOne("SamuraiTracker.Domain.Samurai")
                        .WithMany("SamuraiBattles")
                        .HasForeignKey("SamuraiId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SamuraiTracker.Domain.SecretIdentity", b =>
                {
                    b.HasOne("SamuraiTracker.Domain.Samurai", "Samurai")
                        .WithOne("SecretIdentity")
                        .HasForeignKey("SamuraiTracker.Domain.SecretIdentity", "SamuraiId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SamuraiTracker.Domain.Sword", b =>
                {
                    b.HasOne("SamuraiTracker.Domain.Maker", "Maker")
                        .WithMany("Swords")
                        .HasForeignKey("MakerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SamuraiTracker.Domain.Samurai")
                        .WithMany("Swords")
                        .HasForeignKey("SamuraiId");
                });
        }
    }
}
