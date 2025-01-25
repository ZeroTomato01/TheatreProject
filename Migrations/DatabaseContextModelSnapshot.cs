﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheatreProject.Models;

#nullable disable

namespace TheatreProject.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("TheatreProject.Models.Admin", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("AdminId");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Admin");

                    b.HasData(
                        new
                        {
                            AdminId = 1,
                            Email = "admin1@example.com",
                            Password = "^�H��(qQ��o��)'s`=\rj���*�rB�",
                            UserName = "admin1"
                        },
                        new
                        {
                            AdminId = 2,
                            Email = "admin2@example.com",
                            Password = "\\N@6��G��Ae=j_��a%0�QU��\\",
                            UserName = "admin2"
                        },
                        new
                        {
                            AdminId = 3,
                            Email = "admin3@example.com",
                            Password = "�j\\��f������x�s+2��D�o���",
                            UserName = "admin3"
                        },
                        new
                        {
                            AdminId = 4,
                            Email = "admin4@example.com",
                            Password = "�].��g��Պ��t��?��^�T��`aǳ",
                            UserName = "admin4"
                        },
                        new
                        {
                            AdminId = 5,
                            Email = "admin5@example.com",
                            Password = "E�=���:�-����gd����bF��80]�",
                            UserName = "admin5"
                        });
                });

            modelBuilder.Entity("TheatreProject.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReservationIds")
                        .HasColumnType("TEXT");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("TheatreProject.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AmountOfTickets")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TheatreShowDateId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Used")
                        .HasColumnType("INTEGER");

                    b.HasKey("ReservationId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("TheatreShowDateId");

                    b.ToTable("Reservation");
                });

            modelBuilder.Entity("TheatreProject.Models.TheatreShow", b =>
                {
                    b.Property<int>("TheatreShowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<int>("VenueId")
                        .HasColumnType("INTEGER");

                    b.HasKey("TheatreShowId");

                    b.HasIndex("VenueId");

                    b.ToTable("TheatreShow");

                    b.HasData(
                        new
                        {
                            TheatreShowId = 1,
                            Description = "moo",
                            Price = 20.0,
                            Title = "Cow's Movie",
                            VenueId = 1
                        });
                });

            modelBuilder.Entity("TheatreProject.Models.TheatreShowDate", b =>
                {
                    b.Property<int>("TheatreShowDateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateAndTime")
                        .HasColumnType("TEXT");

                    b.Property<int?>("TheatreShowId")
                        .HasColumnType("INTEGER");

                    b.HasKey("TheatreShowDateId");

                    b.HasIndex("TheatreShowId");

                    b.ToTable("TheatreShowDate");

                    b.HasData(
                        new
                        {
                            TheatreShowDateId = 1,
                            DateAndTime = new DateTime(2025, 1, 22, 11, 44, 30, 565, DateTimeKind.Local).AddTicks(6532),
                            TheatreShowId = 1
                        });
                });

            modelBuilder.Entity("TheatreProject.Models.Venue", b =>
                {
                    b.Property<int>("VenueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Capacity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("VenueId");

                    b.ToTable("Venue");

                    b.HasData(
                        new
                        {
                            VenueId = 1,
                            Capacity = 300,
                            Name = "The Pen"
                        });
                });

            modelBuilder.Entity("TheatreProject.Models.Reservation", b =>
                {
                    b.HasOne("TheatreProject.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("TheatreProject.Models.TheatreShowDate", "TheatreShowDate")
                        .WithMany("Reservations")
                        .HasForeignKey("TheatreShowDateId");

                    b.Navigation("Customer");

                    b.Navigation("TheatreShowDate");
                });

            modelBuilder.Entity("TheatreProject.Models.TheatreShow", b =>
                {
                    b.HasOne("TheatreProject.Models.Venue", "Venue")
                        .WithMany("TheatreShows")
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("TheatreProject.Models.TheatreShowDate", b =>
                {
                    b.HasOne("TheatreProject.Models.TheatreShow", "TheatreShow")
                        .WithMany("TheatreShowDates")
                        .HasForeignKey("TheatreShowId");

                    b.Navigation("TheatreShow");
                });

            modelBuilder.Entity("TheatreProject.Models.TheatreShow", b =>
                {
                    b.Navigation("TheatreShowDates");
                });

            modelBuilder.Entity("TheatreProject.Models.TheatreShowDate", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("TheatreProject.Models.Venue", b =>
                {
                    b.Navigation("TheatreShows");
                });
#pragma warning restore 612, 618
        }
    }
}
