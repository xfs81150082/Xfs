using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public class XfsMysqlSystem : XfsSystem
    {
        public override void XfsAwake()
        {
            base.XfsAwake();
            this.ValTime = 4000;
            this.AddComponent(new XfsMysql());
        }      
        public override void XfsUpdate()
        {
            foreach (XfsEntity entity in GetTmEntities())
            {
                ConnectToMysql(entity);
            }
        }
        ///连接到数据库
        public void ConnectToMysql(XfsEntity entity)
        {
            XfsMysql mysql = entity.GetComponent<XfsMysql>();
            if (!mysql.IsConnecting || mysql.Connection == null || mysql.Connection.State.ToString() != "Open")
            {
                try
                {
                    string connectionString = string.Format("Server = {0}; Database = {1}; User ID = {2}; Password = {3};", mysql.Localhost, mysql.Database, mysql.Root, mysql.Password);
                    Console.WriteLine(XfsTimerTool.CurrentTime() + " 连接MySql数据库成功,版本号:{0},地址:{1} ", connectionString, mysql.Localhost);
                    mysql.Connection = new MySqlConnection(connectionString);
                    mysql.Connection.Open();
                    mysql.IsConnecting = true;
                    Console.WriteLine(XfsTimerTool.CurrentTime() + " 连接MySql数据库成功,版本号:{0},地址:{1} ", mysql.Connection.ServerVersion, mysql.Localhost);
                }
                catch (Exception ex)
                {
                    mysql.IsConnecting = false;
                    Console.WriteLine(XfsTimerTool.CurrentTime() + " 连接MySql数据库,异常:{0} ", ex.Message);
                }
                Console.WriteLine(XfsTimerTool.CurrentTime() + " IsConnecting:" + mysql.IsConnecting + " State:" + mysql.Connection.State);
            }
        }
        


    }
}