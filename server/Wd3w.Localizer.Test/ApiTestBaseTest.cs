using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Wd3w.Localizer.Test.Utils;
using Xunit;

namespace Wd3w.Localizer.Test
{
    public class ApiTestBaseTest : ApiTestBase
    {
        public ApiTestBaseTest(LocalizerApplicationFactory factory) : base(factory)
        {
        }


        [Fact]
        public async Task Test()
        {
            var response = await CreateHestify("api/sample").GetAsync();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var body = await response.Content.ReadAsStringAsync();
            body.Should().Be("hello world");
        }
    }
}