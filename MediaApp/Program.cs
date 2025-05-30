using Auth.Jwt;
using Database.Data;
using Database.Interface;
using DataService.Interface;
using DataService.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models.Dtos.User;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;


// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();



//Adding auth middleware
builder.Services.AddAuthentication(x => {
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["TokenKey"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});




// adding the ability to authorize in swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Chat", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "www-authenticate",
        //Type = SecuritySchemeType.ApiKey,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT with Bearer format... 'Bearer token'",
    });


    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{ }
                    }
                });
});





//instanciating mapping profiles
builder.Services.AddAutoMapper(typeof(UserMappingProfile));





// adding my database context and connection string
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection") , x => x.MigrationsAssembly("Database")));



//dependency injections
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAppDbContext, AppDbContext>();




//adding cors policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins("http://localhost:4200");
    });
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapDefaultControllerRoute();

app.MapControllers();

app.Run();
