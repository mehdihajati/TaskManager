using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Application.Common
{
    public class CacheKeys
    {
        public static string Project(Guid projectId) => $"project : {projectId}";
    }
}