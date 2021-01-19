using System.ComponentModel.DataAnnotations;
using Localizer.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Localizer.Domain.Entities
{
	[Index(nameof(Email))]
	public class Account : EntityBase
	{
		[Required]
		public string Email { get; set; }

		[Required]
		public string PasswordHash { get; set; }

		[Required]
		public string Name { get; set; }
	}
}