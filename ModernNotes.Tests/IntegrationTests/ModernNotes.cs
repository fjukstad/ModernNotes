using System;
using System.IO;
using System.Text;
using System.Net;
using ModernNotes.Models; 
using Xunit;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace ModernNotes.integrationTests
{
    public class ModernNotesShould
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public ModernNotesShould (){
            // Arrange
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
        public async Task ServeHomePage()
        {
            // Act
            var response = await _client.GetAsync("/");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Contains("Modern Notes", responseString);
        }


        [Fact]
        public async Task AllowUsersToStoreNewNotes(){
            var note = new Note(){
                Id = 1,
                Title = "test-note-1-title",
                Content = "test-note-1-content"
            }; 

            var json = JsonConvert.SerializeObject(note);
            var postBody = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/new", postBody);
            var responseCode = response.StatusCode;
            Assert.Equal(HttpStatusCode.Created, responseCode);

            string responseContent = await response.Content.ReadAsStringAsync();
            Note returnedNote = JsonConvert.DeserializeObject<Note>(responseContent);

            Assert.Equal(json.ToString(), responseContent);

        }

        [Fact]
        public async Task AllowUsersToViewOldNotes(){
            var note = new Note(){
                Id = 1,
                Title = "test-note-1-title",
                Content = "test-note-1-content"
            }; 

            var json = JsonConvert.SerializeObject(note);
            var postBody = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/new", postBody);

            var response = await _client.GetAsync("/api/notes/1");
            response.EnsureSuccessStatusCode();

            var oldNote = await response.Content.ReadAsStringAsync();
            Assert.Equal(json.ToString(), oldNote); 
        }

        [Fact]
        public async Task AllowUsersToChangeAPreviousWrittenNote(){
            var note = new Note(){
                Id = 1,
                Title = "test-note-1-title",
                Content = "test-note-1-content"
            }; 

            var json = JsonConvert.SerializeObject(note);
            var postBody = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/new", postBody);

            note.Content = note.Content + "-with-some-additions";
            json = JsonConvert.SerializeObject(note);
            postBody = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("/api/update/1", postBody);
            var responseCode = response.StatusCode;
            Assert.Equal(HttpStatusCode.NoContent, responseCode);

            response = await _client.GetAsync("/api/notes/1");
            var updatedNote = await response.Content.ReadAsStringAsync();
            Assert.Equal(json.ToString(), updatedNote);
        }

        [Fact]
        public async Task AllowUsersToDeleteAPreviousWrittenNote(){
            var note = new Note(){
                Id = 1,
                Title = "test-note-1-title",
                Content = "test-note-1-content"
            }; 

            var json = JsonConvert.SerializeObject(note);
            var postBody = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/new", postBody);

            var response = await _client.PostAsync("/api/delete/1", null);
            var responseCode = response.StatusCode;
            Assert.Equal(HttpStatusCode.NoContent, responseCode);

            response = await _client.GetAsync("/api/notes/1");
            responseCode = response.StatusCode;
            Assert.Equal(HttpStatusCode.NotFound, responseCode);
        }
    }
}
