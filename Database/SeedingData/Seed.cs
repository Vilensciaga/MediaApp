using Database.Data;
using Database.Interface;
using MediaApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Database.SeedingData
{
    public class Seed
    {
        /*
         * Static class to seed data into the database
         * takes a database context
         * checks if the database User tablle is empty tand populates it if not
         * deserialize json file data into AppUser object
         * manually codes in usuer password and salt 
         * adds user
         */
        public static async Task SeedUsers(IAppDbContext context)
        {
            if (await context.Users.AnyAsync()) return;


            var path = Path.GetFullPath(
                        Path.Combine(Directory.GetCurrentDirectory(), "..", "Database", "SeedingData", "UserSeedData.json")
);

            var userData = await System.IO.File.ReadAllTextAsync(path);
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();
                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("1"));
                user.PasswordSalt = hmac.Key;

                context.Users.Add(user);
            }
            await context.SaveChangesAsync();
        }
    }
}
