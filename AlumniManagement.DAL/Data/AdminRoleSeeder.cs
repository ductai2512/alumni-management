using Microsoft.EntityFrameworkCore;
using AlumniManagement.DAL.Entities;

namespace AlumniManagement.DAL.Data.Seed
{
    public static class AdminRoleSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            var admin = await context.Accounts
                .FirstOrDefaultAsync(x => x.Username == "admin");

            if (admin == null)
            {
                Console.WriteLine("Admin account not found.");
                return;
            }

            if (admin.Role != "Admin")
            {
                admin.Role = "Admin";
                await context.SaveChangesAsync();

                Console.WriteLine("Admin role updated to Admin.");
            }
            else
            {
                Console.WriteLine("Admin role already Admin.");
            }
        }
    }
}
