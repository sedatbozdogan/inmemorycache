using CachingExample.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMemoryCache();//in-memory cache kullan?ca??m?z? bildiriyoruz.
builder.Services.AddControllers();
builder.Services.AddHttpClient(); 
builder.Services.AddScoped<IUserService, UserService>(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
