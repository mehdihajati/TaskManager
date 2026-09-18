using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Entities.Aggregates;

namespace TaskManager.Infrastructure.Persistence.Configurations;

public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.Priority).HasConversion<string>();
        builder.Property(x => x.Status).HasConversion<string>();
        builder.HasIndex(x => x.AssigneeId);
        builder.HasOne<Project>().WithMany().HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<User>().WithMany().HasForeignKey(x => x.AssigneeId).OnDelete(DeleteBehavior.SetNull);
    }
}