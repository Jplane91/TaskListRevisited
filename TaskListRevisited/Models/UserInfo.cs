using System;
using System.Collections.Generic;

namespace TaskListRevisited.Models
{
    public partial class UserInfo
    {
        public UserInfo()
        {
            TaskInfo = new HashSet<TaskInfo>();
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string UserPassword { get; set; }

        public virtual ICollection<TaskInfo> TaskInfo { get; set; }
    }
}
