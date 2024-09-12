using Microsoft.AspNetCore.Mvc;
using SantanderCodingTest.Services;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SantanderCodingTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HackerNewsController : ControllerBase
    {
        private readonly HackerNewsService _hackerNewsService;

        public HackerNewsController(HackerNewsService hackerNewsService)
        {
            _hackerNewsService = hackerNewsService;
        }

        [HttpGet("best")]

        public async Task<IActionResult> GetBestStoriesWithScores([FromQuery] int numberOfStories = 10)
        {
            try
            {
                var storyIds = await _hackerNewsService.GetBestStoryIdsAsync();
                var tasks = storyIds.Take(numberOfStories).Select(id => _hackerNewsService.GetStoryDetailsAsync(id));
                var stories = await Task.WhenAll(tasks);

                if (stories == null || !stories.Any())
                {
                    return StatusCode(500, "Stories couldn't be loaded.");
                }
                var sortedStories = stories
                    .OrderByDescending(s => s.Score)
                    .Take(numberOfStories);

                var result = sortedStories.Select(s => new
                {
                    s.Title,
                    uri =s.Url,
                    postedBy=s.By,
                    time = s.FormattedTime,  // Keeping only the formatted date
                    score=s.Score,
                    commentsCount = s.CommentsCount // Renaming descendants to CommentsCount
                });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"error: {ex.Message}");
            }
        }
    }
}
