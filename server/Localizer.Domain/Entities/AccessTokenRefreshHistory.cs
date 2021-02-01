using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Localizer.Domain.Entities.Common;

namespace Localizer.Domain.Entities
{
	public class AccountAccessTokenHistory : EntityBase
	{
		public string FromToken { get; set; }

		[Required]
		public string NewToken { get; set; }

		public virtual Account Account { get; set; }

		[Required]
		[ForeignKey(nameof(Account))]
		public int AccountId { get; set; }
	}
}