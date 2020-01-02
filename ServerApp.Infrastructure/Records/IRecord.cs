using System;

namespace ServerApp.Infrastructure.Records
{
    public interface IRecord
    {
        int Id { get; set; }

        string Creator { get; set; }

        DateTimeOffset Created { get; set; }
    }
}