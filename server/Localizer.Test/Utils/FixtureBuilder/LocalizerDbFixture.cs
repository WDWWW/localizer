// unset

using Localizer.Api.Infrastructure.Helpers;
using Localizer.Domain;
using Localizer.Domain.Entities;

namespace Localizer.Test.Utils.FixtureBuilder
{
	using FixtureBuilder = FixtureBuilderBase<LocalizerDb>;
	public class LocalizerDbFixture : FixtureBuilderBase<LocalizerDb>
	{
		public static readonly string DefaultAccountPassword = "tester01!";
		
		public static readonly Account DefaultAccount = new()
		{
			Email = "tester@test.com",
			Name = "tester",
			EmailConfirmed = true,
			PasswordHash = PasswordHelper.HashPassword(DefaultAccountPassword),
		};
		
		public LocalizerDbFixture(LocalizerDb dbContext) : base(dbContext)
		{
		}

		protected override void ConfigureFixture()
		{
			Configure(() => DefaultAccount);
		}
	}
}