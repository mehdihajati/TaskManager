using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities.Aggregates;

namespace TaskManager.Infrastructure.Persistence.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(80);
        builder.Property(x => x.Description).HasMaxLength(1000);
        builder.HasMany(x => x.Members)
                .WithOne()
                .HasForeignKey(x => x.ProjectId);
        builder.Metadata.FindNavigation(nameof(Project.Members))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}