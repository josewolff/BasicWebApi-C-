using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestVS.Models;

namespace TestVS.Controllers
{

 

    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {

        // GET api/users
        [HttpGet]
        public ActionResult<IEnumerable<Users>> Get()
        {
            using (DbConnection db = new DbConnection())
            {
                return db.Users.ToList();
            }
        }

        // GET api/users/5
        [HttpGet("{userName}")]
        public Object Get(String userName)
        {
            using (DbConnection db = new DbConnection())
            {
               
                Users result = db.Users.FirstOrDefault(User => User.UserName.ToUpper().Equals(userName.ToUpper()));
                if(result == null)
                {
                    String notFoundMessage = "{\"status\":0,\"Message\":\"The UserName " + userName + " not exists.\"}";
                    return Content(notFoundMessage, "application/json");
                }
                return result;
            }
        }

        // POST api/users
        [Route("add")]
        [HttpPost]
        public Object Post([FromBody] Newtonsoft.Json.Linq.JObject value)
        {
            Users posted = value.ToObject<Users>();
            String postResult = "";
            using (DbConnection db = new DbConnection())
            {
                String userNameToPost = value.GetValue("UserName").ToString();
                List<Users> existingUsers = db.Users.Where(User => User.UserName == userNameToPost).ToList();
                if (existingUsers.Count == 0)
                {
                    db.Users.Add(posted);
                    db.SaveChanges();
                    return db.Users.First(User => User.UserName == userNameToPost);
                }
                else
                {
                    postResult = "{\"status\":0,\"Message\":\"The name " + userNameToPost + " already exists.\"}";
                }

            }
            return Content(postResult, "application/json");
        }


        // DELETE api/users/5
        [Route("remove/{userName}")]
        [HttpDelete("{userName}")]
        public Object Delete(String userName)
        {
            String deleteResult = "";
            using (DbConnection db = new DbConnection())
            {

                if (db.Users.Where(t => t.UserName.ToUpper().Equals(userName.ToUpper())).Count() > 0)
                {
                    deleteResult = "{\"status\":1,\"Message\":\"UserName: " + userName + " deleted.\"}";
                    db.Users.Remove(db.Users.First(t => t.UserName.ToUpper().Equals(userName.ToUpper())));
                    db.SaveChanges();
                }
                else
                {
                    deleteResult = "{\"status\":0,\"Message\":\"The UserName: " + userName + " doesnt exists.\"}";
                }

            }
            return Content(deleteResult, "application/json");
        }
    }
}
