using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=todo.db"));
builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularDevServer",
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200") // Adjust the Angular dev server URL
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAngularDevServer");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
