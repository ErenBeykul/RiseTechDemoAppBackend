﻿// <auto-generated />
using System;
using ContactService.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ContactService.DataAccess.Migrations
{
    [DbContext(typeof(RiseTechDemoAppContactContext))]
    [Migration("20221212132042_CreateRiseTechDemoAppContact")]
    partial class CreateRiseTechDemoAppContact
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ContactService.Domain.DBModels.ContactInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("create_date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("email");

                    b.Property<string>("Info")
                        .IsRequired()
                        .HasColumnType("varchar(1000)")
                        .HasColumnName("info");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("is_deleted");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("location");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uuid")
                        .HasColumnName("person_id");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("phone");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("Update_date");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("contact_info", (string)null);
                });

            modelBuilder.Entity("ContactService.Domain.DBModels.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("create_date");

                    b.Property<string>("Firm")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("firm");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("name");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("surname");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("update_date");

                    b.HasKey("Id");

                    b.ToTable("people", (string)null);
                });

            modelBuilder.Entity("ContactService.Domain.DBModels.ContactInfo", b =>
                {
                    b.HasOne("ContactService.Domain.DBModels.Person", "Person")
                        .WithMany("ContactInfo")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("ContactService.Domain.DBModels.Person", b =>
                {
                    b.Navigation("ContactInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
