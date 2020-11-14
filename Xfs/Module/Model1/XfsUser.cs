using System;
using System.Collections.Generic;
namespace Xfs
{
    [Serializable]
    public class XfsUser : XfsComponent
    {      
        public XfsUser() { }
        public XfsUser(string username,string password)
        {
            this.Username = username;
            this.Password = password;
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Serverid { get; set; }
        public string Phone { get; set; }
        public string Qq { get; set; }
        public string RigisterDateTime { get; set; }
        public string LoginDateTime { get; set; }
        public int LoginCount { get; set; }
             
    }
}
