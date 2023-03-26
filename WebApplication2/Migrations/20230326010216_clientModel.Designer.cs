﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication2.Models;

#nullable disable

namespace WebApplication2.Migrations
{
    [DbContext(typeof(MyDatabaseContext))]
    [Migration("20230326010216_clientModel")]
    partial class clientModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("WebApplication2.Models.Client", b =>
                {
                    b.Property<int?>("Client_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
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

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Client_Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("WebApplication2.Models.Roles", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("WebApplication2.Models.User", b =>
                {
                    b.Property<int?>("User_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Job_Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("OnLeave")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Registration_date")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserAuth_Id")
                        .HasColumnType("INTEGER");

                    b.HasKey("User_Id");

                    b.HasIndex("UserAuth_Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebApplication2.Models.UserAuth", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConfirmPassword")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("UserAuths");
                });

            modelBuilder.Entity("WebApplication2.Models.UserRoles", b =>
                {
                    b.Property<int>("UserRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserAuthId")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserRoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserAuthId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("WebApplication2.Models.User", b =>
                {
                    b.HasOne("WebApplication2.Models.UserAuth", "UserAuth")
                        .WithMany("User")
                        .HasForeignKey("UserAuth_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("UserAuth");
                });

            modelBuilder.Entity("WebApplication2.Models.UserRoles", b =>
                {
                    b.HasOne("WebApplication2.Models.Roles", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication2.Models.UserAuth", "UserAuth")
                        .WithMany("UserRole")
                        .HasForeignKey("UserAuthId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("UserAuth");
                });

            modelBuilder.Entity("WebApplication2.Models.Roles", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("WebApplication2.Models.UserAuth", b =>
                {
                    b.Navigation("User");

                    b.Navigation("UserRole");
                });
#pragma warning restore 612, 618
        }
    }
}
