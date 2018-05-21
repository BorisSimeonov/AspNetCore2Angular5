using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TestMakerFreeWebApp.Domain.DomainModels;

namespace TestMakerFreeWebApp.Data
{
    public class TestMakerFreeDbContext : DbContext
    {
        public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<Quiz> Quizzes { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Result> Results { get; set; }

        public TestMakerFreeDbContext(DbContextOptions<TestMakerFreeDbContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ApplyEntityConfigurations(modelBuilder);
        }

        private void ApplyEntityConfigurations(ModelBuilder builder)
        {
            var method = typeof(ModelBuilder).GetMethod("ApplyConfiguration", BindingFlags.Instance | BindingFlags.Public);

            foreach (var type in Assembly.GetExecutingAssembly().GetTypes()
                .Where(c => c.IsClass && !c.IsAbstract && !c.ContainsGenericParameters))
            {
                foreach (var iface in type.GetInterfaces())
                {
                    if (iface.IsConstructedGenericType && iface.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                    {
                        var applyConcreteMethod = method.MakeGenericMethod(iface.GenericTypeArguments.First());
                        applyConcreteMethod.Invoke(builder, new object[] { Activator.CreateInstance(type) });
                        break;
                    }
                }
            }
        }
    }
}
