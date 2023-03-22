using AeC.Datas.Data;
using AeC.Repositories;
using AeC.Repositories.Interfaces;
using AeC.Services;
using AeC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AeCAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AeCAPIContext") ?? throw new InvalidOperationException("Connection string 'AeCAPIContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddSingleton<IMemoryCache, MemoryCache>(sp => new MemoryCache(new MemoryCacheOptions()));
builder.Services.AddSingleton<RestClient>(sp => new RestClient(builder.Configuration["BaseUrl"]));

builder.Services.AddTransient(typeof(IService<>), typeof(ServiceBase<>));
builder.Services.AddTransient(typeof(IRepository<>), typeof(RepositoryBase<>));

builder.Services.AddTransient<IPesquisaService, PesquisaService>();
builder.Services.AddTransient<PesquisaRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
