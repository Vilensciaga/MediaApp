using Database.Data;
using Database.Interface;
using DataService.Interface;
using DataService.Service;
using Microsoft.EntityFrameworkCore;
using Models.Dtos.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//instanciating mapping profiles
builder.Services.AddAutoMapper(typeof(UserMappingProfile));


// adding my database context and connection string
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection") , x => x.MigrationsAssembly("Database")));

//dependency injections
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAppDbContext, AppDbContext>();


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

app.MapDefaultControllerRoute();

app.MapControllers();

app.Run();
