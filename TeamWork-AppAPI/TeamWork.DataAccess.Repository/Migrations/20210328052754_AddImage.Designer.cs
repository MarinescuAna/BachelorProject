﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeamWork.DataAccess.Repository;

namespace TeamWork.DataAccess.Repository.Migrations
{
    [DbContext(typeof(TeamWorkDbContext))]
    [Migration("20210328052754_AddImage")]
    partial class AddImage
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Domain.Assigment", b =>
                {
                    b.Property<Guid>("AssigmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ChecklistDeadline")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxGroup")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AssigmentID");

                    b.HasIndex("UserID");

                    b.ToTable("Assigments");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Domain.AssigmentList", b =>
                {
                    b.Property<Guid>("AssigmentListUniqueID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DomainName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GroupUniqueID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("GroupUniqueID1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TeacherUserEmailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("AssigmentListUniqueID");

                    b.HasIndex("GroupUniqueID1");

                    b.HasIndex("TeacherUserEmailId");

                    b.ToTable("AssigmentList");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Domain.AssigmentMember", b =>
                {
                    b.Property<Guid>("AssigmentMemberID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AssigmentID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AssigmentListUniqueID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AssigmentListUniqueID1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SolutionLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<float>("TeacherGrade")
                        .HasColumnType("real");

                    b.HasKey("AssigmentMemberID");

                    b.HasIndex("AssigmentID");

                    b.HasIndex("AssigmentListUniqueID1");

                    b.ToTable("AssigmentMembers");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Domain.Chat", b =>
                {
                    b.Property<Guid>("ChatID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GroupUniqueID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("GroupUniqueID1")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ChatID");

                    b.HasIndex("GroupUniqueID1");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Domain.CheckList", b =>
                {
                    b.Property<Guid>("CheckListID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CheckListID");

                    b.HasIndex("UserId");

                    b.ToTable("CheckLists");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Domain.CollegueGrade", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AssigmentID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Grade")
                        .HasColumnType("real");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("AssigmentID");

                    b.HasIndex("UserId");

                    b.ToTable("CollegueGrades");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Domain.Group", b =>
                {
                    b.Property<Guid>("GroupUniqueID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GroupName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GroupUniqueID");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Domain.GroupMember", b =>
                {
                    b.Property<Guid>("GroupMemberID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GroupID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("StatusRequest")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("GroupMemberID");

                    b.HasIndex("GroupID");

                    b.HasIndex("UserID");

                    b.ToTable("GroupMembers");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Domain.Image", b =>
                {
                    b.Property<Guid>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageExtention")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImageId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Domain.Item", b =>
                {
                    b.Property<Guid>("ItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CheckListID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ItemID");

                    b.HasIndex("CheckListID");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Domain.Message", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChatID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateSent")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("ChatID");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Domain.User", b =>
                {
                    b.Property<string>("UserEmailId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccessToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("AccessTokenExpiration")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ImageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Institution")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RefreshTokenExpiration")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.HasKey("UserEmailId");

                    b.HasIndex("ImageId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Domain.Assigment", b =>
                {
                    b.HasOne("TeamWork.DataAccess.Domain.Models.Domain.User", "Teacher")
                        .WithMany("Assigments")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Domain.AssigmentList", b =>
                {
                    b.HasOne("TeamWork.DataAccess.Domain.Models.Domain.Group", "Group")
                        .WithMany("AssigmentLists")
                        .HasForeignKey("GroupUniqueID1");

                    b.HasOne("TeamWork.DataAccess.Domain.Models.Domain.User", "Teacher")
                        .WithMany("AssigmentLists")
                        .HasForeignKey("TeacherUserEmailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Domain.AssigmentMember", b =>
                {
                    b.HasOne("TeamWork.DataAccess.Domain.Models.Domain.Assigment", "Assigment")
                        .WithMany("AssigmentMembers")
                        .HasForeignKey("AssigmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeamWork.DataAccess.Domain.Models.Domain.AssigmentList", "AssigmentList")
                        .WithMany()
                        .HasForeignKey("AssigmentListUniqueID1");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Domain.Chat", b =>
                {
                    b.HasOne("TeamWork.DataAccess.Domain.Models.Domain.Group", "Group")
                        .WithMany("Chats")
                        .HasForeignKey("GroupUniqueID1");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Domain.CheckList", b =>
                {
                    b.HasOne("TeamWork.DataAccess.Domain.Models.Domain.User", "User")
                        .WithMany("CheckLists")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Domain.CollegueGrade", b =>
                {
                    b.HasOne("TeamWork.DataAccess.Domain.Models.Domain.Assigment", "Assigment")
                        .WithMany("CollegueGrades")
                        .HasForeignKey("AssigmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeamWork.DataAccess.Domain.Models.Domain.User", "User")
                        .WithMany("CollegueGrades")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Domain.GroupMember", b =>
                {
                    b.HasOne("TeamWork.DataAccess.Domain.Models.Domain.Group", "Group")
                        .WithMany("GroupMembers")
                        .HasForeignKey("GroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeamWork.DataAccess.Domain.Models.Domain.User", "User")
                        .WithMany("GroupMembers")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Domain.Item", b =>
                {
                    b.HasOne("TeamWork.DataAccess.Domain.Models.Domain.CheckList", "CheckList")
                        .WithMany("Items")
                        .HasForeignKey("CheckListID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Domain.Message", b =>
                {
                    b.HasOne("TeamWork.DataAccess.Domain.Models.Domain.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeamWork.DataAccess.Domain.Models.Domain.User", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Domain.User", b =>
                {
                    b.HasOne("TeamWork.DataAccess.Domain.Models.Domain.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
