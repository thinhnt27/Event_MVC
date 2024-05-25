﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Event.Data.Models;

public partial class Net1704_221_3_EventContext : DbContext
{
    public Net1704_221_3_EventContext()
    {
    }

    public Net1704_221_3_EventContext(DbContextOptions<Net1704_221_3_EventContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("data source=localhost;initial catalog=Net1704_221_3_Event;user id=sa;password=12345;Integrated Security=True;TrustServerCertificate=True");
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D89C42FA7D");

            entity.HasIndex(e => e.Phone, "UQ__Customer__5C7E359EE18AC2ED").IsUnique();

            entity.HasIndex(e => e.CustomerName, "UQ__Customer__7A22C6EAAADBEFE9").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Customer__A9D10534410016DD").IsUnique();

            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Events__7944C810462064C5");

            entity.Property(e => e.EventId).ValueGeneratedNever();
            entity.Property(e => e.EventDate).HasColumnType("datetime");
            entity.Property(e => e.EventDescription).HasColumnType("text");
            entity.Property(e => e.EventName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Sponsor)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SubjectCode)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.EventType).WithMany(p => p.Events)
                .HasForeignKey(d => d.EventTypeId)
                .HasConstraintName("FK__Events__EventTyp__440B1D61");
        });

        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.EventTypeId).HasName("PK__EventTyp__A9216B3FFF1AAC9E");

            entity.Property(e => e.EventTypeName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A3840DEE2BC");

            entity.Property(e => e.PaymentId).ValueGeneratedNever();
            entity.Property(e => e.AmountPaid).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PaymentType)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Registration).WithMany(p => p.Payments)
                .HasForeignKey(d => d.RegistrationId)
                .HasConstraintName("FK__Payments__Regist__44FF419A");

            entity.HasOne(d => d.Ticket).WithMany(p => p.Payments)
                .HasForeignKey(d => d.TicketId)
                .HasConstraintName("FK__Payments__Ticket__45F365D3");
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasKey(e => e.RegistrationId).HasName("PK__Registra__6EF58810084A83B2");

            entity.Property(e => e.RegistrationId).ValueGeneratedNever();
            entity.Property(e => e.AttendeeEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CheckinTime).HasColumnType("datetime");
            entity.Property(e => e.FeePaid).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ParticipantName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ParticipantType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationPhone)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.VistorCode)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Customer).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Registrat__Custo__46E78A0C");

            entity.HasOne(d => d.Event).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__Registrat__Event__47DBAE45");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Tickets__712CC607489EFF87");

            entity.Property(e => e.TicketId).ValueGeneratedNever();
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TicketType)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Event).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__Tickets__EventId__48CFD27E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}