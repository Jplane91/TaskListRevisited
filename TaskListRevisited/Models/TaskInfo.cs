using System;
using System.Collections.Generic;

namespace TaskListRevisited.Models
{
    public partial class TaskInfo
    {
        public int TaskNumber { get; set; }
        public string TaskDesc { get; set; }
        public DateTime TaskDueDate { get; set; }
        public string TaskStatus { get; set; }
        public int? UserId { get; set; }

        public virtual UserInfo User { get; set; }
    }
}
