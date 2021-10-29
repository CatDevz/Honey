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
    public class ModrinthHttpService : IDisposable
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions()
        {
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy()
        };

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

            return await _httpClient.GetFromJsonAsync<SearchResponse>(address, _jsonSerializerOptions);
        }

        public async Task<Mod?> GetModAsync(int modId)
        {
            string address = $"https://api.modrinth.com/api/v1/mod/{modId}";
            return await _httpClient.GetFromJsonAsync<Mod>(address, _jsonSerializerOptions);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}