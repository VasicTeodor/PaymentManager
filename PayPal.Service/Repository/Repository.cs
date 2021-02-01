using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PayPal.Service.Data;
using PayPal.Service.Repository.Interfaces;

namespace PayPal.Service.Repository
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        private readonly PayPalContext _context;

        public Repository(PayPalContext context)
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