﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeamWork.DataAccess.Repository;

namespace TeamWork.DataAccess.Repository.Migrations
{
    [DbContext(typeof(TeamWorkDbContext))]
    partial class TeamWorkDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.AssignedTask", b =>
                {
                    b.Property<Guid>("AssignedTaskID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AssignmentID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ListID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SolutionLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("TeacherGrade")
                        .HasColumnType("real");

                    b.HasKey("AssignedTaskID");

                    b.HasIndex("AssignmentID");

                    b.HasIndex("ListID");

                    b.ToTable("AssignedTasks");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Assignment", b =>
                {
                    b.Property<Guid>("AssignmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ChecklistDeadline")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GroupsMax")
                        .HasColumnType("int");

                    b.Property<int>("GroupsTake")
                        .HasColumnType("int");

                    b.Property<Guid>("ListID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AssignmentID");

                    b.HasIndex("ListID");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Chat", b =>
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

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Check", b =>
                {
                    b.Property<Guid>("CheckID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AssignedTaskID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IsChecked")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CheckID");

                    b.HasIndex("AssignedTaskID");

                    b.HasIndex("UserId");

                    b.ToTable("Checks");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.CollegueGrade", b =>
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

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Group", b =>
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

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.GroupMember", b =>
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

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Image", b =>
                {
                    b.Property<Guid>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageExtention")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ImageId");

                    b.HasIndex("UserId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.List", b =>
                {
                    b.Property<Guid>("ListID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Domain")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GroupID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ListDeadline")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ListID");

                    b.ToTable("List");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Message", b =>
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

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.User", b =>
                {
                    b.Property<string>("UserEmailId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccessToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("AccessTokenExpiration")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

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

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.AssignedTask", b =>
                {
                    b.HasOne("TeamWork.DataAccess.Domain.Models.Assignment", "Assignment")
                        .WithMany("AssignedTasks")
                        .HasForeignKey("AssignmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeamWork.DataAccess.Domain.Models.List", "List")
                        .WithMany()
                        .HasForeignKey("ListID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Assignment", b =>
                {
                    b.HasOne("TeamWork.DataAccess.Domain.Models.List", "List")
                        .WithMany("Assignments")
                        .HasForeignKey("ListID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Chat", b =>
                {
                    b.HasOne("TeamWork.DataAccess.Domain.Models.Group", "Group")
                        .WithMany("Chats")
                        .HasForeignKey("GroupUniqueID1");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Check", b =>
                {
                    b.HasOne("TeamWork.DataAccess.Domain.Models.AssignedTask", "AssignedTask")
                        .WithMany("Checks")
                        .HasForeignKey("AssignedTaskID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeamWork.DataAccess.Domain.Models.User", "User")
                        .WithMany("Checks")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.CollegueGrade", b =>
                {
                    b.HasOne("TeamWork.DataAccess.Domain.Models.Assignment", "Assigment")
                        .WithMany("CollegueGrades")
                        .HasForeignKey("AssigmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeamWork.DataAccess.Domain.Models.User", "User")
                        .WithMany("CollegueGrades")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.GroupMember", b =>
                {
                    b.HasOne("TeamWork.DataAccess.Domain.Models.Group", "Group")
                        .WithMany("GroupMembers")
                        .HasForeignKey("GroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeamWork.DataAccess.Domain.Models.User", "User")
                        .WithMany("GroupMembers")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Image", b =>
                {
                    b.HasOne("TeamWork.DataAccess.Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("TeamWork.DataAccess.Domain.Models.Message", b =>
                {
                    b.HasOne("TeamWork.DataAccess.Domain.Models.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeamWork.DataAccess.Domain.Models.User", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
