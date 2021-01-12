namespace Localizer.Domain.Entities.Common
{
	public interface ISoftDelete
	{
		/// <summary>
		///     Whether the entity is deleted
		/// </summary>
		public bool IsDeleted { get; set; }
	}
}