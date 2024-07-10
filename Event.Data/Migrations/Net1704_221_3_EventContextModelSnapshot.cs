﻿// <auto-generated />
using System;
using Event.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Event.Data.Migrations
{
    [DbContext(typeof(Net1704_221_3_EventContext))]
    partial class Net1704_221_3_EventContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Event.Data.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Event.Data.Models.EventType", b =>
                {
                    b.Property<int>("EventTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EventTypeId"));

                    b.Property<string>("EventTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EventTypeId");

                    b.ToTable("EventTypes");
                });

            modelBuilder.Entity("Event.Data.Models.Events", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EventId"));

                    b.Property<double>("ActivedTime")
                        .HasColumnType("float");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EventDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EventName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EventTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NumberTickets")
                        .HasColumnType("int");

                    b.Property<string>("Sponsor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.Property<string>("SubjectCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("EventId");

                    b.HasIndex("EventTypeId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Event.Data.Models.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"));

                    b.Property<decimal?>("AmountPaid")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RegistrationId")
                        .HasColumnType("int");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.Property<int?>("TicketId")
                        .HasColumnType("int");

                    b.Property<int?>("TicketQuantity")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PaymentId");

                    b.HasIndex("RegistrationId");

                    b.HasIndex("TicketId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Event.Data.Models.Registration", b =>
                {
                    b.Property<int>("RegistrationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RegistrationId"));

                    b.Property<string>("AttendeeEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Checkin")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("CheckinTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int?>("EventId")
                        .HasColumnType("int");

                    b.Property<decimal?>("FeePaid")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool?>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("ParticipantName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParticipantType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RegistrationPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VistorCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RegistrationId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("EventId");

                    b.ToTable("Registrations");
                });

            modelBuilder.Entity("Event.Data.Models.Ticket", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TicketId"));

                    b.Property<int?>("AvailableQuantity")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndedTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EventId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("StartedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("TicketType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("TicketId");

                    b.HasIndex("EventId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Event.Data.Models.Events", b =>
                {
                    b.HasOne("Event.Data.Models.EventType", "EventType")
                        .WithMany("Events")
                        .HasForeignKey("EventTypeId");

                    b.Navigation("EventType");
                });

            modelBuilder.Entity("Event.Data.Models.Payment", b =>
                {
                    b.HasOne("Event.Data.Models.Registration", "Registration")
                        .WithMany("Payments")
                        .HasForeignKey("RegistrationId");

                    b.HasOne("Event.Data.Models.Ticket", "Ticket")
                        .WithMany("Payments")
                        .HasForeignKey("TicketId");

                    b.Navigation("Registration");

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("Event.Data.Models.Registration", b =>
                {
                    b.HasOne("Event.Data.Models.Customer", "Customer")
                        .WithMany("Registrations")
                        .HasForeignKey("CustomerId");

                    b.HasOne("Event.Data.Models.Events", "Event")
                        .WithMany("Registrations")
                        .HasForeignKey("EventId");

                    b.Navigation("Customer");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("Event.Data.Models.Ticket", b =>
                {
                    b.HasOne("Event.Data.Models.Events", "Event")
                        .WithMany("Tickets")
                        .HasForeignKey("EventId");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("Event.Data.Models.Customer", b =>
                {
                    b.Navigation("Registrations");
                });

            modelBuilder.Entity("Event.Data.Models.EventType", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("Event.Data.Models.Events", b =>
                {
                    b.Navigation("Registrations");

                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("Event.Data.Models.Registration", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Event.Data.Models.Ticket", b =>
                {
                    b.Navigation("Payments");
                });
#pragma warning restore 612, 618
        }
    }
}
