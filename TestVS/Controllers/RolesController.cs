using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestVS.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestVS.Controllers
{
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        // GET api/users
        [HttpGet]
        public Object Get()
        {
            using (DbConnection db = new DbConnection())
            {
                return db.Roles.Select(x => new { x.RoleId, x.RoleName, x.Description }).Distinct().ToList();
            }
        }

        // GET api/values/5
        [Route("getUserRoles/{userName}")]
        [HttpGet("{userName}")]
        public Object Get(String userName)
        {
            using (DbConnection db = new DbConnection())
            {
                var result = db.Roles.Where(role => role.Users.UserName.Equals(userName))
                                    .Select(role => new {role.RoleId, role.RoleName, role.Description}).ToList();
                return result;
            }
        }


        // GET api/values/5
        [Route("getUsersByRole/{roleName}")]
        [HttpGet("{roleName}")]
        public Object GetUsersByRole(String roleName)
        {
            using (DbConnection db = new DbConnection())
            {
                var result = (from u in db.Users
                               join r in db.Roles
                               on u.Id equals r.Users.Id
                               where r.RoleName.ToUpper().Equals(roleName.ToUpper())
                               select u).ToList();
                          
                return result;
            }
        }



        // POST api/values
        [Route("add")]
        [HttpPost]
        public Object Post([FromBody]Newtonsoft.Json.Linq.JObject value)
        {
            Roles posted = value.ToObject<Roles>();
            String postResult = "";
            using (DbConnection db = new DbConnection())
            {
                String roleToPost = value.GetValue("RoleName").ToString();
                List<Roles> existingRole = db.Roles.Where(role => role.RoleName == roleToPost).ToList();
                if (existingRole.Count == 0)
                {
                    db.Roles.Add(posted);
                    db.SaveChanges();
                    return db.Roles.First(role => role.RoleName == roleToPost);
                }
                else
                {
                    postResult = "{\"status\":0,\"Message\":\"The role " + roleToPost + " already exists.\"}";
                }

            }
            return Content(postResult, "application/json");
        }



        // DELETE api/values/5
        [Route("remove/{rolename}")]
        [HttpDelete("{rolename}")]
        public Object Delete(String rolename)
        {
            String deleteResult = "";
            using (DbConnection db = new DbConnection())
            {

                if (db.Roles.Where(role => role.RoleName.ToUpper().Equals(rolename.ToUpper())).Count() > 0)
                {
                    deleteResult = "{\"status\":1,\"Message\":\"RoleName: " + rolename + " deleted.\"}";
                    db.Roles.RemoveRange(db.Roles.Where(role => role.RoleName.ToUpper().Equals(rolename)));
                    db.SaveChanges();
                }
                else
                {
                    deleteResult = "{\"status\":0,\"Message\":\"The RoleName: " + rolename + " doesnt exists.\"}";
                }

            }
            return Content(deleteResult, "application/json");
        }
    }
}
