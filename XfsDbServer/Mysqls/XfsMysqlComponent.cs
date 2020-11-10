using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsDbServer
{
    public class XfsMysqlComponent : XfsComponent
    {
        public  MySqlConnection Connection { get; set; }             //创建一个数据库连接                                                     
        private string localhost = "127.0.0.1";                       //IP地址
        private string database = "tumoworld";                        //数据库名    
        private string root = "root";                                 //用户名  
        private string password = "";                                 //密码  
        public bool IsConnecting { get; private set; } = false;
        ///连接到数据库  


    }
}
