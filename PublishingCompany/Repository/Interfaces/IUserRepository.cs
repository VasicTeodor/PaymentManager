using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PublishingCompany.Api.Data.Entities;

namespace PublishingCompany.Api.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task<List<Book>> GetUserBoughtBooks(Guid userId);
        Task<User> LogInUser(string email, string password);
        Task<User> GetUserWithBooks(Guid userId);
        Task<bool> SetUserSubscription(Guid userId);
    }
}