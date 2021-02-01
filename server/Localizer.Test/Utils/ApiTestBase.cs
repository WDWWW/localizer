using System;
using Localizer.Api;
using Localizer.Domain;
using Microsoft.Extensions.Logging;
using Wd3w.AspNetCore.EasyTesting;
using Wd3w.AspNetCore.EasyTesting.EntityFrameworkCore;
using Xunit.Abstractions;

namespace Localizer.Test.Utils
{
	public abstract class ApiTestBase : IDisposable
	{
		// ReSharper disable once InconsistentNaming
		protected readonly SystemUnderTest SUT;

		protected ApiTestBase(ITestOutputHelper helper)
		{
			SUT = new SystemUnderTest<Startup>()
				.DisableOptionValidations<LocalizerSettings>()
				.DisableStartupFilters()
				.ReplaceConfigureOptions<LocalizerSettings>(settings =>
				{
					settings.Authentication = new()
					{
						ServiceName = "localizer_test", 
						TokenSigningKey = "localizer_signing_key_test",
					};
					settings.DatabaseConnection = "";
					settings.EmailServer = new();
				})
				.ReplaceLoggerFactory(builder => builder.AddXUnit(helper))
				.ReplaceDistributedInMemoryCache()
				.ReplaceInMemoryDbContext<LocalizerDb>();
		}

		public void Dispose()
		{
			SUT.Dispose();
		}
	}
}