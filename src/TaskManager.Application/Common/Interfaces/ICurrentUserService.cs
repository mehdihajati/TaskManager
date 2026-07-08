using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }
        string? Email { get; }
    }
}