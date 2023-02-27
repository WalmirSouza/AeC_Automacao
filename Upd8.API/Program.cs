using Upd8.Datas.Data;
using Upd8.Repositories;
using Upd8.Repositories.Interfaces;
using Upd8.Services;
using Upd8.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Upd8APIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Upd8APIContext") ?? throw new InvalidOperationException("Connection string 'Upd8APIContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMemoryCache, MemoryCache>(sp => new MemoryCache(new MemoryCacheOptions()));
builder.Services.AddSingleton<RestClient>(sp => new RestClient(builder.Configuration["BaseUrlApiIbge"]));
builder.Services.AddScoped<IApiIbgeService, ApiIbgeService>();

builder.Services.AddTransient(typeof(IService<>), typeof(ServiceBase<>));
builder.Services.AddTransient(typeof(IRepository<>), typeof(RepositoryBase<>));

builder.Services.AddTransient<IMunicipioService, MunicipioService>();
builder.Services.AddTransient<IClienteService, ClienteService>();
builder.Services.AddTransient<ClienteRepository>();

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
