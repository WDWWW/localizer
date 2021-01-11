using Localizer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Localizer.Domain
{
    public class LocalizerDb : DbContext
    {
        public Account Accounts { get; set; }

        public LocalizerDb(DbContextOptions options) : base(options)
        {
        }
    }
}