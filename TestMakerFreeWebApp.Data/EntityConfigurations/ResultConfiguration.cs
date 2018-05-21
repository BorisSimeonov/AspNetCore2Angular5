using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestMakerFreeWebApp.Domain.DomainModels;

namespace TestMakerFreeWebApp.Data.EntityConfigurations
{
    public class ResultConfiguration : IEntityTypeConfiguration<Result>
    {
        public void Configure(EntityTypeBuilder<Result> builder)
        {
            builder
                .ToTable("Results")
                .Property(r => r.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasOne(r => r.Quiz)
                .WithMany(q => q.Results)
                .HasForeignKey(r => r.QuizId);
        }
    }
}
