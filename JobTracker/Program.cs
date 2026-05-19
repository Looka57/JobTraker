using AutoMapper;
using JobTracker.Application.Interfaces;
using JobTracker.Application.Mappings;
using JobTracker.Application.Services;
using JobTracker.Domain.Interfaces;
using JobTracker.Infrastructure.Data;
using JobTracker.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. La base de données
builder.Services.AddDbContext<JobTrackDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Les Repository
    //**a. Le generique
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    //**b. Le specifique
builder.Services.AddScoped<ICandidatureRepository, CandidatureRepository>();

// 3. Les services
builder.Services.AddScoped<ICandidatureService, CandidatureService>();

//4. AutoMapper
builder.Services.AddAutoMapper(typeof(CandidatureProfile));


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