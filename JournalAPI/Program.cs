using JournalAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
       builder => builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader());
});



builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<JournalDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("default")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();
