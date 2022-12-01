﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20221130212046_UpdatedEmpAndSubMatching")]
    partial class UpdatedEmpAndSubMatching
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("Persistence.Models.Employer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Employers");
                });

            modelBuilder.Entity("Persistence.Models.EmployerSubstitute", b =>
                {
                    b.Property<int>("EmployerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SubstituteId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("PublicationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<bool>("WantsToMatch")
                        .HasColumnType("INTEGER");

                    b.HasKey("EmployerId", "SubstituteId");

                    b.HasIndex("SubstituteId");

                    b.ToTable("EmployerSubstitute");
                });

            modelBuilder.Entity("Persistence.Models.Gig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EmployerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EmployerId");

                    b.ToTable("Gigs");
                });

            modelBuilder.Entity("Persistence.Models.GigSubstitute", b =>
                {
                    b.Property<int>("SubstituteId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GigId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("PublicationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<bool>("WantsToMatch")
                        .HasColumnType("INTEGER");

                    b.HasKey("SubstituteId", "GigId");

                    b.HasIndex("GigId");

                    b.ToTable("GigSubstitute");
                });

            modelBuilder.Entity("Persistence.Models.Substitute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Substitutes");
                });

            modelBuilder.Entity("Persistence.Models.EmployerSubstitute", b =>
                {
                    b.HasOne("Persistence.Models.Employer", "Employer")
                        .WithMany("EmployerSubstitutes")
                        .HasForeignKey("EmployerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Persistence.Models.Substitute", "Substitute")
                        .WithMany("EmployerSubstitutes")
                        .HasForeignKey("SubstituteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employer");

                    b.Navigation("Substitute");
                });

            modelBuilder.Entity("Persistence.Models.Gig", b =>
                {
                    b.HasOne("Persistence.Models.Employer", "Employer")
                        .WithMany()
                        .HasForeignKey("EmployerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employer");
                });

            modelBuilder.Entity("Persistence.Models.GigSubstitute", b =>
                {
                    b.HasOne("Persistence.Models.Gig", "Gig")
                        .WithMany("GigSubstitutes")
                        .HasForeignKey("GigId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Persistence.Models.Substitute", "Substitute")
                        .WithMany("GigSubstitutes")
                        .HasForeignKey("SubstituteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gig");

                    b.Navigation("Substitute");
                });

            modelBuilder.Entity("Persistence.Models.Employer", b =>
                {
                    b.Navigation("EmployerSubstitutes");
                });

            modelBuilder.Entity("Persistence.Models.Gig", b =>
                {
                    b.Navigation("GigSubstitutes");
                });

            modelBuilder.Entity("Persistence.Models.Substitute", b =>
                {
                    b.Navigation("EmployerSubstitutes");

                    b.Navigation("GigSubstitutes");
                });
#pragma warning restore 612, 618
        }
    }
}