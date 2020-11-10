using System;
using System.Collections.Generic;
using System.Data;
using Xfs;

namespace XfsDbServer
{
    public class XfsMysqlComponentUpdateSystem : XfsUpdateSystem<XfsMysqlComponent>
    {     
        public override void Update(XfsMysqlComponent self)
        {
            StartConnectToMysql();
        }
        ///连接到数据库
        public void StartConnectToMysql()
        {
            //if (!IsConnecting || Connection == null || Connection.State.ToString() != "Open")
            //{
            //    //try
            //    //{
            //    //    string connectionString = string.Format("Server = {0}; Database = {1}; User ID = {2}; Password = {3};", localhost, database, root, password);                  
            //    //    Console.WriteLine(XfsTimerTool.CurrentTime() + " 连接MySql数据库成功,版本号:{0},地址:{1} ", connectionString, localhost);                
            //    //    Connection = new MySqlConnection(connectionString);
            //    //    Connection.Open();
            //    //    IsConnecting = true;
            //    //    Console.WriteLine(TmTimerTool.CurrentTime() + " 连接MySql数据库成功,版本号:{0},地址:{1} ", Connection.ServerVersion, localhost);
            //    //}
            //    //catch (Exception ex)
            //    //{
            //    //    IsConnecting = false;
            //    //    Console.WriteLine(TmTimerTool.CurrentTime() + " 连接MySql数据库,异常:{0} ", ex.Message);
            //    //}
            //    //Console.WriteLine(TmTimerTool.CurrentTime() + " IsConnecting:" + IsConnecting + " State:" + Connection.State);

            //}
        }
        ///退出数据库
        public void QuitMysql()
        {
            //Connection.Close();
            //Connection = null;
        }               
        // MySQL Query    
        public void DoQuery(string sqlQuery)
        {
            //IDbCommand dbCommand = Connection.CreateCommand();
            //dbCommand.CommandText = sqlQuery;
            //IDataReader reader = dbCommand.ExecuteReader();
            //reader.Close();
            //reader = null;
            //dbCommand.Dispose();
            //dbCommand = null;
        }
        // Get DataSet    
        public DataSet GetDataSet(string sqlString)
        {
            DataSet ds = new DataSet();
            try
            {
                //MySqlDataAdapter da = new MySqlDataAdapter(sqlString, Connection);
                //da.Fill(ds);
            }
            catch (Exception ee)
            {
                throw new Exception("SQL:" + sqlString + "\n" + ee.Message.ToString());
            }
            return ds;
        }
    }
}
