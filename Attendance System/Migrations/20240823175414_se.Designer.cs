﻿// <auto-generated />
using System;
using Attendance_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Attendance_System.Migrations
{
    [DbContext(typeof(ItiDbContext))]
    [Migration("20240823175414_se")]
    partial class se
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Attendance_System.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Attendance_System.Models.Course", b =>
                {
                    b.Property<int>("CrsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CrsId"));

                    b.Property<string>("CrsName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.HasKey("CrsId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Attendance_System.Models.Department", b =>
                {
                    b.Property<int>("DeptId")
                        .HasColumnType("int");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("DeptName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("status")
                        .HasColumnType("bit");

                    b.HasKey("DeptId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Attendance_System.Models.Hr", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Hrs");
                });

            modelBuilder.Entity("Attendance_System.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Attendance_System.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<int>("Dnum")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Hnum")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Verified")
                        .HasColumnType("bit");

                    b.Property<string>("img")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Dnum");

                    b.HasIndex("Hnum");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Attendance_System.Models.StudentAttendece", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<bool>("Friday")
                        .HasColumnType("bit");

                    b.Property<bool>("Monday")
                        .HasColumnType("bit");

                    b.Property<bool>("Saturday")
                        .HasColumnType("bit");

                    b.Property<bool>("Sunday")
                        .HasColumnType("bit");

                    b.Property<bool>("Thursday")
                        .HasColumnType("bit");

                    b.Property<bool>("Tuesday")
                        .HasColumnType("bit");

                    b.Property<bool>("Wednesday")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("StudentAttendeces");
                });

            modelBuilder.Entity("Attendance_System.Models.StudentCourse", b =>
                {
                    b.Property<int>("crsid")
                        .HasColumnType("int");

                    b.Property<int>("stId")
                        .HasColumnType("int");

                    b.Property<int?>("degree")
                        .HasColumnType("int");

                    b.HasKey("crsid", "stId");

                    b.HasIndex("stId");

                    b.ToTable("StudentCourse");
                });

            modelBuilder.Entity("Attendance_System.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Attendance_System.Models.UserRole", b =>
                {
                    b.Property<int>("UId")
                        .HasColumnType("int");

                    b.Property<int>("RId")
                        .HasColumnType("int");

                    b.HasKey("UId", "RId");

                    b.HasIndex("RId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("CourseDepartment", b =>
                {
                    b.Property<int>("CourseCrsId")
                        .HasColumnType("int");

                    b.Property<int>("DepartmentDeptId")
                        .HasColumnType("int");

                    b.HasKey("CourseCrsId", "DepartmentDeptId");

                    b.HasIndex("DepartmentDeptId");

                    b.ToTable("CourseDepartment");
                });

            modelBuilder.Entity("Attendance_System.Models.Student", b =>
                {
                    b.HasOne("Attendance_System.Models.Department", "Department")
                        .WithMany("student")
                        .HasForeignKey("Dnum")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Attendance_System.Models.Hr", "Hr")
                        .WithMany("Student")
                        .HasForeignKey("Hnum")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Hr");
                });

            modelBuilder.Entity("Attendance_System.Models.StudentAttendece", b =>
                {
                    b.HasOne("Attendance_System.Models.Student", "student")
                        .WithOne("StudentAttendece")
                        .HasForeignKey("Attendance_System.Models.StudentAttendece", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("student");
                });

            modelBuilder.Entity("Attendance_System.Models.StudentCourse", b =>
                {
                    b.HasOne("Attendance_System.Models.Course", "course")
                        .WithMany("StudentCourse")
                        .HasForeignKey("crsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Attendance_System.Models.Student", "Student")
                        .WithMany("StudentProgram")
                        .HasForeignKey("stId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("course");
                });

            modelBuilder.Entity("Attendance_System.Models.UserRole", b =>
                {
                    b.HasOne("Attendance_System.Models.Role", "Role")
                        .WithMany("UserRole")
                        .HasForeignKey("RId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Attendance_System.Models.User", "user")
                        .WithMany("UserRole")
                        .HasForeignKey("UId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("user");
                });

            modelBuilder.Entity("CourseDepartment", b =>
                {
                    b.HasOne("Attendance_System.Models.Course", null)
                        .WithMany()
                        .HasForeignKey("CourseCrsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Attendance_System.Models.Department", null)
                        .WithMany()
                        .HasForeignKey("DepartmentDeptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Attendance_System.Models.Course", b =>
                {
                    b.Navigation("StudentCourse");
                });

            modelBuilder.Entity("Attendance_System.Models.Department", b =>
                {
                    b.Navigation("student");
                });

            modelBuilder.Entity("Attendance_System.Models.Hr", b =>
                {
                    b.Navigation("Student");
                });

            modelBuilder.Entity("Attendance_System.Models.Role", b =>
                {
                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("Attendance_System.Models.Student", b =>
                {
                    b.Navigation("StudentAttendece");

                    b.Navigation("StudentProgram");
                });

            modelBuilder.Entity("Attendance_System.Models.User", b =>
                {
                    b.Navigation("UserRole");
                });
#pragma warning restore 612, 618
        }
    }
}
