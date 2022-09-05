# 缓存

## 浏览器缓存

```
        [ResponseCache(Duration = 20)] // 缓存时间设置
        [HttpGet]
        public DateTime Now()
        {
            return DateTime.Now;
        }
```

## 服务器缓存 

	1. 如果安装了 “响应缓存中间件" ，那么Asp .Net Core不仅会根据[ResponseCache]设置来生成cache-control 响应报文头来设置客户端缓存，而且服务器端也会按照[ResponseCache]的设置对响应进行服务器端缓存。
 	2. “响应缓存中间件" 的好处：对于来自不同客户端的相同请求或者不支持客户端缓存的客户端，能降低服务器的压力。
 	3. 用法： app.MapControllers() 之前加上app.UseResponseCaching()。确保app.UseCors()写到app.UseResponseCaching之前。

### 服务器端响应缓存很鸡肋

1. 无法解决恶意请求给服务器带来的压力
2. 服务器端响应缓存还有很多限制，包括但不限于：响应状态码为200的GET或者HEAD响应才可能被缓存；报文头中不能含有Authorization、set-cookie等。
3. 怎么办？采用内存缓存、分布式缓存等。



## 内存缓存

	1. 把缓存数据放到应用程序的内存。内存缓存中保存的是一系列的键值对。
 	2. 网站重启后，内存缓存中的所有数据清空。

### 内存缓存用法

 1. 启用： 

    ```
    builder.Services.AddMemoryCache()
    ```

	2.  注入

    ```
            private readonly IMemoryCache memoryCache;
            var result = this.memoryCache.GetOrCreate("person" + id, (e) =>
                {
                    Console.WriteLine("缓存里未找到...,正在从数据库获取");
                    return GetPerson(id);
                });
    ```

### 缓存的过期时间

	1. 重启服务器
 	2. 解决方法：在数据改变的时候调用Remove或者Set来删除或者修改缓存（优点：及时）；过期时间（只要过期时间比较短，缓存数据不一致的情况不会持续很长时间）
 	3. 两种过期时间策略：绝对过期时间、滑动过期时间。

### 缓存的绝对过期时间

1. 设置缓存过期时间

   ```
   ICacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);
   ```

### 缓存的滑动过期时间

 1. 在过期时间内重新访问后，过期时间从新设置

    ```
    ICacheEntry.SlidingExpiration = TimeSpan.FromSeconds(10);
    ```



## 缓存穿透

1. 由于被查询的数据不存在与数据库中，缓存中为null，每次都会导致从数据库查询。
2. 解决办法：使用GetOrCreate（）方法，会将null视为合法值。



## 缓存雪崩

1. 缓存项集中过期引起缓存雪崩。

2. 解决方法：在基础过期时间之上，再加一个随机的过期时间。

   ```
   cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(Random.Shared.Next(10, 15))
   ```



## 分布式缓存

	1.  常用的分布式缓存服务器有Redis、Memcached等。
 	2.  .Net Core 提供了统一的分布式缓存服务器的操作接口IDistributedCache，用法和内存缓存类似。
 	3.  分布式缓存和内存缓存的区别：缓存值的类型为byte[]，需要进行类型转换，也提供了一些按照string类型存取缓存值的扩展方法。

### 分布式缓存用法

 1. NuGet 安装 Microsoft.Extensions.Caching.StackExchangeRedis

 2. ```
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = "localhost";
        options.InstanceName = "wds";
    });
    ```

	3.  用时间显示测试用法。









