using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PublishingCompany.Api.Data;
using PublishingCompany.Api.Data.Entities;
using PublishingCompany.Api.Repository.Interfaces;

namespace PublishingCompany.Api.Repository
{
    public class UserRepository : Repository<User, Guid>, IUserRepository
    { 
        private readonly DataContext _dataContext;

        public UserRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
        
        public async Task<List<Book>> GetUserBoughtBooks(Guid userId)
        {
            var user = await _dataContext.Users.Include(u => u.UserBoughtBooks).ThenInclude(books => books.Book)
                .FirstOrDefaultAsync(u => u.Id == userId);
            return user.UserBoughtBooks.Select(books => books.Book).ToList();
        }

        public async Task<Data.Entities.User> LogInUser(string email, string password)
        {
            return await _dataContext.Users.FirstOrDefaultAsync(u => u.Password == password && u.Email == email);
        }

        public async Task<User> GetUserWithBooks(Guid userId)
        {
            return await _dataContext.Users.Include(us => us.UserBoughtBooks).ThenInclude(ubb => ubb.Book)
                .FirstOrDefaultAsync(user => user.Id == userId);
        }

        public async Task<bool> SetUserSubscription(Guid userId)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(user => user.Id == userId);

            user.Subscribed = true;
            user.StartDateOfSubscription = DateTime.Now;
            user.EndDateOfSubscription = DateTime.Now.AddMonths(1);

            return (await _dataContext.SaveChangesAsync()) > 0;
        }
    }
}