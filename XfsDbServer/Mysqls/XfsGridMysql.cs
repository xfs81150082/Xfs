//using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsDbServer
{
    class XfsGridMysql : XfsComponent
    {
        public override void XfsAwake()
        {
            //DatabaseFormName = "gridmap";
        }
        public override void OnTransferParameter(object sender, XfsParameter parameter)
        {
            ElevenCode elevenCode = parameter.ElevenCode;
            switch (elevenCode)
            {
                case (ElevenCode.Code0001):
                    Console.WriteLine(XfsTimerTool.CurrentTime() + " TmEngineerMysql: " + elevenCode);
                    GetGridMap(this, parameter);
                    break;
                case (ElevenCode.Code0002):
                    break;
                default:
                    break;
            }
        }
        void GetGridMap(object sender, XfsParameter parameter)
        {
            //int userId = TmParameterTool.GetValue<int>(parameter, ElevenCode.UserLogin.ToString());
            //Console.WriteLine(TmTimerTool.CurrentTime() + " TmEngineerMysql,userId:" + userId);
            //List<TmSoulerDB> dbs = GetTmSoulerdbsByUserId(userId);
            //Console.WriteLine(TmTimerTool.CurrentTime() + " dbs:" + dbs.Count);
            //if (dbs.Count > 0)
            //{
            //    //(sender as TmEngineerHandler).Engineers = dbs;
            //    (sender as TmEngineerHandler).EngineerDbs.Add(userId, dbs);
            //}
            //else
            //{
            //    Console.WriteLine(TmTimerTool.CurrentTime() + " 没有角色");
            //}
        }


        //internal string DatabaseFormName { get; set; }
        //internal List<TmSoulerDB> GetTmSoulerdbs()
        //{
        //    MySqlCommand mySqlCommand = new MySqlCommand("select * from " + DatabaseFormName, XfsMysqlConnection.Connection);//读取数据函数  
        //    MySqlDataReader reader = mySqlCommand.ExecuteReader();
        //    try
        //    {
        //        List<TmSoulerDB> itemDBs = new List<TmSoulerDB>();
        //        while (reader.Read())
        //        {
        //            if (reader.HasRows)
        //            {
        //                TmSoulerDB item = new TmSoulerDB();
        //                item.Id = reader.GetInt32(0);
        //                item.Name = reader.GetString(1);
        //                item.UserId = reader.GetInt32(2);
        //                item.SoulerId = reader.GetInt32(3);
        //                item.Exp = reader.GetInt32(4);
        //                item.Level = reader.GetInt32(5);
        //                item.Hp = reader.GetInt32(6);
        //                item.Mp = reader.GetInt32(7);
        //                item.Coin = reader.GetInt32(8);
        //                item.Diamond = reader.GetInt32(9);
        //                item.SenceId = reader.GetInt32(10);
        //                item.State = reader.GetInt32(11);
        //                item.px = reader.GetDouble(12);
        //                item.py = reader.GetDouble(13);
        //                item.pz = reader.GetDouble(14);
        //                item.ax = reader.GetDouble(15);
        //                item.ay = reader.GetDouble(16);
        //                item.az = reader.GetDouble(17);
        //                item.ServerId = reader.GetInt32(18);
        //                item.CreateDate = reader.GetString(19);
        //                //Console.WriteLine(userId + " " + reader.FieldCount + " " + item.Id);
        //                itemDBs.Add(item);
        //            }
        //        }
        //        return itemDBs;
        //    }
        //    catch (Exception)
        //    {
        //        Console.WriteLine("查询失败...168");
        //        return null;
        //    }
        //    finally
        //    {
        //        reader.Close();
        //    }
        //}                          //读取表格//得到所有角色列表         
        //internal List<TmSoulerDB> GetTmSoulerdbsByUserId(int userId)
        //{
        //    MySqlCommand mySqlCommand = new MySqlCommand("select * from " + DatabaseFormName + " where userid = '" + userId + "'", XfsMysqlConnection.Connection);//读取数据函数  
        //    MySqlDataReader reader = mySqlCommand.ExecuteReader();
        //    try
        //    {
        //        List<TmSoulerDB> itemDBs = new List<TmSoulerDB>();
        //        while (reader.Read())
        //        {
        //            if (reader.HasRows)
        //            {
        //                TmSoulerDB item = new TmSoulerDB();
        //                item.Id = reader.GetInt32(0);
        //                item.Name = reader.GetString(1);
        //                item.UserId = reader.GetInt32(2);
        //                item.SoulerId = reader.GetInt32(3);
        //                item.Exp = reader.GetInt32(4);
        //                item.Level = reader.GetInt32(5);
        //                item.Hp = reader.GetInt32(6);
        //                item.Mp = reader.GetInt32(7);
        //                item.Coin = reader.GetInt32(8);
        //                item.Diamond = reader.GetInt32(9);
        //                item.SenceId = reader.GetInt32(10);
        //                item.State = reader.GetInt32(11);
        //                item.px = reader.GetDouble(12);
        //                item.py = reader.GetDouble(13);
        //                item.pz = reader.GetDouble(14);
        //                item.ax = reader.GetDouble(15);
        //                item.ay = reader.GetDouble(16);
        //                item.az = reader.GetDouble(17);
        //                item.ServerId = reader.GetInt32(18);
        //                item.CreateDate = reader.GetString(19);
        //                //Console.WriteLine(userId + " " + reader.FieldCount + " " + item.Id);
        //                itemDBs.Add(item);
        //            }
        //        }
        //        return itemDBs;
        //    }
        //    catch (Exception)
        //    {
        //        Console.WriteLine("查询失败...168");
        //        return null;
        //    }
        //    finally
        //    {
        //        reader.Close();
        //    }
        //}        //读取表格//得到userid所有角色列表         
        //internal TmSoulerDB GetTmSoulerdbById(int id)
        //{
        //    MySqlCommand mySqlCommand = new MySqlCommand("select * from " + DatabaseFormName + " where id = '" + id + "'", XfsMysqlConnection.Connection);//读取数据函数  
        //    MySqlDataReader reader = mySqlCommand.ExecuteReader();
        //    try
        //    {
        //        TmSoulerDB item = new TmSoulerDB();
        //        while (reader.Read())
        //        {
        //            if (reader.HasRows)
        //            {
        //                item.Id = reader.GetInt32(0);
        //                item.Name = reader.GetString(1);
        //                item.UserId = reader.GetInt32(2);
        //                item.SoulerId = reader.GetInt32(3);
        //                item.Exp = reader.GetInt32(4);
        //                item.Level = reader.GetInt32(5);
        //                item.Hp = reader.GetInt32(6);
        //                item.Mp = reader.GetInt32(7);
        //                item.Coin = reader.GetInt32(8);
        //                item.Diamond = reader.GetInt32(9);
        //                item.SenceId = reader.GetInt32(10);
        //                item.State = reader.GetInt32(11);
        //                item.px = reader.GetDouble(12);
        //                item.py = reader.GetDouble(13);
        //                item.pz = reader.GetDouble(14);
        //                item.ax = reader.GetDouble(15);
        //                item.ay = reader.GetDouble(16);
        //                item.az = reader.GetDouble(17);
        //                item.ServerId = reader.GetInt32(18);
        //                item.CreateDate = reader.GetString(19);
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
        //}                       //读取表格//得到id单个角色列表
        //internal Dictionary<int, TmSouler> GetTmSoulers()
        //{
        //    MySqlCommand mySqlCommand = new MySqlCommand("select * from " + "souler", XfsMysqlConnection.Connection);//读取数据函数  
        //    MySqlDataReader reader = mySqlCommand.ExecuteReader();
        //    try
        //    {
        //        Dictionary<int, TmSouler> dict = new Dictionary<int, TmSouler>();
        //        while (reader.Read())
        //        {
        //            if (reader.HasRows)
        //            {
        //                TmSouler item = new TmSouler();
        //                item.Id = reader.GetInt32(0);
        //                item.Name = reader.GetString(1);
        //                item.Icon = reader.GetString(2);
        //                item.AvatarName = reader.GetString(3);
        //                item.Chater = reader.GetString(4);
        //                item.LevelUpLimit = reader.GetInt32(5);
        //                item.Does = reader.GetString(6);
        //                item.InfoType = (InfoType)reader.GetInt32(8);
        //                item.Quality = (Quality)reader.GetInt32(9);
        //                item.RoleType = (RoleType)reader.GetInt32(7);
        //                item.Duration = reader.GetInt32(10);
        //                item.MaxColdTime = reader.GetInt32(11);
        //                item.Stamina = reader.GetInt32(12);
        //                item.Brains = reader.GetInt32(13);
        //                item.Power = reader.GetInt32(14);
        //                item.Agility = reader.GetInt32(15);
        //                item.Sp = reader.GetDouble(16);
        //                item.Hr = reader.GetDouble(17);
        //                item.Cr = reader.GetDouble(18);
        //                item.StaminaRate = reader.GetDouble(19);
        //                item.BrainsRate = reader.GetDouble(20);
        //                item.PowerRate = reader.GetDouble(21);
        //                item.AgilityRate = reader.GetDouble(22);
        //                //Console.WriteLine(TmTimerTool.CurrentTime() + " TmSouler-186-Power:" + (item.GetComponent<TmProperty>() as TmProperty).Power);
        //                dict.Add(item.Id, item);
        //            }
        //        }
        //        return dict;
        //    }
        //    catch (Exception)
        //    {
        //        Console.WriteLine("查询失败...168");
        //        return null;
        //    }
        //    finally
        //    {
        //        reader.Close();
        //    }
        //}                          //读取表格//得到所有角色列表         
        //internal void InsertItemdb(string name, int soulId, int userid, int exp, int level, int hp, int mp, int coin, int diamond, int senceId, double px, double py, double pz, double ax, double ay, double az, int serverid)
        //{
        //    MySqlCommand mySqlCommand = new MySqlCommand("insert into " + DatabaseFormName + "(name,soulId,userid,exp,level,hp,mp,coin,diamond,senceId,px,py,pz,ax,ay,az,serverid) values('" + name + "','" + soulId + "','" + userid + "','" + exp + "','" + level + "','" + hp + "','" + mp + "','" + coin + "','" + diamond + "','" + senceId + "','" + px + "','" + py + "','" + pz + "','" + ax + "','" + ay + "','" + az + "','" + serverid + "')", XfsMysqlConnection.Connection);  //插入列表行
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
        //internal void UpdateItemdb(int id, int exp, int level, int hp, int mp, int coin, int diamond)
        //{
        //    MySqlCommand mySqlCommand = new MySqlCommand("update " + DatabaseFormName + " set exp = '" + exp + "', level = '" + level + "', hp = '" + hp + "', mp = '" + mp + "', coin = '" + coin + "', diamond = '" + diamond + "' where id = '" + id + "'", XfsMysqlConnection.Connection); //更新列表行
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
        //internal void RemoveItemdb(int id)
        //{
        //    MySqlCommand mySqlCommand = new MySqlCommand("delete from " + DatabaseFormName + " where id = '" + id + "'", XfsMysqlConnection.Connection); //插入用户  
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




    }
}
