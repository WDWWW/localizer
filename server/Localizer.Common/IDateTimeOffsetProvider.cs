using System;

namespace Localizer.Common
{
	public interface IDateTimeOffsetProvider
	{
		public DateTimeOffset Now { get; }
	}
}