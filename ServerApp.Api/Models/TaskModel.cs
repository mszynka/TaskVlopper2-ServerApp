using System;

using Newtonsoft.Json;

namespace ServerApp.Api.Models
{
    public class TaskModel
    {
        public int? TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string Creator { get; set; }

        [JsonProperty("dt")]
        public DateTimeOffset Created { get; set; }
    }
}
