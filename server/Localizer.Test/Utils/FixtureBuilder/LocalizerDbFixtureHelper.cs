// unset

using System;
using System.Threading.Tasks;
using Localizer.Domain;
using Wd3w.AspNetCore.EasyTesting;

namespace Localizer.Test.Utils.FixtureBuilder
{
	public static class LocalizerDbFixtureHelper
	{
		public static SystemUnderTest SetupDb(this SystemUnderTest sut, Action<LocalizerDbFixture> builder)
		{
			return sut.SetupFixture<LocalizerDb>(db =>
			{
				builder(new LocalizerDbFixture(db));
				return Task.CompletedTask;
			});
		}
	}
}