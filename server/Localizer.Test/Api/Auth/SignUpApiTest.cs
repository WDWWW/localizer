using System.Net;
using System.Threading.Tasks;
using Localizer.Api.Infrastructure;
using Localizer.Api.Resources.AuthResource.Models;
using Localizer.Domain.Entities;
using Localizer.Test.Utils;
using Localizer.Test.Utils.FixtureBuilder;
using NSubstitute;
using Wd3w.AspNetCore.EasyTesting;
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
			var response = await SUT.PostAsync("api/auth/sign-up",
				new SignUpRequest
				{
					Email = "sample@emil.com", 
					Name = "tester", 
					Password = "password!!",
				});

			// Then	
			response.ShouldBeOk();
			SUT.VerifyEntityExistsByCondition<Account>(acc =>
				acc.Email == "sample@emil.com" && acc.EmailConfirmed == false);
			SUT.UseSubstitute<IEmailService>(service => service
				.Received()
				.SendMailAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()));
		}

		[Fact]
		public async Task Should_ResponseBadRequest_When_RequestedEmailAlreadyExists()
		{
			// Given
			SUT.SetupDbFixture(fixture => fixture.Add<Account>());

			// When
			var message = await SUT.PostAsync("api/auth/sign-up",
				new SignUpRequest
				{
					Email = LocalizerDbFixture.DefaultAccount.Email,
					Name = "display name",
					Password = "somePassword01!",
				});

			// Then
			message.ShouldBe(HttpStatusCode.BadRequest);
		}
	}
}