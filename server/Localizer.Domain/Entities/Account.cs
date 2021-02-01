using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Localizer.Common;
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
		[NotMapped]
		public bool EmailConfirmed => string.IsNullOrEmpty(EmailVerificationCode);

		/// <summary>
		///		Verification code for email confirmation.	
		/// </summary>
		[Required]
		[MaxLength(KeyLength.EmailVerificationCode)]
		public string EmailVerificationCode { get; set; } = string.Empty;

		/// <summary>
		///		Reset code for changing password.
		/// </summary>
		[Required]
		[MaxLength(KeyLength.PasswordResetCode)]
		public string PasswordResetCode { get; set; } = string.Empty;

		/// <summary>
		///		Access token issuing histories.
		/// </summary>
		public virtual ICollection<AccountAccessTokenHistory> AccessTokenHistories { get; set; }
	}
}