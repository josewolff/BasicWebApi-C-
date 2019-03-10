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
            using (UsersDb db = new UsersDb())
            {
                return db.Users.ToList();
            }
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public Object Get(int id)
        {
            using (UsersDb db = new UsersDb())
            {
               
                Users result = db.Users.FirstOrDefault(User => User.Id == id);
                if(result == null)
                {
                    String notFoundMessage = "{\"status\":0,\"Message\":\"The id " + id + " not exists.\"}";
                    return Content(notFoundMessage, "application/json");
                }
                return result;
            }
        }

        // POST api/users
        [HttpPost]
        public Object Post([FromBody] Newtonsoft.Json.Linq.JObject value)
        {
            Users posted = value.ToObject<Users>();
            String postResult = "";
            using (UsersDb db = new UsersDb())
            {
                String userNameToPost = value.GetValue("userName").ToString();
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
        [HttpDelete("{id}")]
        public Object Delete(int id)
        {
            String deleteResult = "";
            using (UsersDb db = new UsersDb())
            {

                if (db.Users.Where(t => t.Id == id).Count() > 0)
                {
                    deleteResult = "{\"status\":1,\"Message\":\"Id: " + id + " deleted.\"}";
                    db.Users.Remove(db.Users.First(t => t.Id == id));
                    db.SaveChanges();
                }
                else
                {
                    deleteResult = "{\"status\":0,\"Message\":\"The Id: " + id + " doesnt exists.\"}";
                }

            }
            return Content(deleteResult, "application/json");
        }
    }
}
