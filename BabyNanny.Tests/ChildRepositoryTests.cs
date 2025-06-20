using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using BabyNanny.Data;
using BabyNanny.Models;
using Xunit;

public class ChildRepositoryTests
{
    private class Handler : HttpMessageHandler
    {
        private readonly Func<HttpRequestMessage, HttpResponseMessage> _handler;
        public Handler(Func<HttpRequestMessage, HttpResponseMessage> handler)
        {
            _handler = handler;
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_handler(request));
        }
    }

    [Fact]
    public async Task CreateChildAsync_ReturnsCreatedChild()
    {
        var expected = new Child { Id = 5, Name = "Test" };
        var handler = new Handler(req =>
        {
            Assert.Equal(HttpMethod.Post, req.Method);
            var response = new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new StringContent(JsonSerializer.Serialize(expected))
            };
            return response;
        });
        var client = new HttpClient(handler) { BaseAddress = new Uri("http://localhost") };
        var repo = new BabyNannyRepository("test.db", client);
        var result = await repo.CreateChildAsync(new Child { Name = "Test" });
        Assert.NotNull(result);
        Assert.Equal(expected.Id, result!.Id);
    }
}
