﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TelHai.CS.ServerAPI.Models;

#nullable disable

namespace TelHai.CS.ServerAPI.Migrations
{
    [DbContext(typeof(ExamContext))]
    [Migration("20230322122224_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TelHai.CS.ServerAPI.Models.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<int?>("QuestionId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("TelHai.CS.ServerAPI.Models.Error", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ChosenAnswer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CorrectAnswer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SubmitId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubmitId");

                    b.ToTable("Errors");
                });

            modelBuilder.Entity("TelHai.CS.ServerAPI.Models.Exam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DateDay")
                        .HasColumnType("int");

                    b.Property<int?>("DateHour")
                        .HasColumnType("int");

                    b.Property<int?>("DateMinute")
                        .HasColumnType("int");

                    b.Property<int?>("DateMonth")
                        .HasColumnType("int");

                    b.Property<int?>("DateYear")
                        .HasColumnType("int");

                    b.Property<bool?>("IsOrderRandom")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeacherName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("TotalTime")
                        .HasColumnType("float");

                    b.Property<string>("_id")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("TelHai.CS.ServerAPI.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ExamId")
                        .HasColumnType("int");

                    b.Property<bool>("IsRand")
                        .HasColumnType("bit");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("TelHai.CS.ServerAPI.Models.Submit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ExamId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ExamId1")
                        .HasColumnType("int");

                    b.Property<string>("StudentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("_grade")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ExamId1");

                    b.ToTable("Submissions");
                });

            modelBuilder.Entity("TelHai.CS.ServerAPI.Models.Answer", b =>
                {
                    b.HasOne("TelHai.CS.ServerAPI.Models.Question", null)
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TelHai.CS.ServerAPI.Models.Error", b =>
                {
                    b.HasOne("TelHai.CS.ServerAPI.Models.Submit", null)
                        .WithMany("Errors")
                        .HasForeignKey("SubmitId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TelHai.CS.ServerAPI.Models.Question", b =>
                {
                    b.HasOne("TelHai.CS.ServerAPI.Models.Exam", null)
                        .WithMany("Questions")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TelHai.CS.ServerAPI.Models.Submit", b =>
                {
                    b.HasOne("TelHai.CS.ServerAPI.Models.Exam", null)
                        .WithMany("Submissions")
                        .HasForeignKey("ExamId1")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TelHai.CS.ServerAPI.Models.Exam", b =>
                {
                    b.Navigation("Questions");

                    b.Navigation("Submissions");
                });

            modelBuilder.Entity("TelHai.CS.ServerAPI.Models.Question", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("TelHai.CS.ServerAPI.Models.Submit", b =>
                {
                    b.Navigation("Errors");
                });
#pragma warning restore 612, 618
        }
    }
}