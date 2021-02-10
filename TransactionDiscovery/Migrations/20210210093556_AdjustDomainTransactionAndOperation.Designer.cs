﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TransactionDiscovery.Infrastructure.Persistence;

namespace TransactionDiscovery.Infrastructure.Migrations
{
    [DbContext(typeof(TdsDbContext))]
    [Migration("20210210093556_AdjustDomainTransactionAndOperation")]
    partial class AdjustDomainTransactionAndOperation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TransactionDiscovery.Core.Domain.Account", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("SequenceNumber")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("TransactionDiscovery.Core.Domain.Operation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId");

                    b.ToTable("Operations");
                });

            modelBuilder.Entity("TransactionDiscovery.Core.Domain.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Hash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SourceAccountId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("SourceAccountId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("TransactionDiscovery.Core.Domain.Operation", b =>
                {
                    b.HasOne("TransactionDiscovery.Core.Domain.Transaction", null)
                        .WithMany("Operations")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TransactionDiscovery.Core.Domain.Transaction", b =>
                {
                    b.HasOne("TransactionDiscovery.Core.Domain.Account", null)
                        .WithMany("Transactions")
                        .HasForeignKey("SourceAccountId");
                });

            modelBuilder.Entity("TransactionDiscovery.Core.Domain.Account", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("TransactionDiscovery.Core.Domain.Transaction", b =>
                {
                    b.Navigation("Operations");
                });
#pragma warning restore 612, 618
        }
    }
}
