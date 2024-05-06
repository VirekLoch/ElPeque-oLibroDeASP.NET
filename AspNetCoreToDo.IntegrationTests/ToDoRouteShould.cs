using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace AspNetCoreTodo.IntegrationTests
{
    public class TodoRouteShould:IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public TodoRouteShould(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ChallengeAnonymousUser()
        {
            //Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "/todo");

            //Act: request the /to-do route
            var response = await _client.SendAsync(request);

            //Assert: the user is sent to the login page
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("http://localhost/Identity/Account/Login?ReturnUrl=%2Ftodo", response.RequestMessage.RequestUri.ToString());
        }
    }
}