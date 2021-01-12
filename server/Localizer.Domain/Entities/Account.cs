using System.ComponentModel.DataAnnotations;
using Localizer.Domain.Entities.Common;

namespace Localizer.Domain.Entities
{
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