namespace Localizer.Domain.Entities.Common
{
	public interface IEntityKey<TKey> where TKey : struct
	{
		/// <summary>
		///     Internal Identifier for entity
		/// </summary>
		public TKey Id { get; set; }
	}
}