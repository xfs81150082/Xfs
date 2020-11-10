using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public class XfsMysqlUpdateSystem : XfsUpdateSystem<XfsMysql>
    {
        public override void Update(XfsMysql self)
        {
            ConnectToMysql(self);
        }
        ///连接到数据库
        public void ConnectToMysql(XfsMysql self)
        {
            if (!self.IsConnecting || self.Connection == null || self.Connection.State.ToString() != "Open")
            {
                try
                {
                    string connectionString = string.Format("Server = {0}; Database = {1}; User ID = {2}; Password = {3};", self.Localhost, self.Database, self.Root, self.Password);
                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " 连接MySql数据库成功,版本号:{0},地址:{1} ", connectionString, self.Localhost);
                    self.Connection = new MySqlConnection(connectionString);
                    self.Connection.Open();
                    self.IsConnecting = true;
                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " 连接MySql数据库成功,版本号:{0},地址:{1} ", self.Connection.ServerVersion, self.Localhost);
                }
                catch (Exception ex)
                {
                    self.IsConnecting = false;
                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " 连接MySql数据库,异常:{0} ", ex.Message);
                }
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " IsConnecting:" + self.IsConnecting + " State:" + self.Connection.State);
            }
        }
        


    }
}