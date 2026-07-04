using TaskManager.Domain.Common;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities
{
    public class TaskItem:BaseAuditableEntity
    {

        public string Title { get;private set; }
        public string Description { get;private set; }
        public TaskItemStatus Status { get;private set; }
        public Priority Priority { get;private set; }
        public DateTimeOffset? DueDate { get;private set; }
        public Guid ProjectId { get;private set; }
        public Guid? AssigneeId { get;private set; }
        private TaskItem() { }
        protected TaskItem(string title, string description, Priority proiority, DateTimeOffset? dueDate, Guid projectId, Guid? assigneeId)
        {
            Title = title;
            Description = description;
            Status = TaskItemStatus.Todo;
            Priority = proiority;
            DueDate = dueDate;
            ProjectId = projectId;
            AssigneeId = assigneeId;
            SetCreatedAt();
        }
        public static TaskItem CreateTask(string title,string description,Priority priority,Guid projectId, DateTimeOffset? dueDate = null,Guid? assigneeId = null)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty");
            if (projectId == Guid.Empty)
                throw new ArgumentException("ProjectId is not valid");
            
            return new TaskItem(title, description, priority, dueDate, projectId, assigneeId);
        }
        public void ChangeStatus (ProjectRole requesterRole, TaskItemStatus newStatus,Guid requesterId)
        {
                if (requesterRole != ProjectRole.Owner
                && requesterRole != ProjectRole.Manager
                && requesterId != AssigneeId)
                throw new InvalidOperationException("You don't have permission to change task status");
            if (IsDeleted)
                throw new InvalidOperationException("TaskItem is deleted");
            if (Status == newStatus)
                throw new InvalidOperationException("Task already has this status");
            Status = newStatus;
            SetUpdatedAt();
         }
        public void ChangePriority(ProjectRole requesterRole,Priority newPeriority)
        {
            if(requesterRole != ProjectRole.Owner && requesterRole != ProjectRole.Manager)
                throw new InvalidOperationException("You don't have permission to change task Priority");
            if (Priority == newPeriority)
                throw new InvalidOperationException("Task Already Has this priority");
            if (IsDeleted)
                throw new InvalidOperationException("Cannot change priority of a closed task");
            Priority = newPeriority;
            SetUpdatedAt();
        }
        public void Assign(Guid assigneeId, ProjectRole requesterRole)
        {
            if(assigneeId ==Guid.Empty)
                throw new InvalidOperationException("AsigneeiD cannot be empty");
            if (requesterRole != ProjectRole.Manager &&  requesterRole != ProjectRole.Owner)
                throw new InvalidOperationException("You dont have permission asign someone to tasks");
            if (IsDeleted)
                throw new InvalidOperationException("TaskItem is deleted and cant asign somebody to deleted taskitem");
            if (AssigneeId == assigneeId)
                throw new InvalidOperationException("Task already asigned to someone");
            AssigneeId = assigneeId;
            SetUpdatedAt();
        }
        public void UnAssign(ProjectRole requesterRole)
        {
            if (requesterRole != ProjectRole.Manager &&  requesterRole != ProjectRole.Owner)
                throw new InvalidOperationException("You dont have permission unasign someone to tasks");
            if (IsDeleted)
                throw new InvalidOperationException("TaskItem is closed");
            if (AssigneeId == null)
                throw new InvalidOperationException("Task Is not assing to anybody");
            AssigneeId = null;
            SetUpdatedAt();
        }
        public void Close(ProjectRole requesterRole)
        {
            if (requesterRole != ProjectRole.Manager &&  requesterRole != ProjectRole.Owner)
                throw new InvalidOperationException("You dont have permission delete tasks");
            if (IsDeleted)
                throw new InvalidOperationException("TaskItem is already deleted");
            SetDeleted();
            SetUpdatedAt();
        }
    }
}