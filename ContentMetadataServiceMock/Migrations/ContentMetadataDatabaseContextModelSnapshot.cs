﻿// <auto-generated />
using System;
using ContentMetadataServiceMock.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ContentMetadataServiceMock.Migrations
{
    [DbContext(typeof(ContentMetadataDatabaseContext))]
    partial class ContentMetadataDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("ContentMetadataServiceMock.Persistance.Data.ContentCommentData", b =>
                {
                    b.Property<Guid>("ContentCommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ContentId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ContentCommentId");

                    b.HasIndex("ContentId");

                    b.ToTable("ContentComments");
                });

            modelBuilder.Entity("ContentMetadataServiceMock.Persistance.Data.ContentData", b =>
                {
                    b.Property<Guid>("ContentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Duration")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ImageFileId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("VideoFileId")
                        .HasColumnType("TEXT");

                    b.HasKey("ContentId");

                    b.ToTable("Contents");
                });

            modelBuilder.Entity("ContentMetadataServiceMock.Persistance.Data.LicenseRulesData", b =>
                {
                    b.Property<Guid>("Uuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ContentId")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Duration")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Price")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Uuid");

                    b.HasIndex("ContentId");

                    b.ToTable("LicenseRules");
                });

            modelBuilder.Entity("ContentMetadataServiceMock.Persistance.Data.ContentCommentData", b =>
                {
                    b.HasOne("ContentMetadataServiceMock.Persistance.Data.ContentData", "Content")
                        .WithMany("Comments")
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Content");
                });

            modelBuilder.Entity("ContentMetadataServiceMock.Persistance.Data.LicenseRulesData", b =>
                {
                    b.HasOne("ContentMetadataServiceMock.Persistance.Data.ContentData", "Content")
                        .WithMany("LicenseRules")
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Content");
                });

            modelBuilder.Entity("ContentMetadataServiceMock.Persistance.Data.ContentData", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("LicenseRules");
                });
#pragma warning restore 612, 618
        }
    }
}
