﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

#nullable disable

namespace EFCore.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.11");

            modelBuilder.Entity("Persistence.Models.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EmployerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SubstituteId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Chats");
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

            modelBuilder.Entity("Persistence.Models.JobConfirmation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ChatId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("EmployerId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsTaken")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SubstituteId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ChatId")
                        .IsUnique();

                    b.HasIndex("EmployerId");

                    b.HasIndex("SubstituteId");

                    b.ToTable("JobConfirmations");
                });

            modelBuilder.Entity("Persistence.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AuthorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ChatId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ChatId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Persistence.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("Persistence.Models.Employer", b =>
                {
                    b.HasBaseType("Persistence.Models.User");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("WorkPlace")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("Employer");
                });

            modelBuilder.Entity("Persistence.Models.Substitute", b =>
                {
                    b.HasBaseType("Persistence.Models.User");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("Substitute");
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
                        .WithMany("Gigs")
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

            modelBuilder.Entity("Persistence.Models.JobConfirmation", b =>
                {
                    b.HasOne("Persistence.Models.Chat", "Chat")
                        .WithOne("JobConfirmation")
                        .HasForeignKey("Persistence.Models.JobConfirmation", "ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Persistence.Models.User", "Employer")
                        .WithMany()
                        .HasForeignKey("EmployerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Persistence.Models.User", "Substitute")
                        .WithMany()
                        .HasForeignKey("SubstituteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("Employer");

                    b.Navigation("Substitute");
                });

            modelBuilder.Entity("Persistence.Models.Message", b =>
                {
                    b.HasOne("Persistence.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Persistence.Models.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Chat");
                });

            modelBuilder.Entity("Persistence.Models.Chat", b =>
                {
                    b.Navigation("JobConfirmation");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Persistence.Models.Gig", b =>
                {
                    b.Navigation("GigSubstitutes");
                });

            modelBuilder.Entity("Persistence.Models.Employer", b =>
                {
                    b.Navigation("EmployerSubstitutes");

                    b.Navigation("Gigs");
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
