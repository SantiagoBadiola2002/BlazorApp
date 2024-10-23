﻿// <auto-generated />
using System;
using BlazorApp.Models.BaseDeDatos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlazorApp.Migrations
{
    [DbContext(typeof(ContextoBD))]
    partial class ContextoBDModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BlazorApp.Models.Administrador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Administradores");
                });

            modelBuilder.Entity("BlazorApp.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cedula")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("varchar(12)");

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("OficinaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OficinaId");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("BlazorApp.Models.GerenteCalidad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.ToTable("GerentesCalidad");
                });

            modelBuilder.Entity("BlazorApp.Models.Oficina", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Oficinas");
                });

            modelBuilder.Entity("BlazorApp.Models.Operario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Contraseña")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("EstaDisponible")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<int>("OficinaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OficinaId");

                    b.ToTable("Operarios");
                });

            modelBuilder.Entity("BlazorApp.Models.RegistroDeAtencion", b =>
                {
                    b.Property<int>("RegistroDeAtencionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<int>("DuracionAtencion")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("OficinaId")
                        .HasColumnType("int");

                    b.Property<int>("OperarioId")
                        .HasColumnType("int");

                    b.HasKey("RegistroDeAtencionId");

                    b.HasIndex("ClienteId");

                    b.HasIndex("OficinaId");

                    b.HasIndex("OperarioId");

                    b.ToTable("RegistrosDeAtencion");
                });

            modelBuilder.Entity("BlazorApp.Models.Cliente", b =>
                {
                    b.HasOne("BlazorApp.Models.Oficina", null)
                        .WithMany("Clientes")
                        .HasForeignKey("OficinaId");
                });

            modelBuilder.Entity("BlazorApp.Models.Operario", b =>
                {
                    b.HasOne("BlazorApp.Models.Oficina", null)
                        .WithMany("Operarios")
                        .HasForeignKey("OficinaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BlazorApp.Models.RegistroDeAtencion", b =>
                {
                    b.HasOne("BlazorApp.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlazorApp.Models.Oficina", "Oficina")
                        .WithMany()
                        .HasForeignKey("OficinaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlazorApp.Models.Operario", "Operario")
                        .WithMany()
                        .HasForeignKey("OperarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Oficina");

                    b.Navigation("Operario");
                });

            modelBuilder.Entity("BlazorApp.Models.Oficina", b =>
                {
                    b.Navigation("Clientes");

                    b.Navigation("Operarios");
                });
#pragma warning restore 612, 618
        }
    }
}
