using ExpressionTreeToString;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using ZSpitz.Util;

namespace efcoredemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Expression<Func<Book, bool>> e1 = b => b.Price > 5;
            Func<Book, bool> f1 = b => b.Price > 5;
            Expression<Func<Book, Book, double>> e2 = (b1, b2) => b1.Price + b2.Price;

            //Console.WriteLine(e1.ToString(BuiltinRe nderer.ObjectNotation, Language.CSharp));
            Console.WriteLine(e1.ToString(BuiltinRenderer.FactoryMethods, Language.CSharp));

            #region MyRegion
            // 手动创建表达式树
            //var paramB = Expression.Parameter(typeof(Book), "b");
            //var constExp5 = Expression.Constant(5.0, typeof(double));
            //var memberPrice = Expression.MakeMemberAccess(paramB, typeof(Book).GetProperty("Price"));
            //BinaryExpression binExpGreaterThan = Expression.GreaterThan(memberPrice, constExp5);
            //var expRoot = Expression.Lambda<Func<Book, bool>>(binExpGreaterThan, paramB);
            #endregion

            //using (MyDbContext dbContext = new MyDbContext())
            //{
            //    //dbContext.Books.Where(e1).ToArray(); // ef可以将其识别为sql语句，select * from T_Books where Price>5 
            //    dbContext.Books.Where(f1).ToArray(); // 获取所有数据再执行委托所绑定的方法 select * from T_Books
            //    //dbContext.Database.ExecuteSqlInterpolated(@$"insert into T_Books(Name, PubTime, Price) select Name, PubTime, Price from T_Books");
            //    //var items = dbContext.Books.AsNoTracking().ToList();


            //    //dbContext.SaveChanges();
            //}

            Console.ReadKey();
        }
    }
}
