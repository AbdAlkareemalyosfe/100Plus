using Base;
using Google.Api;
using IRepostry.IRepo;
using Microsoft.EntityFrameworkCore;
using Repostry.NotificationService;
using Repostry.Repo;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<HundredPlusDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(HundredPlusDbContext).Assembly.FullName))
    );

builder.Services.AddScoped<IRepoProduct, RepoProduct>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<IRepoOffer, RepoOffer>();
builder.Services.AddScoped<IRepoCategory, RepoCategory>();
builder.Services.AddScoped<IRepoOrder, RepoOrder>();
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
