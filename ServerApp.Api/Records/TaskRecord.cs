using System;
using NHibernate;
using ServerApp.Infrastructure.Records;

namespace ServerApp.Api.Records
{
    public class TaskRecord : IRecord
    {
        public TaskRecord()
        {
            Created = DateTimeOffset.Now;
        }

        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }

        public virtual string Creator { get; set; }

        public virtual DateTimeOffset Created { get; set; }
    }

    public interface ITaskRepository : IBaseRepository<TaskRecord>
    {
    }

    public class TaskRepository : BaseRepository<TaskRecord>, ITaskRepository
    {
        public TaskRepository(ISession _session) : base(_session)
        {
        }
    }
}