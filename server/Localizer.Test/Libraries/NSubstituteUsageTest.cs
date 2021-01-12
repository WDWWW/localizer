using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Localizer.Test.Libraries
{
	public class NSubstituteUsageTest
	{
		private readonly ISomethingProvider _somethingProvider;

		public NSubstituteUsageTest()
		{
			_somethingProvider = Substitute.For<ISomethingProvider>();
		}

		[Fact]
		public void MockPropertyTest()
		{
			// Given
			_somethingProvider.SomethingNow.Returns("hello");

			// When
			var result = _somethingProvider.SomethingNow;

			// Then
			result.Should().Be("hello");
			var _ = _somethingProvider.Received().SomethingNow; // property verifying call count
		}

		[Fact]
		public void MockMethod()
		{
			// Given
			_somethingProvider.GetSomething().Returns("hello_method_call");

			// When
			var result = _somethingProvider.GetSomething();

			// Then
			result.Should().Be("hello_method_call");
			_somethingProvider.Received().GetSomething();
		}

		[Fact]
		public async Task MockAsyncMethod()
		{
			// Given
			_somethingProvider.GetSomethingAsync().Returns("hello_async_method_call");

			// When
			var result = await _somethingProvider.GetSomethingAsync();

			// Then
			result.Should().Be("hello_async_method_call");
			await _somethingProvider.Received().GetSomethingAsync();
		}

		public interface ISomethingProvider
		{
			public string SomethingNow { get; set; }

			public string GetSomething();

			public Task<string> GetSomethingAsync();

			public void SetSomething(string value);
		}
	}
}