using System;

namespace Localizer.Domain.Entities.Common
{
    public interface ICreatedAt
    {
        /// <summary>
        ///     Created time
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }
    }
    
}