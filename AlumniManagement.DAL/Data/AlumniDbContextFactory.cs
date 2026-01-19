using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AlumniManagement.DAL.Data
{
    public class AlumniDbContextFactory : IDesignTimeDbContextFactory<AlumniDbContext>
    {
        public AlumniDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AlumniDbContext>();

            optionsBuilder.UseNpgsql(
                "Host=dpg-d5n49fcmrvns73fhcuv0-a.singapore-postgres.render.com;Port=5432;Database=alumni_db_t42a;Username=alumni_db_t42a_user;Password=L8eRei2R8LCkGkMWoEb2FeiBoIkoe2nZ;SSL Mode=Require;Trust Server Certificate=true"
            );

            return new AlumniDbContext(optionsBuilder.Options);
        }
    }
}
