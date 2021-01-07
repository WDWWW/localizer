using System;

namespace Localizer.Common
{
    public class UtcDateTimeOffsetProvider : IDateTimeOffsetProvider
    {
        public DateTimeOffset Now => DateTimeOffset.UtcNow;
    }
}