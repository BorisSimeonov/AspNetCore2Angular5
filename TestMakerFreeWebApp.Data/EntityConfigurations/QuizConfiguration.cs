using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestMakerFreeWebApp.Domain.DomainModels;

namespace TestMakerFreeWebApp.Data.EntityConfigurations
{
    public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
            builder
               .ToTable("Quizzes")
               .HasOne(q => q.User)
               .WithMany(u => u.Quizzes)
               .HasForeignKey(q => q.UserId);

            builder
                .Property(q => q.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasMany(q => q.Questions)
                .WithOne(qs => qs.Quiz)
                .HasForeignKey(qs => qs.QuizId);
        }
    }
}
