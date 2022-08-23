using ExpressionTreeToString;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
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
            //Console.WriteLine(e1.ToString(BuiltinRenderer.FactoryMethods, Language.CSharp));

            #region 表达式树
            // 手动创建表达式树
            //var paramB = Expression.Parameter(typeof(Book), "b");
            //var constExp5 = Expression.Constant(5.0, typeof(double));
            //var memberPrice = Expression.MakeMemberAccess(paramB, typeof(Book).GetProperty("Price"));
            //BinaryExpression binExpGreaterThan = Expression.GreaterThan(memberPrice, constExp5);
            //var expRoot = Expression.Lambda<Func<Book, bool>>(binExpGreaterThan, paramB);
            #endregion

            #region MyRegion
            //using (MyDbContext dbContext = new MyDbContext())
            //{
            //    //dbContext.Books.Where(e1).ToArray(); // ef可以将其识别为sql语句，select * from T_Books where Price>5 
            //    dbContext.Books.Where(f1).ToArray(); // 获取所有数据再执行委托所绑定的方法 select * from T_Books
            //    //dbContext.Database.ExecuteSqlInterpolated(@$"insert into T_Books(Name, PubTime, Price) select Name, PubTime, Price from T_Books");
            //    //var items = dbContext.Books.AsNoTracking().ToList();


            //    //dbContext.SaveChanges();
            //}

            #endregion

            var result = Query<Book>("Id", "Name");
            foreach (var item in result)
            {
                Console.WriteLine(item[0] + "\t" + item[1]);
            }
             
            Console.ReadKey();
        }

        static IEnumerable<object[]> Query<T>(params string[] propertyNames) where T : class
        {
            var param = Expression.Parameter(typeof(T));
            List<Expression> expressions = new List<Expression>();

            foreach (var proName in propertyNames)
            {
                Expression expression = Expression.Convert(Expression.MakeMemberAccess(param, typeof(T).GetProperty(proName)), typeof(object));

                expressions.Add(expression);
            }


            var newArrExp = Expression.NewArrayInit(typeof(object), expressions.ToArray());
            var selectExp = Expression.Lambda<Func<T, object[]>>(newArrExp, param);

            using (MyDbContext ctx = new MyDbContext())
            {
                return ctx.Set<T>().Select(selectExp).ToArray();
            }
        }


    }
}
