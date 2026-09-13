using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Entities.Aggregates;

namespace TaskManager.Infrastructure.Persistence.Configurations;

public class ProjectMemberConfiguration : IEntityTypeConfiguration<ProjectMember>

{
    public void Configure(EntityTypeBuilder<ProjectMember> builder)
    {
        builder.HasOne<User>().WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        builder.Property(x => x.Role).HasConversion<string>();
        builder.HasIndex(x => new { x.UserId, x.ProjectId }).IsUnique();

    }
}