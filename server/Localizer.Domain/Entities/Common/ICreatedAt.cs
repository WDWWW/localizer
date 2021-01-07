using System;

namespace Localizer.Domain.Entities.Common
{
    public interface ICreatedAt
    {
        public DateTimeOffset CreatedAt { get; set; }
    }
    
}