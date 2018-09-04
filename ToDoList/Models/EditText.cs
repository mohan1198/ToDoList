using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    public class EditText
    {
        public int TaskId { get; set; }
        public Guid GuserId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
    }
}
