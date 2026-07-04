using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Domain.Common
{
    public class BaseAuditableEntity:BaseEntity
    {
        public string CreatedBy { get;private set; }
        public string UpdatedBy { get; private set; }
    }
}