var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// 需要取出應用系統組態配置(連接字串)
ConfigurationManager manager = builder.Configuration;

// 將自訂物件封裝 注入到特定物件 taipei
IConfiguration section = manager.GetSection("taipei");
UbikeService.Models.TaipeiResource taipei = new UbikeService.Models.TaipeiResource();
section.Bind(taipei);
// 將 taipei 物件加入為服務 Singleton Design Pattern
builder.Services.AddSingleton(taipei.GetType(), taipei);

//取出允許前端 Domain 屬性
string[] corsURL = manager.GetSection("CORS").Get<string[]>();
//自訂CORS政策服務，允許前端服務可以過來
var ubikeWeb = "_ubikeWeb";
builder.Services.AddCors(
    options =>
    {
        options.AddPolicy(name: ubikeWeb,
            policy =>
            {
                policy.WithOrigins("https://localhost:7175");
            });
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(ubikeWeb);

app.MapControllers();

app.Run();
