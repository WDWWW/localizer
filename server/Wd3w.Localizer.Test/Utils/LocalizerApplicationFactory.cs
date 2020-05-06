using Microsoft.AspNetCore.Mvc.Testing;
using Wd3w.Localizer.Api;

namespace Wd3w.Localizer.Test.Utils
{
    public class LocalizerApplicationFactory : WebApplicationFactory<Startup>
    {
        public LocalizerApplicationFactory()
        {
        }
    }
}