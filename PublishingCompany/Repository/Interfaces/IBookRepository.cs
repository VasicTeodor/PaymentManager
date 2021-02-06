using System;
using PublishingCompany.Api.Data.Entities;

namespace PublishingCompany.Api.Repository.Interfaces
{
    public interface IBookRepository : IRepository<Book, Guid>
    {
        
    }
}