﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sharpcode.ServiceRadar.Persistence;

#nullable disable

namespace Sharpcode.ServiceRadar.Persistence.Migrations
{
    [DbContext(typeof(BrokerDbContext))]
    partial class BrokerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Sharpcode.ServiceRadar.Model.Entities.BusinessApplication", b =>
                {
                    b.Property<int>("ApplicationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ApplicationId"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ApplicationId");

                    b.ToTable("Application", (string)null);

                    b.HasData(
                        new
                        {
                            ApplicationId = 1,
                            CreatedAt = new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            DeletedAt = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Description = "Demo Application after initial migration",
                            Title = "Demo Application",
                            UpdatedAt = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Version = "0.0.1"
                        });
                });

            modelBuilder.Entity("Sharpcode.ServiceRadar.Model.Entities.BusinessIssue", b =>
                {
                    b.Property<int>("BusinessIssueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BusinessIssueId"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BusinessIssuePriority")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("ClosedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<TimeSpan>("ImpactDuration")
                        .HasColumnType("time");

                    b.Property<int>("IssueType")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("IssuedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("IssuerId")
                        .HasColumnType("int");

                    b.Property<int>("OrganisationId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BusinessIssueId");

                    b.HasIndex("IssuerId");

                    b.HasIndex("OrganisationId");

                    b.ToTable("BusinessIssue", (string)null);

                    b.HasData(
                        new
                        {
                            BusinessIssueId = 1,
                            Body = "This is a demo issue after initial migration",
                            BusinessIssuePriority = 2,
                            ImpactDuration = new TimeSpan(0, 1, 1, 1, 0),
                            IssueType = 0,
                            IssuedAt = new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            IssuerId = 1,
                            OrganisationId = 1,
                            Title = "Demo issue"
                        });
                });

            modelBuilder.Entity("Sharpcode.ServiceRadar.Model.Entities.BusinessIssue2Application", b =>
                {
                    b.Property<int>("BusinessIssue2ApplicationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BusinessIssue2ApplicationId"));

                    b.Property<int>("BusinessApplicationId")
                        .HasColumnType("int");

                    b.Property<int>("BusinessIssueId")
                        .HasColumnType("int");

                    b.HasKey("BusinessIssue2ApplicationId");

                    b.HasIndex("BusinessApplicationId");

                    b.HasIndex("BusinessIssueId");

                    b.ToTable("BusinessIssue2Application");

                    b.HasData(
                        new
                        {
                            BusinessIssue2ApplicationId = 1,
                            BusinessApplicationId = 1,
                            BusinessIssueId = 1
                        });
                });

            modelBuilder.Entity("Sharpcode.ServiceRadar.Model.Entities.Issuer", b =>
                {
                    b.Property<int>("IssuerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IssuerId"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("IssuerMail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IssuerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IssuerId");

                    b.ToTable("Issuer", (string)null);

                    b.HasData(
                        new
                        {
                            IssuerId = 1,
                            CreatedAt = new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            IssuerMail = "demo@serviceradar.net",
                            IssuerName = "Demo Issuer"
                        });
                });

            modelBuilder.Entity("Sharpcode.ServiceRadar.Model.Entities.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MessageId"));

                    b.Property<int>("BusinessIssueId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("MessageBody")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MessageId");

                    b.HasIndex("BusinessIssueId");

                    b.ToTable("Message", (string)null);

                    b.HasData(
                        new
                        {
                            MessageId = 1,
                            BusinessIssueId = 1,
                            CreatedAt = new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            MessageBody = "First demo message.",
                            Title = "Demo Message 1"
                        });
                });

            modelBuilder.Entity("Sharpcode.ServiceRadar.Model.Entities.Organisation", b =>
                {
                    b.Property<int>("OrganisationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrganisationId"));

                    b.Property<string>("Desription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrganisationId");

                    b.ToTable("Organisation", (string)null);

                    b.HasData(
                        new
                        {
                            OrganisationId = 1,
                            Desription = "Default organisation after initial migration",
                            Title = "Default"
                        });
                });

            modelBuilder.Entity("Sharpcode.ServiceRadar.Model.Entities.RemoteClient", b =>
                {
                    b.Property<int>("RemoteClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RemoteClientId"));

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrganisationId")
                        .HasColumnType("int");

                    b.Property<string>("RemoteClientKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RemoteClientId");

                    b.HasIndex("OrganisationId");

                    b.ToTable("RemoteClient", (string)null);
                });

            modelBuilder.Entity("Sharpcode.ServiceRadar.Model.Entities.BusinessIssue", b =>
                {
                    b.HasOne("Sharpcode.ServiceRadar.Model.Entities.Issuer", "Issuer")
                        .WithMany()
                        .HasForeignKey("IssuerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sharpcode.ServiceRadar.Model.Entities.Organisation", "Organisation")
                        .WithMany("BusinessIssues")
                        .HasForeignKey("OrganisationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Issuer");

                    b.Navigation("Organisation");
                });

            modelBuilder.Entity("Sharpcode.ServiceRadar.Model.Entities.BusinessIssue2Application", b =>
                {
                    b.HasOne("Sharpcode.ServiceRadar.Model.Entities.BusinessApplication", "BusinessApplication")
                        .WithMany("BusinessIssues")
                        .HasForeignKey("BusinessApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sharpcode.ServiceRadar.Model.Entities.BusinessIssue", "BusinessIssue")
                        .WithMany("BusinessApplications")
                        .HasForeignKey("BusinessIssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BusinessApplication");

                    b.Navigation("BusinessIssue");
                });

            modelBuilder.Entity("Sharpcode.ServiceRadar.Model.Entities.Message", b =>
                {
                    b.HasOne("Sharpcode.ServiceRadar.Model.Entities.BusinessIssue", "BusinessIssue")
                        .WithMany("Messages")
                        .HasForeignKey("BusinessIssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BusinessIssue");
                });

            modelBuilder.Entity("Sharpcode.ServiceRadar.Model.Entities.RemoteClient", b =>
                {
                    b.HasOne("Sharpcode.ServiceRadar.Model.Entities.Organisation", "Organisation")
                        .WithMany("RemoteClients")
                        .HasForeignKey("OrganisationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organisation");
                });

            modelBuilder.Entity("Sharpcode.ServiceRadar.Model.Entities.BusinessApplication", b =>
                {
                    b.Navigation("BusinessIssues");
                });

            modelBuilder.Entity("Sharpcode.ServiceRadar.Model.Entities.BusinessIssue", b =>
                {
                    b.Navigation("BusinessApplications");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Sharpcode.ServiceRadar.Model.Entities.Organisation", b =>
                {
                    b.Navigation("BusinessIssues");

                    b.Navigation("RemoteClients");
                });
#pragma warning restore 612, 618
        }
    }
}
