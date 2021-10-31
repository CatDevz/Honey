using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using Honey.Core.Modrinth;
using Honey.Core.Modrinth.Filters;
using Honey.Core.Modrinth.Models;
using RichardSzalay.MockHttp;
using Xunit;

namespace Honey.Core.Tests
{
    public class FakeHttpClientFactory : IHttpClientFactory
    {
        private readonly HttpClient _httpClient;

        public FakeHttpClientFactory(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public HttpClient CreateClient(string name) => _httpClient;
    }
    
    public class ModrinthHttpTests
    {
        [Fact]
        public async Task SearchTest()
        {
            var expectedResponse = new SearchResponse(new List<ModResult>(), 0, 0, 0);
            string url = "https://api.modrinth.com/api/v1/mod?q=MyMod";
            
            var mockHttpMessageHandler = new MockHttpMessageHandler();
            
            mockHttpMessageHandler
                .When(url)
                .Respond("application/json", JsonSerializer.Serialize(expectedResponse));

            var httpClientFactory = new FakeHttpClientFactory(mockHttpMessageHandler.ToHttpClient());
            var modrinthService = new ModrinthHttpService(httpClientFactory);

            var actualResponse = await modrinthService.SearchAsync(new SearchOptions { Query = "MyMod" });
            
            Assert.Equal(expectedResponse.Hits, actualResponse?.Hits);
            Assert.Equal(expectedResponse.Limit, actualResponse?.Limit);
            Assert.Equal(expectedResponse.Offset, actualResponse?.Offset);
            Assert.Equal(expectedResponse.TotalHits, actualResponse?.TotalHits);
        }

        [Fact]
        public async Task SearchWithFilterTest()
        {
            var expectedResponse = new SearchResponse(new List<ModResult>(), 0, 10, 0);
            string url = "https://api.modrinth.com/api/v1/mod?q=MyMod&filters=categories=fabric&index=relevance&offset=0&limit=10";
            
            var mockHttpMessageHandler = new MockHttpMessageHandler();
            
            mockHttpMessageHandler
                .When(url)
                .Respond("application/json", JsonSerializer.Serialize(expectedResponse));

            var httpClientFactory = new FakeHttpClientFactory(mockHttpMessageHandler.ToHttpClient());
            var modrinthService = new ModrinthHttpService(httpClientFactory);

            var actualResponse = await modrinthService.SearchAsync(new SearchOptions
            {
                Query = "MyMod",
                Filters = new FilterBuilder()
                    .Filter("categories", "fabric")
            });
            
            Assert.Equal(expectedResponse.Hits, actualResponse?.Hits);
            Assert.Equal(expectedResponse.Limit, actualResponse?.Limit);
            Assert.Equal(expectedResponse.Offset, actualResponse?.Offset);
            Assert.Equal(expectedResponse.TotalHits, actualResponse?.TotalHits);
        }
    }
}