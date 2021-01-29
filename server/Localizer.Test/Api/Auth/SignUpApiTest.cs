// unset

using System.Threading.Tasks;
using Localizer.Api.Infrastructure;
using Localizer.Api.Resources.AuthResource.Models;
using Localizer.Domain.Entities;
using Localizer.Test.Utils;
using NSubstitute;
using Wd3w.AspNetCore.EasyTesting;
using Wd3w.AspNetCore.EasyTesting.Hestify;
using Wd3w.AspNetCore.EasyTesting.NSubstitute;
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
		public async Task Should_CreateAccountWithSendingConfirmationCodeEmail_When_AllRequiredInfoIsValid()
		{
			// Given
			SUT.ReplaceWithSubstitute<IEmailService>(service => service
				.SendMailAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
				.ReturnsAsync());

			// When 
			var response = await SUT.Resource("api/auth/sign-up")
				.WithJsonBody(new SignUpRequest
				{
					Email = "sample@emil.com",
					Name = "tester",
					Password = "password!!",
				})
				.PostAsync();
			
			// Then
			response.ShouldBeOk();
			SUT.VerifyEntityExistsByCondition<Account>(account => account.Email == "sample@emil.com" && account.EmailConfirmed == false);
			SUT.UseSubstitute<IEmailService>(service => service
				.Received()
				.SendMailAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()));
		}
	}
}