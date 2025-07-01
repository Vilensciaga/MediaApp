using Database.Data;
using Database.Interface;
using Database.SeedingData;
using Extensions.AppExtensions;
using MediaApp.Middleware;
using Microsoft.EntityFrameworkCore;


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



var app = builder.Build();

/*
 * using method to create the database from migrations on file
 * call Seed static method in database/seedindData 
 * logs error on failure
 */

await using (var scope = app.Services.CreateAsyncScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<IAppDbContext>();
        //applies pending migrations or create database if it dont exixt
        await context.Database.MigrateAsync();
        await Seed.SeedUsers(context);       
    }
    catch (Exception ex)
    {
        // handle errors or log them
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error during DB seeding or migration");

    }
}


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
