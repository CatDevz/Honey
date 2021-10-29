using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Honey.Core.Modrinth.Models;
using O9d.Json.Formatting;

namespace Honey.Core.Modrinth
{
    public class ModrinthHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions()
        {
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy()
        };

        public ModrinthHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<SearchResponse?> SearchAsync(SearchOptions searchOptions)
        {
            var parameters = new Dictionary<string, string>()
            {
                {"q", searchOptions.Query},
                {"filters", searchOptions.Filters.BuildStatement()},
                {"index", searchOptions.SortBy.ToString()},
                {"offset", searchOptions.Offset.ToString()},
                {"limit", searchOptions.Limit.ToString()}
            };

            string queryParamsString = string.Join("&", parameters.Select((key, value) => $"{key}={value}"));
            string address = $"https://api.modrinth.com/api/v1/mod?{queryParamsString}";

            var httpClient = _httpClientFactory.CreateClient();
            return await httpClient.GetFromJsonAsync<SearchResponse>(address, _jsonSerializerOptions);
        }

        public async Task<Mod?> GetModAsync(int modId)
        {
            string address = $"https://api.modrinth.com/api/v1/mod/{modId}";
            
            var httpClient = _httpClientFactory.CreateClient();
            return await httpClient.GetFromJsonAsync<Mod>(address, _jsonSerializerOptions);
        }
    }
}