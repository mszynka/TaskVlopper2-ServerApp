using System;
using System.Linq;
using System.Threading.Tasks;
using FluentNHibernate.Testing;
using NHibernate;
using ServerApp.Infrastructure.Persistence;
using ServerApp.Infrastructure.Records;
using Xunit;

namespace ServerApp.Infrastructure.Tests.Records
{
    public class TestRecord : IRecord
    {
        public TestRecord()
        {
            Created = DateTimeOffset.Now;
        }

        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Creator { get; set; }
        public virtual DateTimeOffset Created { get; set; }
    }

    public class TestBaseRepository : BaseRepository<TestRecord>
    {
        public TestBaseRepository(ISession session) : base(session)
        {
        }
    }

    public class BaseRepositoryTests : IDisposable
    {
        private readonly TestBaseRepository _repository;
        private readonly ISession _session;

        public BaseRepositoryTests()
        {
            _session = SessionFactoryBuilder.BuildTestSessionFactory<TestRecord>().OpenSession();
            _repository = new TestBaseRepository(_session);
        }

        public void Dispose()
        {
            _session?.Dispose();
        }
        
        [Fact]
        public void Can_correctly_map_TestRecord()
        {
            new PersistenceSpecification<TestRecord>(_session)
                .CheckProperty(c => c.Id, 1)
                .CheckProperty(c => c.Name, "John")
                .CheckProperty(c => c.Creator, "Doe")
                .VerifyTheMappings();
        }

        [Fact]
        public async Task It_can_create_record_and_query_correct_result()
        {
            var record = new TestRecord()
            {
                Name = "Test name",
                Creator = "dr John Creator"
            };

            await _repository.CreateAsync(record);

            var records = await _repository.QueryAsync();

            Assert.Equal(1, records.Count);
            Assert.Equal(record.Name, records.First().Name);
            Assert.Equal(record.Creator, records.First().Creator);
            Assert.Equal(record.Created, records.First().Created);
        }
    }
}