using System;
using System.Threading.Tasks;
using PublishingCompany.Api.Data;
using PublishingCompany.Api.Repository.Interfaces;

namespace PublishingCompany.Api.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context, IBookRepository bookRepository, IOrderRepository orderRepository, IShoppingCartRepository shoppingCartRepository, IUserRepository userRepository)
        {
            _context = context;
            BooksRepository = bookRepository;
            ShoppingCartRepository = shoppingCartRepository;
            OrderRepository = orderRepository;
            UserRepository = userRepository;
        }

        public IBookRepository BooksRepository { get; set; }
        public IShoppingCartRepository ShoppingCartRepository { get; set; }
        public IOrderRepository OrderRepository { get; set; }
        public IUserRepository UserRepository { get; set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}