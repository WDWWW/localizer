using System;

namespace Localizer.Domain.Entities.Common
{
	public interface IUpdatedAt
	{
		/// <summary>
		///     Updated time
		/// </summary>
		public DateTimeOffset UpdatedAt { get; set; }
	}
}