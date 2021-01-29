// unset

using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.Core;

namespace Localizer.Test.Utils
{
	public static class NSubstituteHelper
	{

		public static ConfiguredCall ReturnsAsync(this Task value) => value.Returns(Task.CompletedTask);

		public static ConfiguredCall ReturnsAsync<T>(this Task<T> value, T returns) => value.Returns(Task.FromResult(returns));
	}
}