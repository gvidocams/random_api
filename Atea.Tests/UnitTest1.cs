using System.Threading.Tasks;
using Atea.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refit;

namespace Atea.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var publicApi = RestService.For<IPublicApi>("https://api.publicapis.org");
            var response = await publicApi.GetApi();

            response.Should().NotBeNull();
        }
    }
}
