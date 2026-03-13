using Auth_Exam.Infrastructure.Data;
using Auth_Exam.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Auth_Exam.Api.Extensions
{
    public static class SeedDataExtension
    {
        public static async Task SeedDatabaseAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                await context.Database.EnsureCreatedAsync();

                string[] roles = { "Admin", "User" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                string testEmail = "test@example.com";
                var existingUser = await userManager.FindByEmailAsync(testEmail);

                if (existingUser == null)
                {
                    var testUser = new User
                    {
                        UserName = "testuser",
                        Email = testEmail,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(testUser, "Test123!");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(testUser, "User");
                    }
                }


                if (!context.Products.Any())
                {
                    var products = new List<Product>
                    {
                        new Product
                        {
                            Name = "Laptop",
                            Description = "High-performance laptop for professionals",
                            Price = 1299.99m
                        },
                        new Product
                        {
                            Name = "Keyboard",
                            Description = "Mechanical RGB keyboard",
                            Price = 129.99m
                        },
                        new Product
                        {
                            Name = "Mouse",
                            Description = "Wireless mouse with precision tracking",
                            Price = 49.99m
                        },
                        new Product
                        {
                            Name = "Monitor",
                            Description = "4K Ultra HD monitor 32 inch",
                            Price = 499.99m
                        },
                        new Product
                        {
                            Name = "Headphones",
                            Description = "Noise-cancelling wireless headphones",
                            Price = 199.99m
                        },
                        new Product
                        {
                            Name = "Webcam",
                            Description = "1080p HD webcam with microphone",
                            Price = 79.99m
                        },
                        new Product
                        {
                            Name = "USB Hub",
                            Description = "7-port USB 3.0 hub",
                            Price = 29.99m
                        },
                        new Product
                        {
                            Name = "Laptop Stand",
                            Description = "Ergonomic aluminum laptop stand",
                            Price = 39.99m
                        }
                    };

                    context.Products.AddRange(products);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
