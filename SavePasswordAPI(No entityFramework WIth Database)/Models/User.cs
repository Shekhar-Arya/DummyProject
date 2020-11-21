using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SavePasswordAPI_No_entityFramework_WIth_Database_.Models
{
    public class User
    {
        public int id { get; set; }
        [DisplayName("Website")]
        public string website { get; set; }
        [DisplayName("User Name")]
        public string username { get; set; }
        [DisplayName("Password")]
        public string password { get; set; }
    }
}