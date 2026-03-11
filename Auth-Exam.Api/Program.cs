using Microsoft.EntityFrameworkCore;
using Auth_Exam.Infrastructure.Data;
using Auth_Exam.Api.Extensions;
using Auth_Exam.Core.Contracts;
using Auth_Exam.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddIdentity();
builder.Services.AddAuthenticationConfiguration(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddScoped<IProductService, ProductService>();
var app = builder.Build();

// Seed database
await app.SeedDatabaseAsync();

// Configure middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

