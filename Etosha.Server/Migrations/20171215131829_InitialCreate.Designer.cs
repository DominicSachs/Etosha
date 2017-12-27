﻿// <auto-generated />
using Etosha.Server.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Etosha.Server.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20171215131829_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Etosha.Server.Entities.Advise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreationDate");

                    b.Property<Guid>("CustomerId");

                    b.Property<string>("Description");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Advises");
                });

            modelBuilder.Entity("Etosha.Server.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Etosha.Server.Entities.Advise", b =>
                {
                    b.HasOne("Etosha.Server.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
