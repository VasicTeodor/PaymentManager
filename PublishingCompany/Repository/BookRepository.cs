using System;
using PublishingCompany.Api.Data;
using PublishingCompany.Api.Data.Entities;
using PublishingCompany.Api.Repository.Interfaces;

namespace PublishingCompany.Api.Repository
{
    public class BookRepository : Repository<Book, Guid>, IBookRepository
    {
        private readonly DataContext _context;

        public BookRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}