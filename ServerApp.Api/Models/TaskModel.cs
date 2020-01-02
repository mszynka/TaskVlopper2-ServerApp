using System;
using Newtonsoft.Json;
using ServerApp.Api.Records;

namespace ServerApp.Api.Models
{
    public class TaskModel
    {
        public int? TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string Creator { get; set; }

        [JsonProperty("dt")] public DateTimeOffset Created { get; set; }

        public static TaskModel FromRecord(TaskRecord record)
        {
            return new TaskModel
            {
                TaskId = record.Id,
                Title = record.Title,
                Description = record.Description,
                Creator = record.Creator,
                Created = record.Created
            };
        }

        public TaskRecord ToRecord()
        {
            return new TaskRecord
            {
                Id = this.TaskId ?? 0,
                Title = this.Title,
                Description = this.Description,
                Creator = this.Creator,
                Created = this.Created
            };
        }
    }
}