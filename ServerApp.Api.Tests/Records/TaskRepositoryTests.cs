using FluentNHibernate.Testing;
using ServerApp.Api.Records;
using ServerApp.Infrastructure.Persistence;
using Xunit;

namespace ServerApp.Api.Tests.Records
{
    public class TaskRepositoryTests
    {
        [Fact]
        public void Can_correctly_map_TaskRecord()
        {
            new PersistenceSpecification<TaskRecord>(SessionFactoryBuilder.BuildTestSessionFactory<TaskRecord>().OpenSession())
                .CheckProperty(c => c.Id, 1)
                .CheckProperty(c => c.Title, "John")
                .CheckProperty(c => c.Description, "Some long text testing description")
                .CheckProperty(c => c.Creator, "Doe")
                .VerifyTheMappings();
        }
    }
}