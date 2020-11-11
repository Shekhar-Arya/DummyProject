using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Collections;

namespace MVCforPasswordSaver.Models
{
    public class User : IEnumerable<User>
    {
        private List<User> user = new List<User>();
        public IEnumerator<User> GetEnumerator()
        {
            return user.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        [DisplayName("Name")]
        public string name { get; set; }
        public List<UserPassword> passwordSaver = new List<UserPassword>();
        public User()
        {
        }
    }
}