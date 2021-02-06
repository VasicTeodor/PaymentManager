

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using PublishingCompany.Api.Data.Entities;

namespace PublishingCompany.Api.Data
{
    public class Seed
    {
        private readonly DataContext _dataContext;

        public Seed(DataContext context)
        {
            _dataContext = context;
        }

        public void SeedData()
        {
            SeedBooks();
            SeedUsers();
        }

        private void SeedUsers()
        {
            if (!_dataContext.Users.Any())
            {
                
                var user = new User()
                {
                    Email = "user@gmail.com",
                    Name = "Pera",
                    Surname = "Peric",
                    City = "Novi Sad",
                    Country = "Srbija",
                    IsBetaReader = false,
                    Password = "1234",
                    UserRole = UserRole.READER.ToString()
                };

                _dataContext.Users.Add(user);

                var admin = new User
                {
                    Email = "writer@gmail.com",
                    Name = "Pera",
                    Surname = "Peric",
                    City = "Novi Sad",
                    Country = "Srbija",
                    IsBetaReader = false,
                    UserRole = UserRole.WRITER.ToString(),
                    Password = "1234"
                };

                _dataContext.Users.Add(admin);
                _dataContext.SaveChanges();
            }
        }

        private void SeedBooks()
        {
            if (!_dataContext.Books.Any())
            {
                var bookList = new List<Book>
                {
                    new Book()
                    {
                        Genre = "Comedy",
                        Isbn = 1231425415,
                        NumberOfPages = 233,
                        PlaceOfPublishing = "Serbia",
                        Price = 250,
                        Publisher = "Agamonda",
                        Synopsis = "Book of comedy",
                        Title = "Comedy 101",
                        Writers = "Pera, Djoka, Misa"
                    },
                    new Book()
                    {
                        Genre = "Sci-Fi",
                        Isbn = 21451223,
                        NumberOfPages = 233,
                        PlaceOfPublishing = "Serbia",
                        Price = 322,
                        Publisher = "FreFly",
                        Synopsis = "Book of something",
                        Title = "Something 101",
                        Writers = "Misa"
                    },
                    new Book()
                    {
                        Genre = "Fantasy",
                        Isbn = 15334262,
                        NumberOfPages = 534,
                        PlaceOfPublishing = "USA",
                        Price = 250,
                        Publisher = "USA Publish",
                        Synopsis = "Book of hobits",
                        Title = "Hobit",
                        Writers = "Tolkin"
                    },
                    new Book()
                    {
                        Genre = "Fantasy",
                        Isbn = 6786765,
                        NumberOfPages = 567,
                        PlaceOfPublishing = "UK",
                        Price = 567,
                        Publisher = "JK",
                        Synopsis = "Book of wizards",
                        Title = "Harry Potter",
                        Writers = "J.K. Rowling"
                    },
                    new Book()
                    {
                        Genre = "Fantasy",
                        Isbn = 3467889767,
                        NumberOfPages = 443,
                        PlaceOfPublishing = "UK",
                        Price = 250,
                        Publisher = "Trotter Co",
                        Synopsis = "Book of comedy",
                        Title = "Harry Trotter",
                        Writers = "Derek and Rodney Trotter"
                    },
                    new Book()
                    {
                        Genre = "Science",
                        Isbn = 3467889767,
                        NumberOfPages = 443,
                        PlaceOfPublishing = "USA",
                        Price = 250,
                        Publisher = "Trotter Co",
                        Synopsis = "Book of finance",
                        Title = "Bogati Otac, Siromasni Otac",
                        Writers = "Robert Kiyosaki and Sharon Lechter"
                    }
                };

                foreach (var book in bookList)
                {
                    _dataContext.Books.Add(book);
                }

                _dataContext.SaveChanges();
            }
        }

    }
}