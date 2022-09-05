using webapi.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//builder.Services.AddResponseCaching();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<Calculator>();
builder.Services.AddMemoryCache();   // 内存缓存
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost";
    options.InstanceName = "wds_"; // 增加前缀，避免冲突
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseResponseCaching(); // 启用服务器端缓存

app.MapControllers();

app.Run();
