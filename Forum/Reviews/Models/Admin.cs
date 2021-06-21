using System;

namespace Reviews.Models
{
    public class Admin : Document
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}