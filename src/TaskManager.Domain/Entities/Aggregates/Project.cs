using TaskManager.Domain.Common;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities.Aggregates
{
    public class Project:BaseAuditableEntity
    {
        public string Name { get;private set; }
        public string Description { get;private set; }
        public ProjectStatus Status { get;private set; }
        public DateTimeOffset? Deadline { get; private set; }
        public Guid OwnerId { get; private set; }
        // collections pattern
        //internal need
        private readonly List<ProjectMember> _members = new();
        //external need
        public IReadOnlyCollection<ProjectMember> Members => _members.AsReadOnly();
        private Project() { }
        protected Project(string name, string description, Guid ownerId, DateTimeOffset? deadline)
        {
            Name = name;
            Description = description;
            OwnerId = ownerId;
            Deadline = deadline;
            Status = ProjectStatus.Planning; 
            SetCreatedAt();
        }
        public static Project CreateProject(string name,string description,Guid ownerId, DateTimeOffset? deadline)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("name cannot be empty");
            if (ownerId == Guid.Empty)
                throw new ArgumentException("ownerid is invalid");
            var project = new Project(name,description,ownerId,deadline);
            //automatically add creator as an owner to project
            var owner =ProjectMember.Create(project.Id,ownerId,ProjectRole.Owner) ;
            project._members.Add(owner);
            return project;
        }
        public void AddMember(Guid userId, ProjectRole role, ProjectRole requesterRole)
        {
            if (requesterRole != ProjectRole.Owner && requesterRole != ProjectRole.Manager)
                throw new InvalidOperationException("You dont have a permission to add members to this project");
            if (_members.Any(m=>m.UserId ==userId &&!m.IsDeleted))
                throw new InvalidOperationException("User is already a member");
            if (role == ProjectRole.Owner)
                throw new InvalidOperationException("Project can only have one Owner");
            var member =ProjectMember.Create(Id,userId,role);
            _members.Add(member);
            SetUpdatedAt();

        }
        public void RemoveMember(Guid userId, ProjectRole requesterRole)
        {
            var member = _members.FirstOrDefault(m=>m.UserId== userId &&!m.IsDeleted);
            if (requesterRole != ProjectRole.Owner && requesterRole != ProjectRole.Manager)
                throw new InvalidOperationException("You dont have a permission to Remove members from this project");
            if (member is null)
                throw new InvalidOperationException("User is not a member of this project");
            if (member.Role == ProjectRole.Owner)
                throw new InvalidOperationException("Owner cannot be removed");
            member.Remove(requesterRole);
            SetUpdatedAt();
        }
        public void ChangeStatus(ProjectRole requesterRole , ProjectStatus newStatus) 
        {
            if (requesterRole != ProjectRole.Owner && requesterRole != ProjectRole.Manager)
                throw new InvalidOperationException("You don't have permission to change project status");
            if (Status == newStatus)
                throw new InvalidOperationException("Project Already has this status");
            if (IsDeleted)
                throw new InvalidOperationException("Project is deleted");
            Status = newStatus;
            SetUpdatedAt();

        }
        public void ArchiveProject(ProjectRole requesterRole) 
        {
            if (requesterRole != ProjectRole.Owner )
                throw new InvalidOperationException("You don't have permission to Archive project");
            if (IsDeleted)
                throw new InvalidOperationException("Project Already has been deleted");
            SetDeleted();
            SetUpdatedAt();
        }

    }
}