using Localizer.Api;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Localizer.Test.Utils
{
    public class LocalizerApplicationFactory : WebApplicationFactory<Startup>
    {
        public LocalizerApplicationFactory()
        {
        }
    }
}