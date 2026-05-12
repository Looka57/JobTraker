using JobTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using JobTracker.Domain.Interfaces;
using JobTracker.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. La base de données
builder.Services.AddDbContext<JobTrackDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    
// 2. Le Repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // installation Swashbuckle.AspNetCore

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();