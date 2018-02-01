using System;
using System.IO;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNetCore.Hosting;

namespace ModernNotes.integrationTests
{
    public class ModernNotesShould
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public ModernNotesShould (){
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseContentRoot(GetContentRoot()));
            _client = _server.CreateClient();
        }

        // Not proud of this one, but it's a bug in asp.net core 2 with testhost
        // using dotnet xunit: https://github.com/xunit/xunit/issues/1519 We
        // simply use it to set the root directory for the test server above
        // (for serving static files). Also mentioned in a comment here:
        // https://docs.microsoft.com/en-us/aspnet/core/testing/integration-testing
        public static string GetContentRoot()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var webSiteRelativePath = @"../../../../../ModernNotes";
            return Path.Combine(currentDirectory, webSiteRelativePath);
        }
        [Fact]
        public async Task ReturnIndex()
        {
            var response = await _client.GetAsync("/");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal("Hello World!", responseString);
            }
    }
}
