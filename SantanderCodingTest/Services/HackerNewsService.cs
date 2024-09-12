using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using SantanderCodingTest.Models;

namespace SantanderCodingTest.Services
{
    public class HackerNewsService
    {
        private readonly HttpClient _httpClient;

        public HackerNewsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
    }

        // Fetches the IDs of the best stories
        public async Task<List<int>> GetBestStoryIdsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://hacker-news.firebaseio.com/v0/beststories.json");
                response.EnsureSuccessStatusCode();

                var storyIds = await response.Content.ReadFromJsonAsync<List<int>>();

                if (storyIds == null || !storyIds.Any())
                {
                    throw new Exception("Couln't find the Id of the Stories.");
                }

                return storyIds;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<Story> GetStoryDetailsAsync(int storyId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://hacker-news.firebaseio.com/v0/item/{storyId}.json");
                response.EnsureSuccessStatusCode();

                var story = await response.Content.ReadFromJsonAsync<Story>();

                if (story == null)
                {
                    throw new Exception($"Couln't find Story {storyId}.");
                }
                return story;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }
    }
}
