using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublishingCompany.Api.Repository.Interfaces
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        Task<TEntity> Get(TKey id);
        Task<IEnumerable<TEntity>> GetAll();
        Task Add(TEntity entity);
        void Update(TEntity entity);
        Task Delete(TKey id);
    }
}