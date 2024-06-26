﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using XHS_Pro.Data;

#nullable disable

namespace XHS_Pro.Migrations
{
    [DbContext(typeof(XHS_ProContext))]
    [Migration("20240410093442_AddGoodsCommentid")]
    partial class AddGoodsCommentid
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("XHS_Pro.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("created")
                        .HasColumnType("datetime2");

                    b.Property<int>("deleted")
                        .HasColumnType("int");

                    b.Property<int>("goodsid")
                        .HasColumnType("int");

                    b.Property<string>("headphoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("noteid")
                        .HasColumnType("int");

                    b.Property<DateTime>("updated")
                        .HasColumnType("datetime2");

                    b.Property<int>("userid")
                        .HasColumnType("int");

                    b.Property<string>("username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("XHS_Pro.Models.Goods", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("created")
                        .HasColumnType("datetime2");

                    b.Property<int>("delete")
                        .HasColumnType("int");

                    b.Property<string>("goodsContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("goodsName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("goodsTag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.Property<DateTime>("updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Goods");
                });

            modelBuilder.Entity("XHS_Pro.Models.Note", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("PictureUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("collectionnum")
                        .HasColumnType("int");

                    b.Property<string>("content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("created")
                        .HasColumnType("datetime2");

                    b.Property<int>("deleted")
                        .HasColumnType("int");

                    b.Property<int>("praisenum")
                        .HasColumnType("int");

                    b.Property<string>("surfacePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("updated")
                        .HasColumnType("datetime2");

                    b.Property<int>("userid")
                        .HasColumnType("int");

                    b.Property<string>("username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("videourl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Note");
                });

            modelBuilder.Entity("XHS_Pro.Models.Start", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("enable")
                        .HasColumnType("int");

                    b.Property<int>("noteid")
                        .HasColumnType("int");

                    b.Property<int>("userid")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Start");
                });

            modelBuilder.Entity("XHS_Pro.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("age")
                        .HasColumnType("int");

                    b.Property<DateTime>("created")
                        .HasColumnType("datetime2");

                    b.Property<int>("gender")
                        .HasColumnType("int");

                    b.Property<string>("headphoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("introduction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("updated")
                        .HasColumnType("datetime2");

                    b.Property<string>("username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("usertype")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("XHS_Pro.Models.Zan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("enable")
                        .HasColumnType("int");

                    b.Property<int>("noteid")
                        .HasColumnType("int");

                    b.Property<int>("userid")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Zan");
                });
#pragma warning restore 612, 618
        }
    }
}
