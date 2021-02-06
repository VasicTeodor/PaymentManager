using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PublishingCompany.Api.Data.Entities;
using PublishingCompany.Api.Dtos;
using PublishingCompany.Api.Repository.Interfaces;
using PublishingCompany.Api.Services.Interfaces;

namespace PublishingCompany.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateOrder(OrderDto order)
        {
            var orderId = Guid.NewGuid();
            var newOrder = new Order()
            {
                OrderPrice = order.OrderPrice,
                UserId = order.UserId,
                Id = orderId,
                OrderItems = new List<OrderItem>()
            };

            foreach (var orderBookId in order.BookIds)
            {
                newOrder.OrderItems.Add(new OrderItem()
                {
                    BookId = orderBookId,
                    OrderId = orderId
                });
            }

            await _unitOfWork.OrderRepository.Add(newOrder);
            await _unitOfWork.CompleteAsync();
            return orderId;
        }

        public async Task<bool> CompleteOrder(Guid orderId, string status)
        {
            if (status != "SUCCESS")
            {
                return false;
            }

            var order = await _unitOfWork.OrderRepository.GetOrderWithItems(orderId);
            var orderedBooks = order.OrderItems.Select(item => item.Book).ToList();
            var user = await _unitOfWork.UserRepository.GetUserWithBooks(order.UserId);

            user.UserBoughtBooks ??= new List<UserBoughtBook>();

            foreach (var orderedBook in orderedBooks)
            {
                user.UserBoughtBooks.Add(new UserBoughtBook()
                {
                    User = user,
                    UserId = user.Id,
                    Book = orderedBook,
                    BookId = orderedBook.Id
                });
            }

            return (await _unitOfWork.CompleteAsync()) > 0;
        }
    }
}