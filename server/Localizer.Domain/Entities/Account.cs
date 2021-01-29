using System.ComponentModel.DataAnnotations;
using Localizer.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Localizer.Domain.Entities
{
	[Index(nameof(Email))]
	public class Account : EntityBase
	{
		/// <summary>
		///		Email address
		/// </summary>
		[Required]
		public string Email { get; set; }

		/// <summary>
		///		Password hash
		/// </summary>
		[Required]
		public string PasswordHash { get; set; }

		/// <summary>
		///		Display name
		/// </summary>
		[Required]
		public string Name { get; set; }

		/// <summary>
		///		Determine account email confirmed.
		/// </summary>
		[Required]
		public bool EmailConfirmed { get; set; }
	}
}