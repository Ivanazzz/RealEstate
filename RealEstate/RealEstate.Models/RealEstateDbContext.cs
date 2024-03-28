using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RealEstate.Models.Entities;
using System.Reflection;

namespace RealEstate.Models
{
    public class RealEstateDbContext : DbContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Settlement> Settlements { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Estate> Estates { get; set; }

        public DbSet<Inspection> Inspections { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ApplyConfiguration(builder);
        }

        protected void ApplyConfiguration(ModelBuilder builder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetInterfaces().Any(gi =>
                    gi.IsGenericType
                    && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
                .ToList();

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                builder.ApplyConfiguration(configurationInstance);
            }
        }
    }
}
