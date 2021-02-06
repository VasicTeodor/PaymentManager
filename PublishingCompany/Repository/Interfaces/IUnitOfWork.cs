using System.Threading.Tasks;

namespace PublishingCompany.Api.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IBookRepository BooksRepository { get; set; }
        IShoppingCartRepository ShoppingCartRepository { get; set; }
        IOrderRepository OrderRepository { get; set; }
        IUserRepository UserRepository { get; set; }
        int Complete();
        Task<int> CompleteAsync();
    }
}