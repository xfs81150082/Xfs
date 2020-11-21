using Xfs;
using System;
using System.Collections.Generic;

namespace XfsDbServer
{
     class XfsUserMysql : XfsComponent
    {
        //public void OnTransferParameter(object sender, XfsParameter parameter)
        //{
        //    ElevenCode elevenCode = parameter.ElevenCode;
        //    switch (elevenCode)
        //    {
        //        case (ElevenCode.Code0001):
        //            //GetTmUserByName(sender, parameter);
        //            break;
        //        case (ElevenCode.Code0002):
        //            break;
        //        default:
        //            break;
        //    }
        //}
        private string SoulName = "Users";
        //private void GetTmUserByName(object sender, XfsParameter parameter)
        //{
        //    string name = XfsParameterTool.GetValue<string>(parameter, "Username");
        //    XfsUser user2 = GetUserByUserName(name);
        //    if (user2 != null)
        //    {
        //        //(sender as TmUserHandler).User = user2;
        //    }
        //}
        //#region  ///取得user，插入，删除，更新
        //private XfsUser GetUserByUserName(string username)
        //{
        //    MySqlCommand mySqlCommand = new MySqlCommand("select * from " + SoulName + " where username = '" + username + "'", XfsMysqlConnection.Connection);//读取数据函数  
        //    MySqlDataReader reader = mySqlCommand.ExecuteReader();
        //    try
        //    {
        //        XfsUser item = new XfsUser();
        //        while (reader.Read())
        //        {
        //            if (reader.HasRows)
        //            {
        //                item.Id = reader.GetInt32(0);
        //                item.Username = reader.GetString(1);
        //                item.Password = reader.GetString(2);
        //                item.Serverid = reader.GetInt32(3);
        //                item.Phone = reader.GetString(4);
        //                item.Qq = reader.GetString(5);
        //                item.LoginCount = reader.GetInt32(6);
        //                item.LoginDateTime = reader.GetString(7);
        //                item.RigisterDateTime = reader.GetString(8);
        //            }
        //        }
        //        return item;
        //    }
        //    catch (Exception)
        //    {
        //        Console.WriteLine("查询失败...");
        //        return null;
        //    }
        //    finally
        //    {
        //        reader.Close();
        //    }
        //}     //得到user用户  //读取表格
        //private void InsertItemdb(string name, int soulId, int userid, int exp, int level, int hp, int mp, int coin, int diamond, int senceId, double px, double py, double pz, double ax, double ay, double az, int serverid)
        //{
        //    MySqlCommand mySqlCommand = new MySqlCommand("insert into " + SoulName + "(name,soulId,userid,exp,level,hp,mp,coin,diamond,senceId,px,py,pz,ax,ay,az,serverid) values('" + name + "','" + soulId + "','" + userid + "','" + exp + "','" + level + "','" + hp + "','" + mp + "','" + coin + "','" + diamond + "','" + senceId + "','" + px + "','" + py + "','" + pz + "','" + ax + "','" + ay + "','" + az + "','" + serverid + "')", XfsMysqlConnection.Connection);  //插入列表行
        //    try
        //    {
        //        mySqlCommand.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        string message = ex.Message;
        //        Console.WriteLine("插入数据失败..." + message);
        //    }
        //}
        //private void RemoveItemdb(int id)
        //{
        //    MySqlCommand mySqlCommand = new MySqlCommand("delete from " + SoulName + " where id = '" + id + "'", XfsMysqlConnection.Connection); //插入用户  
        //    try
        //    {
        //        mySqlCommand.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        string message = ex.Message;
        //        Console.WriteLine("删除数据失败..." + message);
        //    }
        //}
        //private void UpdateItemdb(int id, int exp, int level, int hp, int mp, int coin, int diamond)
        //{
        //    MySqlCommand mySqlCommand = new MySqlCommand("update " + SoulName + " set exp = '" + exp + "', level = '" + level + "', hp = '" + hp + "', mp = '" + mp + "', coin = '" + coin + "', diamond = '" + diamond + "' where id = '" + id + "'", XfsMysqlConnection.Connection); //更新列表行
        //    try
        //    {
        //        mySqlCommand.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        string message = ex.Message;
        //        Console.WriteLine("修改数据失败..." + message);
        //    }
        //}
        //#endregion
    }
}