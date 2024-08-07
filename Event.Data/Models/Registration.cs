﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event.Data.Models;

public partial class Registration
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RegistrationId { get; set; }
    [Required]
    public int? EventId { get; set; }
    [Required]
    public string VistorCode { get; set; }
    [Required]
    public string ParticipantName { get; set; }
    [Required]
    public string ParticipantType { get; set; }
    [Required]
    [EmailAddress]
    public string AttendeeEmail { get; set; }
    public DateTime? RegistrationDate { get; set; }
    [Required]
    [Phone]
    public string RegistrationPhone { get; set; }
    [Required]
    public decimal? FeePaid { get; set; }
    [Required]
    public bool? Checkin { get; set; }

    public DateTime? CheckinTime { get; set; }
    [Required]
    public int? CustomerId { get; set; }
    [Required]
    public bool? Gender { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual Events Event { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}