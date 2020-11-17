using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SavingPasswordForAll.Controllers
{
    public class ValuesController : ApiController
    {
        static List<User> userAndpassword = new List<User>();
        // GET api/values
        public List<User> Get()
        {
            User u = new User();
            PasswordSaver p = new PasswordSaver();
            u.name = "Shekhar";
            p.website = "www";
            p.username = "sss";
            p.password = "123";
            u.passwordSaver.Add(p);
            userAndpassword.Add(u);
            return userAndpassword;
        }

        // GET api/values/5
        public List<PasswordSaver> Get(string na)
        {
            var b = new List<PasswordSaver>();
            foreach(var a in userAndpassword)
            {
                if(a.name == na)
                {
                    b = a.passwordSaver;
                    break;
                }
            }
            return b;
        }

        // POST api/values
        public void Post([FromBody]User u)
        {
            User a = new User();
            a.name = u.name;
            userAndpassword.Add(a);
        }

        // PUT api/values/5
        public void Put(string na, string web,[FromBody] string value)
        {
            foreach(var a in userAndpassword)
            {
                if(a.name == na)
                {
                    foreach(var b in a.passwordSaver)
                    {
                        if(b.website==web)
                        {
                            b.password = value;
                        }
                    }
                }
            }
        }

        // DELETE api/values/5
        public void Delete(string na, string web)
        {
            foreach(var a in userAndpassword)
            {
                if(a.name == na)
                {
                    foreach(var b in a.passwordSaver)
                    {
                        if(b.website == web)
                        {
                            a.passwordSaver.Remove(b);
                            break;
                        }
                    }
                    break;
                }
            }
        }
    }
}
