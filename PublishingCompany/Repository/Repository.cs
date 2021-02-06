using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PublishingCompany.Api.Data;
using PublishingCompany.Api.Repository.Interfaces;

namespace PublishingCompany.Api.Repository
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            this._context = context;
        }

        public async Task Add(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }


        public async Task Delete(TKey id)
        {
            var getEntity = Get(id);
            _context.Set<TEntity>().Remove(await getEntity);
        }

        public async Task<TEntity> Get(TKey id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

    }
}