using System;
using Localizer.Api;
using Microsoft.Extensions.Logging;
using Wd3w.AspNetCore.EasyTesting;
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
				.ReplaceLoggerFactory(builder => builder.AddXUnit(helper))
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
				});
		}

		public void Dispose()
		{
			SUT.Dispose();
		}
	}
}