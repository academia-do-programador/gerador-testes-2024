﻿// <auto-generated />
using GeradorTestes.ConsoleApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GeradorTestes.ConsoleApp.Migrations
{
    [DbContext(typeof(GeradorTesteDbContext))]
    [Migration("20240703163411_Add-Materias")]
    partial class AddMaterias
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GeradorTestes.Dominio.ModuloDisciplina.Disciplina", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.ToTable("TBDisciplina", (string)null);
                });

            modelBuilder.Entity("GeradorTestes.Dominio.ModuloMateria.Materia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Disciplina_Id")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<int>("Serie")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Disciplina_Id");

                    b.ToTable("TBMateria", (string)null);
                });

            modelBuilder.Entity("GeradorTestes.Dominio.ModuloMateria.Materia", b =>
                {
                    b.HasOne("GeradorTestes.Dominio.ModuloDisciplina.Disciplina", "Disciplina")
                        .WithMany("Materias")
                        .HasForeignKey("Disciplina_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_TBMateria_TBDisciplina");

                    b.Navigation("Disciplina");
                });

            modelBuilder.Entity("GeradorTestes.Dominio.ModuloDisciplina.Disciplina", b =>
                {
                    b.Navigation("Materias");
                });
#pragma warning restore 612, 618
        }
    }
}