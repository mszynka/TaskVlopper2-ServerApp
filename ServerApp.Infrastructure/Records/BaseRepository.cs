using System.Collections.Generic;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Criterion;

namespace ServerApp.Infrastructure.Records
{
    public interface IBaseRepository<TRecord> where TRecord : class, IRecord
    {
        Task<IList<TRecord>> QueryAsync();
        Task<TRecord> GetAsync(int id);
        Task CreateAsync(TRecord record);
        Task<TRecord> UpdateAsync(TRecord record);
        Task<TRecord> DeleteAsync(int id);
    }

    public abstract class BaseRepository<TRecord> : IBaseRepository<TRecord>
        where TRecord : class, IRecord
    {
        private readonly ISession _session;

        protected BaseRepository(ISession session)
        {
            _session = session;
        }

        public async Task<IList<TRecord>> QueryAsync()
        {
            var criteria = _session.CreateCriteria<TRecord>();
            criteria.AddOrder(Order.Asc(nameof(IRecord.Id)));
            return await criteria.ListAsync<TRecord>();
        }

        public async Task<TRecord> GetAsync(int id)
        {
            return await _session.GetAsync<TRecord>(id);
        }

        public async Task CreateAsync(TRecord record)
        {
            await _session.SaveOrUpdateAsync(record);
        }

        public async Task<TRecord> UpdateAsync(TRecord record)
        {
            await _session.SaveOrUpdateAsync(record);
            return record;
        }

        public async Task<TRecord> DeleteAsync(int id)
        {
            var record = await GetAsync(id);
            await _session.DeleteAsync(record);
            return record;
        }
    }
}