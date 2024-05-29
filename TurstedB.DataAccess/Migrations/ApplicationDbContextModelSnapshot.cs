﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TrustedB.DataAccess.Data;

#nullable disable

namespace TrustedB.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("character varying(21)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("TrustedB.Models.Attachments", b =>
                {
                    b.Property<Guid>("FileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("text");

                    b.Property<string>("AttachmentSetDate")
                        .HasColumnType("text");

                    b.Property<string>("FileName")
                        .HasColumnType("text");

                    b.Property<string>("FilePath")
                        .HasColumnType("text");

                    b.Property<string>("FileType")
                        .HasColumnType("text");

                    b.Property<string>("MainFile")
                        .HasColumnType("text");

                    b.Property<Guid?>("TopicId")
                        .HasColumnType("uuid");

                    b.Property<int?>("stateId")
                        .HasColumnType("integer");

                    b.HasKey("FileId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("TopicId");

                    b.HasIndex("stateId");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("TrustedB.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("ArabicName")
                        .HasColumnType("text");

                    b.Property<string>("EnglishName")
                        .HasColumnType("text");

                    b.HasKey("CategoryId");

                    b.ToTable("Category");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            ArabicName = "توجيه",
                            EnglishName = "Guide"
                        },
                        new
                        {
                            CategoryId = 2,
                            ArabicName = "صور",
                            EnglishName = "Images"
                        },
                        new
                        {
                            CategoryId = 3,
                            ArabicName = "مرئيات",
                            EnglishName = "Videos"
                        },
                        new
                        {
                            CategoryId = 4,
                            ArabicName = "صوتيات",
                            EnglishName = "Audio"
                        });
                });

            modelBuilder.Entity("TrustedB.Models.CommentHistory", b =>
                {
                    b.Property<Guid>("CommentHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("text");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<string>("CommentSetDate")
                        .HasColumnType("text");

                    b.Property<Guid?>("TopicId")
                        .HasColumnType("uuid");

                    b.Property<int?>("stateId")
                        .HasColumnType("integer");

                    b.HasKey("CommentHistoryId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("TopicId");

                    b.HasIndex("stateId");

                    b.ToTable("CommentHistory");
                });

            modelBuilder.Entity("TrustedB.Models.StateHistory", b =>
                {
                    b.Property<Guid>("StateHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .HasColumnType("text");

                    b.Property<string>("StateSetDate")
                        .HasColumnType("text");

                    b.Property<Guid>("TopicId")
                        .HasColumnType("uuid");

                    b.HasKey("StateHistoryId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("TopicId");

                    b.ToTable("StateHistory");
                });

            modelBuilder.Entity("TrustedB.Models.StateTransition", b =>
                {
                    b.Property<int>("StateTransitionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("StateTransitionId"));

                    b.Property<Guid?>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<int?>("Statefrom")
                        .HasColumnType("integer");

                    b.Property<int?>("Stateto")
                        .HasColumnType("integer");

                    b.HasKey("StateTransitionId");

                    b.ToTable("StateTransition");

                    b.HasData(
                        new
                        {
                            StateTransitionId = 1,
                            Statefrom = 1,
                            Stateto = 2
                        },
                        new
                        {
                            StateTransitionId = 2,
                            Statefrom = 2,
                            Stateto = 3
                        },
                        new
                        {
                            StateTransitionId = 3,
                            Statefrom = 3,
                            Stateto = 4
                        },
                        new
                        {
                            StateTransitionId = 4,
                            Statefrom = 4,
                            Stateto = 5
                        },
                        new
                        {
                            StateTransitionId = 5,
                            Statefrom = 4,
                            Stateto = 1
                        },
                        new
                        {
                            StateTransitionId = 6,
                            Statefrom = 3,
                            Stateto = 1
                        },
                        new
                        {
                            StateTransitionId = 7,
                            Statefrom = 2,
                            Stateto = 1
                        });
                });

            modelBuilder.Entity("TrustedB.Models.Topics", b =>
                {
                    b.Property<Guid>("TopicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Active")
                        .HasColumnType("text");

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("text");

                    b.Property<int?>("CategoryID")
                        .HasColumnType("integer");

                    b.Property<string>("CreationDate")
                        .HasColumnType("text");

                    b.Property<string>("MainFile")
                        .HasColumnType("text");

                    b.Property<string>("Titel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TopicDiscription")
                        .HasColumnType("text");

                    b.Property<int?>("stateId")
                        .HasColumnType("integer");

                    b.HasKey("TopicId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("CategoryID");

                    b.HasIndex("stateId");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("TrustedB.Models.TopicsStates", b =>
                {
                    b.Property<int>("stateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("stateId"));

                    b.Property<string>("ArabicName")
                        .HasColumnType("text");

                    b.Property<string>("EnglishName")
                        .HasColumnType("text");

                    b.HasKey("stateId");

                    b.ToTable("TopicsStates");

                    b.HasData(
                        new
                        {
                            stateId = 1,
                            ArabicName = "قيد اعداد",
                            EnglishName = "Current"
                        },
                        new
                        {
                            stateId = 2,
                            ArabicName = "اعتماد",
                            EnglishName = "Approved"
                        },
                        new
                        {
                            stateId = 3,
                            ArabicName = "تصحيح لغوي",
                            EnglishName = "Check Lang"
                        },
                        new
                        {
                            stateId = 4,
                            ArabicName = "اعتماد تصميم",
                            EnglishName = "Approve Desgin"
                        },
                        new
                        {
                            stateId = 5,
                            ArabicName = "نشر",
                            EnglishName = "Publish"
                        });
                });

            modelBuilder.Entity("TrustedB.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Note")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.Property<string>("TariqaRole")
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue("ApplicationUser");
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
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
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

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrustedB.Models.Attachments", b =>
                {
                    b.HasOne("TrustedB.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("TrustedB.Models.Topics", "Topic")
                        .WithMany()
                        .HasForeignKey("TopicId");

                    b.HasOne("TrustedB.Models.TopicsStates", "TopicsStates")
                        .WithMany()
                        .HasForeignKey("stateId");

                    b.Navigation("ApplicationUser");

                    b.Navigation("Topic");

                    b.Navigation("TopicsStates");
                });

            modelBuilder.Entity("TrustedB.Models.CommentHistory", b =>
                {
                    b.HasOne("TrustedB.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("TrustedB.Models.Topics", "Topic")
                        .WithMany()
                        .HasForeignKey("TopicId");

                    b.HasOne("TrustedB.Models.TopicsStates", "TopicsStates")
                        .WithMany()
                        .HasForeignKey("stateId");

                    b.Navigation("ApplicationUser");

                    b.Navigation("Topic");

                    b.Navigation("TopicsStates");
                });

            modelBuilder.Entity("TrustedB.Models.StateHistory", b =>
                {
                    b.HasOne("TrustedB.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("TrustedB.Models.Topics", "Topics")
                        .WithMany()
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("Topics");
                });

            modelBuilder.Entity("TrustedB.Models.Topics", b =>
                {
                    b.HasOne("TrustedB.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("TrustedB.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID");

                    b.HasOne("TrustedB.Models.TopicsStates", "TopicsStates")
                        .WithMany()
                        .HasForeignKey("stateId");

                    b.Navigation("ApplicationUser");

                    b.Navigation("Category");

                    b.Navigation("TopicsStates");
                });
#pragma warning restore 612, 618
        }
    }
}
