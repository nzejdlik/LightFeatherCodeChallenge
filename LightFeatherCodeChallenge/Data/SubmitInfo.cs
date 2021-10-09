using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LightFeatherCodeChallenge.Data
{
	public class SubmitInfo
	{
		public string? Email { get; set; }

		[Required]
		public string? FirstName { get; set; }

		[Required]
		public string? LastName { get; set; }

		public string? PhoneNumber { get; set; }

		[Required]
		public string? Supervisor { get; set; }
	}
}
