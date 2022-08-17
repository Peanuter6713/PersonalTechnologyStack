using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace efcoredemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Expression<Func<Book, bool>> e1 = b => b.Price > 5;

            Func<Book, bool> f1 = b => b.Price > 5;

            using (MyDbContext dbContext = new MyDbContext())
            {
                //dbContext.Books.Where(e1).ToArray();
                dbContext.Books.Where(f1).ToArray();
                //dbContext.Database.ExecuteSqlInterpolated(@$"insert into T_Books(Name, PubTime, Price) select Name, PubTime, Price from T_Books");
                //var items = dbContext.Books.AsNoTracking().ToList();


                //dbContext.SaveChanges();
            }

            Console.ReadKey();
        }
    }
}
