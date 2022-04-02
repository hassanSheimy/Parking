﻿// <auto-generated />
using System;
using DarGates.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DarGates.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20220330080801_SeedPerHour")]
    partial class SeedPerHour
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DarGates.DB.GardErrorLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("EndPoint")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GardErrorLog");
                });

            modelBuilder.Entity("DarGates.DB.GardUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int?>("GateID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rank")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("UID")
                        .HasColumnType("bigint");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("GateID");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("DarGates.DB.Gate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrinterMac")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Gate");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "بوابه رقم 1",
                            PrinterMac = "DC:0D:30:CC:27:08"
                        });
                });

            modelBuilder.Entity("DarGates.DB.GateLog", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CivilCount")
                        .HasColumnType("int");

                    b.Property<float>("CivilPrice")
                        .HasColumnType("real");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedByUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Image1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("InDate")
                        .HasColumnType("Date");

                    b.Property<string>("InTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPayed")
                        .HasColumnType("bit");

                    b.Property<int>("MilitryCount")
                        .HasColumnType("int");

                    b.Property<float>("MilitryPrice")
                        .HasColumnType("real");

                    b.Property<DateTime?>("OutDate")
                        .HasColumnType("Date");

                    b.Property<string>("OutTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParkType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("QRCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Total")
                        .HasColumnType("real");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("DeletedByUserId");

                    b.HasIndex("UserId");

                    b.ToTable("GateLog");
                });

            modelBuilder.Entity("DarGates.DB.Invitation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("Date");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GardUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("GateLogId")
                        .HasColumnType("int");

                    b.Property<int>("InvitationTypeID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QrCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("GardUserId");

                    b.HasIndex("GateLogId");

                    b.HasIndex("InvitationTypeID");

                    b.ToTable("Invitation");
                });

            modelBuilder.Entity("DarGates.DB.InvitationType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("InvitationType");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Type = "VIP Invitation"
                        },
                        new
                        {
                            ID = 2,
                            Type = "Normal"
                        });
                });

            modelBuilder.Entity("DarGates.DB.OfficialHoliday", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("Date");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("Date");

                    b.HasKey("Id");

                    b.ToTable("OfficialHoliday");
                });

            modelBuilder.Entity("DarGates.DB.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DeletedByUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<float>("PriceInHoliday")
                        .HasColumnType("real");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DeletedByUserId");

                    b.ToTable("Owner");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDeleted = false,
                            Price = 5f,
                            PriceInHoliday = 32f,
                            Type = "Militry"
                        },
                        new
                        {
                            Id = 2,
                            IsDeleted = false,
                            Price = 10.5f,
                            PriceInHoliday = 15f,
                            Type = "Civil"
                        },
                        new
                        {
                            Id = 3,
                            IsDeleted = false,
                            Price = 30f,
                            PriceInHoliday = 20.5f,
                            Type = "عضو دار"
                        },
                        new
                        {
                            Id = 4,
                            IsDeleted = false,
                            Price = 20f,
                            PriceInHoliday = 20.5f,
                            Type = "ق.م"
                        },
                        new
                        {
                            Id = 5,
                            IsDeleted = false,
                            Price = 10f,
                            PriceInHoliday = 20.5f,
                            Type = "مدنى"
                        },
                        new
                        {
                            Id = 6,
                            IsDeleted = false,
                            Price = 10f,
                            PriceInHoliday = 20.5f,
                            Type = "انشطه"
                        });
                });

            modelBuilder.Entity("DarGates.DB.PrinterLog", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedByUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("GateLogID")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPayed")
                        .HasColumnType("bit");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<DateTime?>("PrintDate")
                        .HasColumnType("Date");

                    b.Property<string>("PrintTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RePrintReasonID")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("DeletedByUserId");

                    b.HasIndex("GateLogID");

                    b.HasIndex("RePrintReasonID");

                    b.HasIndex("UserID");

                    b.ToTable("PrinterLog");
                });

            modelBuilder.Entity("DarGates.DB.PrinterMacs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PhoneMac")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrinterMac")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PrinterMac");
                });

            modelBuilder.Entity("DarGates.DB.RePrintReason", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("RePrintReason");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Price = 5f,
                            Reason = "فقدان التذكره"
                        },
                        new
                        {
                            ID = 2,
                            Price = 0f,
                            Reason = "خطأ فى الطباعه"
                        });
                });

            modelBuilder.Entity("DarGates.DB.SignInLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GardUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LogInDate")
                        .HasColumnType("Date");

                    b.Property<string>("LogInTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LogOutDate")
                        .HasColumnType("Date");

                    b.Property<string>("LogOutTime")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GardUserId");

                    b.ToTable("SignInLog");
                });

            modelBuilder.Entity("DarGates.DB.WeeklyHoliday", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Day")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsHoliday")
                        .HasColumnType("bit");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("WeeklyHoliday");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Day = "Monday",
                            IsHoliday = false,
                            Number = 1
                        },
                        new
                        {
                            Id = 2,
                            Day = "Tuesday",
                            IsHoliday = false,
                            Number = 2
                        },
                        new
                        {
                            Id = 3,
                            Day = "Wednesday",
                            IsHoliday = false,
                            Number = 3
                        },
                        new
                        {
                            Id = 4,
                            Day = "Thursday",
                            IsHoliday = false,
                            Number = 4
                        },
                        new
                        {
                            Id = 5,
                            Day = "Friday",
                            IsHoliday = true,
                            Number = 5
                        },
                        new
                        {
                            Id = 6,
                            Day = "Saturday",
                            IsHoliday = true,
                            Number = 6
                        },
                        new
                        {
                            Id = 7,
                            Day = "Sunday",
                            IsHoliday = false,
                            Number = 0
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("DarGates.DB.GardUser", b =>
                {
                    b.HasOne("DarGates.DB.Gate", "Gate")
                        .WithMany("User")
                        .HasForeignKey("GateID");

                    b.Navigation("Gate");
                });

            modelBuilder.Entity("DarGates.DB.GateLog", b =>
                {
                    b.HasOne("DarGates.DB.GardUser", "DeletedByUser")
                        .WithMany("DeletedLogs")
                        .HasForeignKey("DeletedByUserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("DarGates.DB.GardUser", "User")
                        .WithMany("GateLogs")
                        .HasForeignKey("UserId");

                    b.Navigation("DeletedByUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DarGates.DB.Invitation", b =>
                {
                    b.HasOne("DarGates.DB.GardUser", "GardUser")
                        .WithMany("Invitations")
                        .HasForeignKey("GardUserId");

                    b.HasOne("DarGates.DB.GateLog", "GateLog")
                        .WithMany()
                        .HasForeignKey("GateLogId");

                    b.HasOne("DarGates.DB.InvitationType", "InvitationType")
                        .WithMany()
                        .HasForeignKey("InvitationTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GardUser");

                    b.Navigation("GateLog");

                    b.Navigation("InvitationType");
                });

            modelBuilder.Entity("DarGates.DB.Owner", b =>
                {
                    b.HasOne("DarGates.DB.GardUser", "DeletedByUser")
                        .WithMany()
                        .HasForeignKey("DeletedByUserId");

                    b.Navigation("DeletedByUser");
                });

            modelBuilder.Entity("DarGates.DB.PrinterLog", b =>
                {
                    b.HasOne("DarGates.DB.GardUser", "DeletedByUser")
                        .WithMany("DeletedPrinterLogs")
                        .HasForeignKey("DeletedByUserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("DarGates.DB.GateLog", "GateLog")
                        .WithMany("PrinterLogs")
                        .HasForeignKey("GateLogID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DarGates.DB.RePrintReason", "RePrintReason")
                        .WithMany()
                        .HasForeignKey("RePrintReasonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DarGates.DB.GardUser", "User")
                        .WithMany("PrinterLogs")
                        .HasForeignKey("UserID");

                    b.Navigation("DeletedByUser");

                    b.Navigation("GateLog");

                    b.Navigation("RePrintReason");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DarGates.DB.SignInLog", b =>
                {
                    b.HasOne("DarGates.DB.GardUser", "GardUser")
                        .WithMany("SignInLog")
                        .HasForeignKey("GardUserId");

                    b.Navigation("GardUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DarGates.DB.GardUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DarGates.DB.GardUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DarGates.DB.GardUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DarGates.DB.GardUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DarGates.DB.GardUser", b =>
                {
                    b.Navigation("DeletedLogs");

                    b.Navigation("DeletedPrinterLogs");

                    b.Navigation("GateLogs");

                    b.Navigation("Invitations");

                    b.Navigation("PrinterLogs");

                    b.Navigation("SignInLog");
                });

            modelBuilder.Entity("DarGates.DB.Gate", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("DarGates.DB.GateLog", b =>
                {
                    b.Navigation("PrinterLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
