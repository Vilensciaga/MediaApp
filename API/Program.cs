using Database.Data;
using Database.Interface;
using Database.SeedingData;
using MediaApp.Middleware;
using Extensions.AppExtensions;
using Microsoft.EntityFrameworkCore;
using F23.Kernel;
using UseCases.MemberUsecases.GetMember;
using UseCases.Member.GetMembers;


var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();


//Services from my extension class library for a simpler program file
builder.Services.AddApplicationServices(config);
builder.Services.AddIdentityServices(config);
builder.Services.AddSwaggerIdentityService();
builder.Services.CorsServices();
builder.Services.AddRegisterQueryServices();




var app = builder.Build();

/*
 * using method to create the database from migrations on file
 * call Seed static method in database/seedindData 
 * logs error on failure
 */
await app.SeedDatabase();


app.UseMiddleware<ExceptionMiddleware>();

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
