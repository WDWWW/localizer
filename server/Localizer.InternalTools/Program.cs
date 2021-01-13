using System.Threading.Tasks;
using Typin;

namespace Localizer.InternalTools
{
	class Program
	{
		static async Task Main(string[] args)
		{
			await new CliApplicationBuilder().AddCommandsFromThisAssembly().Build().RunAsync();
		}
	}
}