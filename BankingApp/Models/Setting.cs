﻿using System.ComponentModel.DataAnnotations;

namespace BankingApp.Models
{
	public class Setting
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string? Key { get; set; }
		public string? Value { get; set; }
	}
}