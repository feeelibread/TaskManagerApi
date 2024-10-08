using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerApi.Models
{
    public class TaskManager
    {
        public int Id { get; set; }
        public required string TaskName { get; set; }

        public TaskEnum TaskStatus { get; set; }
        public DateTime TaskDate { get; set; }



    }
}