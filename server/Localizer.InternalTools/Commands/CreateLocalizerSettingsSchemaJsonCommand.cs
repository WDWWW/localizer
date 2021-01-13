// unset

using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Localizer.Api;
using NJsonSchema;
using Typin;
using Typin.Attributes;
using Typin.Console;

namespace Localizer.InternalTools.Commands
{
	[Command("create-schema", Description = "Create json schema about appsettings.json")]
	public class CreateLocalizerSettingsSchemaJsonCommand : ICommand
	{
		class AppSettingsJson
		{
			[Required]
			public LocalizerSettings LocalizerSettings { get; set; }
		}

		[CommandOption("output", 'o', Description = "Output relative path")]
		public string Output { get; set; }

		public async ValueTask ExecuteAsync(IConsole console)
		{
			await console.Output.WriteLineAsync("Downloading recent jsonschema for asp.net core default appsettings json.");
			var defaultSettingsJsonSchema = await JsonSchema.FromUrlAsync("https://json.schemastore.org/appsettings");

			await console.Output.WriteLineAsync("Create appsettings json for localizer");
			var settingSchema = JsonSchema.FromType<AppSettingsJson>();

			await console.Output.WriteLineAsync("Combining schema..");
			settingSchema.AllOf.Add(defaultSettingsJsonSchema);

			await console.Output.WriteLineAsync("Create file for schmea");

			var outputFilePath = Path.Join(Directory.GetCurrentDirectory(), Output ?? "", "localizer.schema.json");
			
			await using var fileStream = File.Create(outputFilePath);
			await fileStream.WriteAsync(Encoding.UTF8.GetBytes(settingSchema.ToJson()));
			await console.Output.WriteLineAsync("Done");
		}
	}
}