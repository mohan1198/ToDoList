using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    public class User
    {   
        [Key]
        public int UserId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required ]
        [MinLength(6)]
        public string Password { get; set; }
        public DateTime date { get; set; }

        public Guid GuserId { get; set; }
        public Byte[] key { get; set; }
        public Byte[] iv { get; set; }
        public virtual ICollection<Task> Task { get; set; }

    }
}
