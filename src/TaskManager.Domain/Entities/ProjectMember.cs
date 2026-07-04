using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Domain.Common;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities
{
    public class ProjectMember:BaseEntity
    {
        public Guid ProjectId { get;private set; }
        public Guid UserId { get; private set; }
        public ProjectRole Role { get; private set; }
        private ProjectMember(){ } //efcore

        protected ProjectMember(Guid projectId, Guid userId, ProjectRole role)
        {
            ProjectId = projectId;
            UserId = userId;
            Role = role;
            SetCreatedAt();
        }
        public static ProjectMember Create(Guid projectId, Guid userId, ProjectRole role)
        {
            if (projectId == Guid.Empty)
                throw new ArgumentException("ProjectId is not valid");
            if (userId == Guid.Empty)
                throw new ArgumentException("UserId is not valid");
            if (!Enum.IsDefined(typeof(ProjectRole), role))
                throw new ArgumentException("Role is not valid");
            return new ProjectMember(projectId, userId, role);
        }
        public static ProjectMember Create(Guid projectId, Guid userId)
        {
            return Create(projectId, userId, ProjectRole.Member);
        }
        public void ChangeRole(ProjectRole newRole,ProjectRole requesterRole)
        {
            if (Role == ProjectRole.Owner)
                throw new InvalidOperationException("owner role can nt be changed");
            if (Role == ProjectRole.Manager && requesterRole != ProjectRole.Owner)
                throw new InvalidOperationException("Only owner can change the manager role");
            if (requesterRole != ProjectRole.Owner && requesterRole != ProjectRole.Manager)
                throw new InvalidOperationException("You dont have a permission to change roles");
            if (Role == newRole)
                throw new InvalidOperationException("member already has this role ");
            Role= newRole;
            SetUpdatedAt();
        }
        public void Remove(ProjectRole requesterRole)
        {
            if (Role == ProjectRole.Owner)
                throw new InvalidOperationException("owner role can nt be removed");
            if (Role == ProjectRole.Manager && requesterRole != ProjectRole.Owner)
                throw new InvalidOperationException("Only owner can removed the manager ");
            if (requesterRole != ProjectRole.Owner && requesterRole != ProjectRole.Manager)
                throw new InvalidOperationException("You dont have a permission to remove members");
            SetDeleted();
        }

    }
}