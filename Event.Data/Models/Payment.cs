﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event.Data.Models;

public partial class Payment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PaymentId { get; set; }
    [Required]
    public int? RegistrationId { get; set; }
    [Required]
    public int? TicketId { get; set; }
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "It is not invalide number.")]
    public int? TicketQuantity { get; set; }
    [Required]
    public DateTime? PaymentDate { get; set; }
    [Required]
    [Range(1,double.MaxValue,ErrorMessage ="It is not invalide number.")]
    public decimal? AmountPaid { get; set; }
    [Required]
    public bool? Status { get; set; }
    [Required]
    public string PaymentType { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }

    public virtual Registration Registration { get; set; }

    public virtual Ticket Ticket { get; set; }
}