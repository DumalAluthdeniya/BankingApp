﻿using System.ComponentModel.DataAnnotations;

namespace BankingApp.Models
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }

    }
}