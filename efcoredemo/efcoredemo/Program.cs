using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace efcoredemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MyDbContext dbContext = new MyDbContext())
            {
                //Book book = new Book()
                //{
                //    Title = "Java",
                //    PubTime = DateTime.Now,
                //    Price = 9.9
                //};

                //dbContext.Books.Add(book);
                //var book = dbContext.Books.FirstOrDefault();
                //book.Name = "Java 从入门到入土";

                //dbContext.Database.ExecuteSqlInterpolated(@$"insert into T_Books(Name, PubTime, Price) select Name, PubTime, Price from T_Books");
                //var items = dbContext.Books.AsNoTracking().ToList();

           


                dbContext.SaveChanges();
            }

            Console.ReadKey();
        }
    }
}
