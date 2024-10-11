﻿// <auto-generated />
using System;
using GamesCatalog.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GamesCatalog.Server.Migrations
{
    [DbContext(typeof(GamesDbContext))]
    partial class GamesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("GamePlatform", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlatformsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("GameId", "PlatformsId");

                    b.HasIndex("PlatformsId");

                    b.ToTable("GamePlatform");
                });

            modelBuilder.Entity("GameTag", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TagsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("GameId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("GameTag");
                });

            modelBuilder.Entity("GamesCatalog.Server.Data.Catalog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Catalogs");
                });

            modelBuilder.Entity("GamesCatalog.Server.Data.CatalogLink", b =>
                {
                    b.Property<int>("CatalogId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GameId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("TEXT");

                    b.HasKey("CatalogId", "GameId");

                    b.HasIndex("GameId");

                    b.ToTable("CatalogLinks");
                });

            modelBuilder.Entity("GamesCatalog.Server.Data.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("GamesCatalog.Server.Data.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ContentsUrls")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("TEXT");

                    b.Property<int?>("DeveloperId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsReleased")
                        .HasColumnType("boolean");

                    b.Property<int?>("ParentGameId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PreviewUrl")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("decimal(8,2)");

                    b.Property<int?>("PublisherId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Rating")
                        .HasColumnType("tinyint");

                    b.Property<string>("Requirements")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("YearOfRelease")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("DeveloperId");

                    b.HasIndex("ParentGameId");

                    b.HasIndex("PublisherId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("GamesCatalog.Server.Data.Platform", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Platforms");
                });

            modelBuilder.Entity("GamesCatalog.Server.Data.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("GamePlatform", b =>
                {
                    b.HasOne("GamesCatalog.Server.Data.Game", null)
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GamesCatalog.Server.Data.Platform", null)
                        .WithMany()
                        .HasForeignKey("PlatformsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameTag", b =>
                {
                    b.HasOne("GamesCatalog.Server.Data.Game", null)
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GamesCatalog.Server.Data.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GamesCatalog.Server.Data.CatalogLink", b =>
                {
                    b.HasOne("GamesCatalog.Server.Data.Catalog", "Catalog")
                        .WithMany()
                        .HasForeignKey("CatalogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GamesCatalog.Server.Data.Game", "Game")
                        .WithMany("CatalogsLinks")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Catalog");

                    b.Navigation("Game");
                });

            modelBuilder.Entity("GamesCatalog.Server.Data.Game", b =>
                {
                    b.HasOne("GamesCatalog.Server.Data.Company", "Developer")
                        .WithMany()
                        .HasForeignKey("DeveloperId");

                    b.HasOne("GamesCatalog.Server.Data.Game", "ParentGame")
                        .WithMany("Dlcs")
                        .HasForeignKey("ParentGameId");

                    b.HasOne("GamesCatalog.Server.Data.Company", "Publisher")
                        .WithMany()
                        .HasForeignKey("PublisherId");

                    b.Navigation("Developer");

                    b.Navigation("ParentGame");

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("GamesCatalog.Server.Data.Game", b =>
                {
                    b.Navigation("CatalogsLinks");

                    b.Navigation("Dlcs");
                });
#pragma warning restore 612, 618
        }
    }
}
