using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly Calculator calculator;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache; // redis

        public TestController(Calculator calculator, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.calculator = calculator;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        static List<Person> persons = new List<Person>()
        {
            new Person() { Id = 1, Name = "Alex" },
            new Person() { Id = 11, Name = "Black" },
            new Person() { Id = 111, Name = "Carbon" }
        };

        private Person? GetPerson(int id)
        {
            return persons.FirstOrDefault(p => p.Id == id);
        }

        [HttpGet]
        public ActionResult<Person?> TestMemoryCache(int id)
        {
            var result = this.memoryCache.GetOrCreate("person" + id, cacheEntry =>
            {
                Console.WriteLine("缓存里未找到...,正在从数据库获取");
                //cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                //cacheEntry.SlidingExpiration = TimeSpan.FromSeconds(10);
                // 过期时间随机
                cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(Random.Shared.Next(10, 15));
                return GetPerson(id);
            });

            Console.WriteLine(string.Format("当前id={0}的信息 {1}", id, GetPerson(id).Name));
            if (result == null)
            {
                return NotFound("Not found ...");
            }

            return Ok(result);
        }

        [HttpPut]
        public ActionResult ChangePersons(int id, string name)
        {
            var person = persons.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return NotFound("Not found person ...");
            }
            person.Name = name;
            //this.memoryCache.Remove("person" + id);
            return Ok(GetPerson(id));
        }

        [HttpGet("{x}/{y}")]
        public ActionResult Calculate(int x, int y)
        {
            int result = 0;

            result = this.calculator.Calculate(x, y);

            return Ok(result);
        }

        [ResponseCache(Duration = 20)]
        [HttpGet]
        public DateTime Now()
        {
            return DateTime.Now;
        }

        [HttpGet]
        public ActionResult<Person?> TestDistributedCache(int id)
        {
            string? str = this.distributedCache.GetString("person" + id);
            Person? person;
            if (str == null)
            {
                person = GetPerson(id);
                this.distributedCache.SetString("person" + id, JsonSerializer.Serialize(person));
            }
            else
            {
                person = JsonSerializer.Deserialize<Person?>(str);
            }
            
            if (person == null)
            {
                return NotFound("Not found");
            }

            return Ok(person);
        }
    }

    public class Calculator
    {
        public int Calculate(int x, int y)
        {
            return x + y;
        }
    }
}
