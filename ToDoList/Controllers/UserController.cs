using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class UserController : Controller
    {
        ToDoContext Context = new ToDoContext();
        Output Output = new Output();
        [HttpPost("Register")]
        public IActionResult RegisterUser([FromBody]RegisterDto Register)
        {
            if (Register == null)
            {
                Output.Description = "send a json";
                Output.Status = "Failure";
                return new JsonResult(Output);
            }
            try
            {
                if (Regex.Match(input: Register.Email, pattern: @".+@[A-Za-z]+.[A-Za-z]+").Success)
                {
                    if (Regex.Match(Register.Password, ".{8,}").Success)
                    {
                        User User = new User();

                        try
                        {
                            Encrypt encrypt = new Encrypt();
                            User.Email = Register.Email;
                            User.Password = encrypt.Encryption(Register.Password);
                            User.GuserId = Guid.NewGuid();
                            User.key = encrypt.GetKey();
                            User.iv = encrypt.GetIV();
                            User.date = DateTime.Now;
                            Context.Users.AddRange(User);
                            Context.SaveChanges();
                            return Ok("Thanks for Registration");
                        }
                        catch (Exception e)
                        {
                            Output.Status = "Faliure";
                            Output.Description = "Email id already exists";
                            return new JsonResult(Output);

                        }


                    }
                    else
                        return new JsonResult(new List<object>() {
                        new { Error="Invalid password", Description="password should be minimum 8 characters"}
                       });


                }
                else
                    return new JsonResult(new List<object>()
                {
                    new { Error="Invalid email",Description="entered email id is not valid. Valid email type: aaa@gmail.com"}
                });


            }
            catch (Exception e)
            {
                return new JsonResult(new List<object>()
                        {
                        new { Error="Invalid Json file", Description="all keys are not correct please send valid keys"}
                        });
            }
        }

        [HttpPost("login")]
        public IActionResult ValidateUser([FromBody]LoginDto login)
        {
            if (login == null)
                return new JsonResult(new List<object>()
                {
                        new { Error="no json file", Description="send a json "}
                });
            User user = (from e in Context.Users
                         where e.Email.Equals(login.email.Trim())
                         select e).FirstOrDefault();
            if(user==null)
            {
                return new JsonResult(new { Error = "invalid user", Description = "try to register this " });
            }
            Encrypt encrypt = new Encrypt();

            
            var PasswordCheck = encrypt.Encryption(login.password, user.key, user.iv);

            var id = from e in Context.Users
                     where e.Email.Equals(login.email.Trim()) && e.Password.Equals(PasswordCheck)
                     select e.UserId;

            if (id == null || id.Count() == 0)
                return new JsonResult(new List<object>()
                {
                        new { Error="invalid password", Description=""}
                });
            else
            {
               var Token= TokenController.GenerateToken(login.email);
                return Ok(new JsonResult(new  { AccessToken = Token, UserId = user.GuserId } ));

            }

        }
    }
}
