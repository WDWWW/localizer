using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using Localizer.Api.Resources.AuthResource.Models;
using Localizer.Domain.Entities;
using Localizer.Test.Utils;
using Localizer.Test.Utils.FixtureBuilder;
using Wd3w.AspNetCore.EasyTesting;
using Xunit;
using Xunit.Abstractions;

namespace Localizer.Test.Api.Auth
{
	public class SignInApiTest : ApiTestBase
	{
		public SignInApiTest(ITestOutputHelper helper) : base(helper)
		{
		}

		[Fact]
		public async Task Should_ReturnNotFound_When_EmailDoesntExists()
		{
			// Given
			// When
			var message = await SUT.PostAsync("api/auth/sign-in", new SignInRequest {Email = "none@user.com", Password = "just-test"});

			// Then
			message.ShouldBe(HttpStatusCode.NotFound);
		}

		[Fact]
		public async Task Should_ReturnForbid_When_RequestEmailAccountIsUnconfirmed()
		{
			// Given
			SUT.SetupDbFixture(_ => _.Add<Account>(account => account.EmailVerificationCode = "NOCONF"));

			// When
			var message = await SUT.PostAsync("api/auth/sign-in",
				new SignInRequest {Email = LocalizerDbFixture.DefaultAccount.Email, Password = LocalizerDbFixture.DefaultAccountPassword});

			// Then
			message.ShouldBe(HttpStatusCode.Forbidden);
		}

		[Fact]
		public async Task Should_ReturnUnauthorized_When_RequestPasswordIsInvalid()
		{
			// Given
			SUT.SetupDbFixture(_ => _.Add<Account>());

			// When
			var message = await SUT.PostAsync("api/auth/sign-in",
				new SignInRequest {Email = LocalizerDbFixture.DefaultAccount.Email, Password = "invalid_password"});

			// Then
			message.ShouldBe(HttpStatusCode.Unauthorized);
		}

		[Fact]
		public async Task Should_ReturnJWT_When_RequestPasswordIsInvalid()
		{
			// Given
			SUT.SetupDbFixture(_ => _.Add<Account>());

			// When
			var message = await SUT.PostAsync("api/auth/sign-in",
				new SignInRequest {Email = LocalizerDbFixture.DefaultAccount.Email, Password = LocalizerDbFixture.DefaultAccountPassword});

			// Then
			var response = await message.ShouldBeCodeWithBody<SignInResponse>(HttpStatusCode.OK);
			response.Token.Should().NotBeNullOrEmpty();
		}
	}
}