using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ToDoList.Models;
using Task = ToDoList.Models.Task;


namespace ToDoList.Controllers
{


    [Route("api/ToDoList")]
    public class ToDoController : Controller
    {
       

        ToDoContext Context = new ToDoContext();
        
        
        [HttpPost("AddTask")]
        [Authorize]
        public IActionResult AddTask([FromBody]AddTaskDto AddTask)
        {
            if (AddTask == null)
                return new JsonResult(new List<object>()
               {
                       new { Error="no json file", Description="send a json "}
                });

            Task Tasks = new Task();
            try
            {
                Tasks.Title = AddTask.Title;
                Tasks.SubTitle = AddTask.SubTitle;
                Tasks.Status = AddTask.Status;
                User user = (from e in Context.Users
                             where e.GuserId == AddTask.GuserId
                             select e).FirstOrDefault();
                
                if (user!=null)
                {    
                    Tasks.GuserId = AddTask.GuserId;
                    Tasks.UserId = user.UserId;
                }
                else
                    return new JsonResult(new List<object>()
                {
                        new { Error="user does not exists", Description="the user for which you are entering task does not exists"}
                });
                Context.Tasks.Add(Tasks);
                Context.SaveChanges();
                return Ok("Task added");
            }
            catch (Exception e)
            {
                return new JsonResult(new List<object>()
                {
                        new { Error="Invalid Entry in Json", Description="send a json "}
                });
            }
        }

        
        [HttpDelete("Delete")]
        [Authorize]
        public IActionResult DeleteTask([FromBody]DeleteTask DeleteTask)
        {
            if (DeleteTask == null)
                return new JsonResult(new List<object>()
                {
                        new { Error="no json file", Description="send a json "}
                });

            try
            {
                var del = (from e in Context.Tasks
                           where e.GuserId == DeleteTask.GuserId && e.TaskId == DeleteTask.TaskId
                           select e).FirstOrDefault();
                Context.Tasks.Remove(del);
                Context.SaveChanges();
                return Ok("Task deleted");

            }
            catch (Exception e)
            {
                return new JsonResult(new List<object>()
                {
                    new { Error="invalid Json", Description="Json sent has invalid key value pair"}
                });
            }
        }

        
        [HttpPatch("Edit")]
        [Authorize]
        public IActionResult EditTask([FromBody]EditText EditTask)
        {
            if (EditTask == null)
                return new JsonResult(new List<object>()
                {
                        new { Error="no json file", Description="send a json "}
                });

            try
            {
                Task edit = (from e in Context.Tasks
                             where e.GuserId == EditTask.GuserId && e.TaskId == EditTask.TaskId
                             select e).FirstOrDefault();
                if (edit == null)
                    return new JsonResult(new List<object>()
                {
                        new { Error="no task to edit", Description="task is either completed or does not exists"}
                });
                edit.Title = EditTask.Title;
                edit.SubTitle = EditTask.SubTitle;
                Context.SaveChanges();
                return Ok("Task updated");

            }
            catch (Exception e)
            {
                return new JsonResult(new List<object>()
                {
                    new { Error="invalid Json", Description="Json sent has invalid key value pair"}
                });
            }
        }


       
        [HttpPost("DisplayTask")]
        [Authorize]
        public IActionResult DisplayTask([FromBody]DisplayTaskDto Display)
        {
            if (Display.Guserid == null)
                return new JsonResult(new List<object>()
                {
                        new { Error="invalid userid", Description="userid is wrong"}
                });

            try
            {
                var UserTasks = from e in Context.Tasks
                              where e.GuserId == Display.Guserid
                              select e;
                //var displaytask = db.Tasks.Where(m => m.UserId == UserId).ToList();
                if (UserTasks == null || UserTasks.Count() == 0)
                    return new JsonResult(new List<object>()
                {
                        new { Error="no task", Description="user has no task"}
                });

                return new JsonResult(UserTasks);

            }
            catch (Exception e)
            {
                return new JsonResult(new List<object>()
                {
                    new { Error="invalid Json", Description="Json sent has invalid key value pair"}
                });
            }
        }
    }
}
