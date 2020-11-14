using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public class XfsMysql : XfsComponent
    {
        public bool IsConnecting { get; set; } = false;
        public MySqlConnection Connection { get; set; }              //创建一个数据库连接                                                     
        public string Localhost { get; set; } = "127.0.0.1";                      //IP地址
        public string Database { get; set; } = "tumoworld";                       //数据库名    
        public string Root { get; set; } = "root";                                //用户名  
        public string Password { get; set; } = "";                                //密码  
        public XfsMysql() { }
        public XfsMysql(string localhost, string database, string root, string password)
        {
            Localhost = localhost;
            Database = database;
            Root = root;
            Password = password;
        }

    }
}