using System;
using System.Text.Json.Serialization;

namespace SantanderCodingTest.Models
{
    public class Story
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string By { get; set; }

        // Original Unix timestamp
        public long Time { get; set; }

        // Computed property for DateTime formatted string
        
        [JsonPropertyName("Date Time")]
        public string FormattedTime => DateTimeOffset.FromUnixTimeSeconds(Time).ToString("yyyy-MM-ddTHH:mm:ss");

        public int Score { get; set; }

        // Maps "descendants" from JSON to "CommentsCount" in your model
        [JsonPropertyName("descendants")]
        public int CommentsCount { get; set; }

        // Hide 'Time' property from JSON output
        [JsonIgnore]
        public DateTimeOffset DateTime => DateTimeOffset.FromUnixTimeSeconds(Time);
    }
}
