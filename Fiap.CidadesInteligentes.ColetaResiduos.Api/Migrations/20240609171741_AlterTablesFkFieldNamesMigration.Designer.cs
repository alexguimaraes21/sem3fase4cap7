﻿// <auto-generated />
using System;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240609171741_AlterTablesFkFieldNamesMigration")]
    partial class AlterTablesFkFieldNamesMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Fiap.CidadesInteligentes.ColetaResiduos.Api.Models.CollectionModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(19)")
                        .HasColumnName("ID");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("ContainerId")
                        .HasColumnType("NUMBER(19)")
                        .HasColumnName("CONTAINER_ID");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("DATE_TIME");

                    b.Property<long>("RouteId")
                        .HasColumnType("NUMBER(19)")
                        .HasColumnName("ROUTE_ID");

                    b.HasKey("Id");

                    b.HasIndex("ContainerId");

                    b.HasIndex("RouteId");

                    b.ToTable("NET_COLLECTIONS", (string)null);
                });

            modelBuilder.Entity("Fiap.CidadesInteligentes.ColetaResiduos.Api.Models.ContainerModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(19)")
                        .HasColumnName("CONTAINER_ID");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Capacity")
                        .HasColumnType("FLOAT")
                        .HasColumnName("CAPACITY");

                    b.Property<int>("CurrentLevel")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("CURRENT_LEVEL");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("LOCATION");

                    b.HasKey("Id");

                    b.ToTable("NET_CONTAINERS", (string)null);
                });

            modelBuilder.Entity("Fiap.CidadesInteligentes.ColetaResiduos.Api.Models.RouteModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(19)")
                        .HasColumnName("ROUTE_ID");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("DESCRIPTION");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("END_TIME");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("START_TIME");

                    b.Property<long>("TruckId")
                        .HasColumnType("NUMBER(19)")
                        .HasColumnName("TRUCK_ID");

                    b.HasKey("Id");

                    b.HasIndex("TruckId");

                    b.ToTable("NET_ROUTES", (string)null);
                });

            modelBuilder.Entity("Fiap.CidadesInteligentes.ColetaResiduos.Api.Models.TruckModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(19)")
                        .HasColumnName("TRUCK_ID");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<bool>("Available")
                        .HasColumnType("NUMBER(1,0)")
                        .HasColumnName("AVAILABLE");

                    b.Property<decimal>("Capacity")
                        .HasColumnType("FLOAT")
                        .HasColumnName("CAPACITY");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasColumnType("CHAR(7)")
                        .HasColumnName("LICENSE_PLATE");

                    b.HasKey("Id");

                    b.ToTable("NET_TRUCKS", (string)null);
                });

            modelBuilder.Entity("Fiap.CidadesInteligentes.ColetaResiduos.Api.Models.UserModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(19)")
                        .HasColumnName("USER_ID");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATE")
                        .HasColumnName("CREATED_AT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("EMAIL");

                    b.Property<bool>("IsActive")
                        .HasColumnType("NUMBER(1,0)")
                        .HasColumnName("IS_ACTIVE");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("PASSWORD");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("ROLE");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("NET_USERS", (string)null);
                });

            modelBuilder.Entity("Fiap.CidadesInteligentes.ColetaResiduos.Api.Models.CollectionModel", b =>
                {
                    b.HasOne("Fiap.CidadesInteligentes.ColetaResiduos.Api.Models.ContainerModel", "Container")
                        .WithMany()
                        .HasForeignKey("ContainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fiap.CidadesInteligentes.ColetaResiduos.Api.Models.RouteModel", "Route")
                        .WithMany()
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Container");

                    b.Navigation("Route");
                });

            modelBuilder.Entity("Fiap.CidadesInteligentes.ColetaResiduos.Api.Models.RouteModel", b =>
                {
                    b.HasOne("Fiap.CidadesInteligentes.ColetaResiduos.Api.Models.TruckModel", "Truck")
                        .WithMany()
                        .HasForeignKey("TruckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Truck");
                });
#pragma warning restore 612, 618
        }
    }
}
