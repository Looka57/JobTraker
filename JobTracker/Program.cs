using AutoMapper;
using JobTracker.API.Middlewares;
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
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IInteractionRepository, InteractionRepository>();

// 3. Les services
// 3. Les services
builder.Services.AddScoped<ICandidatureService, CandidatureService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IInteractionService, InteractionService>();


//4. AutoMapper
builder.Services.AddAutoMapper(typeof(CandidatureProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // installation Swashbuckle.AspNetCore

//5. CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:3000") // Les ports par défaut de Vue/Vite
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();

app.UseCors("AllowVueApp");

//6.Middlewares
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();