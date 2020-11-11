using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace SavingPasswordForAll
{
    public class User
    {
        public string name { get; set; }
        public List<PasswordSaver> passwordSaver = new List<PasswordSaver>();
    }
}