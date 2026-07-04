using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; private set; } = Guid.CreateVersion7();
        public DateTimeOffset CreatedAt { get;private set; }
        public DateTimeOffset UpdatedAt { get;private set; }
        public bool IsDeleted { get; private set; }
        public DateTimeOffset? DeletedAt { get; private set; }
        protected void SetDeleted()
        {
            IsDeleted = true;
            DeletedAt = DateTimeOffset.UtcNow;
        }
        protected void SetCreatedAt()
        {
            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        protected void SetUpdatedAt()
        {
            UpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}