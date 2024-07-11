﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Event.Data.Models;

public partial class Net1704_221_3_EventContext : DbContext
{
    public Net1704_221_3_EventContext()
       
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Events> Events { get; set; }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }


    public static string GetConnectionString(string connectionStringName)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = config.GetConnectionString(connectionStringName);
        return connectionString;
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=Net1704_221_3_Event;Persist Security Info=True;User ID=sa;Password=12345;Encrypt=False");
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}