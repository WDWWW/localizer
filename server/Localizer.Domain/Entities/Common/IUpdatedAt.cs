using System;

namespace Localizer.Domain.Entities.Common
{
    public interface IUpdatedAt
    {
        public DateTimeOffset UpdatedAt { get; set; }
    }
}