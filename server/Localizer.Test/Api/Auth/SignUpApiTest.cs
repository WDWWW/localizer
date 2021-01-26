// unset

using System.Threading.Tasks;
using Hestify;
using Localizer.Api.Resources.AuthResource.Models;
using Localizer.Domain;
using Localizer.Test.Utils;
using Wd3w.AspNetCore.EasyTesting;
using Wd3w.AspNetCore.EasyTesting.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace Localizer.Test.Api.Auth
{
	public class SignUpApiTest : ApiTestBase
	{
		public SignUpApiTest(ITestOutputHelper helper) : base(helper)
		{
		}

		[Fact]
		public async Task Test()
		{
			using var client = SUT
				.ReplaceInMemoryDbContext<LocalizerDb>()
				.ReplaceDistributedInMemoryCache()
				.SetupFixture((LocalizerDb db) =>
				{
					return Task.CompletedTask;
				})
				.CreateClient();

			var response = await client.Resource("api/auth/sign-up")
				.WithJsonBody(new SignUpRequest
				{
					Email = "sample@emil.com",
					Name = "tester",
					Password = "password!!"
				})
				.PostAsync();
			
			response.ShouldBeOk();
		}
	}
}