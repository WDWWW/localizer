using System;

namespace Localizer.Domain.Entities.Common
{
	public abstract class EntityBase : IEntityKey<int>, ICreatedAt, IUpdatedAt, ISoftDelete
	{
		/// <inheritdoc />
		public DateTimeOffset CreatedAt { get; set; }

		/// <inheritdoc />
		public int Id { get; set; }

		/// <inheritdoc />
		public bool IsDeleted { get; set; }

		/// <inheritdoc />
		public DateTimeOffset UpdatedAt { get; set; }
	}
}