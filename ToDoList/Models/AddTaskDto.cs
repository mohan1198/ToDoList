using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    public class AddTaskDto
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public Boolean Status { get; set; }
        public Guid GuserId { get; set; }
    }
}
