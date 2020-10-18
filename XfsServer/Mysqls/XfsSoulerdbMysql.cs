using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using Xfs;

namespace XfsServer
{
     class XfsSoulerdbMysql : XfsComponent
    {
        internal string DatabaseFormName { get; set; }
        internal ArrayList TmSoulerDBs()
        {
            MySqlCommand mySqlCommand = new MySqlCommand("select * from " + DatabaseFormName, XfsMysqlConnection.Connection);//读取数据函数  
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                ArrayList itemDBs = new ArrayList();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        TmSoulerDB item = new TmSoulerDB();
                        item.Id = reader.GetInt32(0);
                        item.Name = reader.GetString(1);
                        item.UserId = reader.GetInt32(2);
                        item.SoulerId = reader.GetInt32(3);
                        item.Exp = reader.GetInt32(4);
                        item.Level = reader.GetInt32(5);
                        item.Coin = reader.GetInt32(6);
                        item.Diamond = reader.GetInt32(7);
                        item.Hp = reader.GetInt32(8);
                        item.Mp = reader.GetInt32(9);
                        item.State = reader.GetInt32(10);
                        item.CdTime = reader.GetDouble(11);
                        item.ServerId = reader.GetInt32(12);
                        item.SenceId = reader.GetInt32(13);
                        item.px = reader.GetDouble(14);
                        item.py = reader.GetDouble(15);
                        item.pz = reader.GetDouble(16);
                        item.ax = reader.GetDouble(17);
                        item.ay = reader.GetDouble(18);
                        item.az = reader.GetDouble(19);
                        item.CreateDate = reader.GetString(20);
                        itemDBs.Add(item);
                    }
                }
                return itemDBs;
            }
            catch (Exception)
            {
                Console.WriteLine("查询失败...168");
                return null;
            }
            finally
            {
                reader.Close();
            }
        }                          //读取表格//得到所有角色列表         
        internal Dictionary<int, TmSoulerDB> GetTmSoulerDBsDict()
        {
            Console.WriteLine("TmSoulerdbMysql...61" + " " + DatabaseFormName);

            MySqlCommand mySqlCommand = new MySqlCommand("select * from " + DatabaseFormName, XfsMysqlConnection.Connection);//读取数据函数  
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                Dictionary<int, TmSoulerDB> itemDBs = new Dictionary<int, TmSoulerDB>();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        TmSoulerDB item = new TmSoulerDB();
                        item.Id = reader.GetInt32(0);
                        item.Name = reader.GetString(1);
                        item.SoulerId = reader.GetInt32(2);
                        item.UserId = reader.GetInt32(3);
                        item.Exp = reader.GetInt32(4);
                        item.Level = reader.GetInt32(5);
                        item.Coin = reader.GetInt32(6);
                        item.Diamond = reader.GetInt32(7);
                        item.Hp = reader.GetInt32(8);
                        item.Mp = reader.GetInt32(9);
                        item.State = reader.GetInt32(10);
                        item.CdTime = reader.GetDouble(11);
                        item.ServerId = reader.GetInt32(12);
                        item.SenceId = reader.GetInt32(13);
                        item.px = reader.GetDouble(14);
                        item.py = reader.GetDouble(15);
                        item.pz = reader.GetDouble(16);
                        item.ax = reader.GetDouble(17);
                        item.ay = reader.GetDouble(18);
                        item.az = reader.GetDouble(19);
                        item.CreateDate = reader.GetString(20);
                        itemDBs.Add(item.Id, item);
                        Console.WriteLine("Id...93"+" "+item.Id);
                    }
                }
                return itemDBs;
            }
            catch (Exception)
            {
                Console.WriteLine("tmsouler 102:"+"查询失败...168");
                return null;
            }
            finally
            {
                reader.Close();
            }
        }                          //读取表格//得到所有角色列表         
        internal List<TmSoulerDB> GetTmSoulerDBsList()
        {
            MySqlCommand mySqlCommand = new MySqlCommand("select * from " + DatabaseFormName, XfsMysqlConnection.Connection);//读取数据函数  
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                List<TmSoulerDB> itemDBs = new List<TmSoulerDB>();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        TmSoulerDB item = new TmSoulerDB();
                        item.Id = reader.GetInt32(0);
                        item.Name = reader.GetString(1);
                        item.UserId = reader.GetInt32(2);
                        item.SoulerId = reader.GetInt32(3);
                        item.Exp = reader.GetInt32(4);
                        item.Level = reader.GetInt32(5);
                        item.Coin = reader.GetInt32(6);
                        item.Diamond = reader.GetInt32(7);
                        item.Hp = reader.GetInt32(8);
                        item.Mp = reader.GetInt32(9);
                        item.State = reader.GetInt32(10);
                        item.CdTime = reader.GetDouble(11);
                        item.ServerId = reader.GetInt32(12);
                        item.SenceId = reader.GetInt32(13);
                        item.px = reader.GetDouble(14);
                        item.py = reader.GetDouble(15);
                        item.pz = reader.GetDouble(16);
                        item.ax = reader.GetDouble(17);
                        item.ay = reader.GetDouble(18);
                        item.az = reader.GetDouble(19);
                        item.CreateDate = reader.GetString(20);
                        itemDBs.Add(item);
                    }
                }
                return itemDBs;
            }
            catch (Exception)
            {
                Console.WriteLine("查询失败...168");
                return null;
            }
            finally
            {
                reader.Close();
            }
        }                          //读取表格//得到所有角色列表         
        internal List<TmSoulerDB> GetTmSoulerdbsByUserId(int userId)
        {
            MySqlCommand mySqlCommand = new MySqlCommand("select * from " + DatabaseFormName + " where userid = '" + userId + "'", XfsMysqlConnection.Connection);//读取数据函数  
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                List<TmSoulerDB> itemDBs = new List<TmSoulerDB>();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        TmSoulerDB item = new TmSoulerDB();
                        item.Id = reader.GetInt32(0);
                        item.Name = reader.GetString(1);
                        item.UserId = reader.GetInt32(2);
                        item.SoulerId = reader.GetInt32(3);
                        item.Exp = reader.GetInt32(4);
                        item.Level = reader.GetInt32(5);
                        item.Coin = reader.GetInt32(6);
                        item.Diamond = reader.GetInt32(7);
                        item.Hp = reader.GetInt32(8);
                        item.Mp = reader.GetInt32(9);
                        item.State = reader.GetInt32(10);
                        item.CdTime = reader.GetDouble(11);
                        item.ServerId = reader.GetInt32(12);
                        item.SenceId = reader.GetInt32(13);
                        item.px = reader.GetDouble(14);
                        item.py = reader.GetDouble(15);
                        item.pz = reader.GetDouble(16);
                        item.ax = reader.GetDouble(17);
                        item.ay = reader.GetDouble(18);
                        item.az = reader.GetDouble(19);
                        item.CreateDate = reader.GetString(20);
                        itemDBs.Add(item);
                    }
                }
                return itemDBs;
            }
            catch (Exception)
            {
                Console.WriteLine("查询失败...168");
                return null;
            }
            finally
            {
                reader.Close();
            }
        }        //读取表格//得到userid所有角色列表         
        internal TmSoulerDB GetTmSoulerdbById(int id)
        {
            MySqlCommand mySqlCommand = new MySqlCommand("select * from " + DatabaseFormName + " where id = '" + id + "'", XfsMysqlConnection.Connection);//读取数据函数  
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                TmSoulerDB item = new TmSoulerDB();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        item.Id = reader.GetInt32(0);
                        item.Name = reader.GetString(1);
                        item.UserId = reader.GetInt32(2);
                        item.SoulerId = reader.GetInt32(3);
                        item.Exp = reader.GetInt32(4);
                        item.Level = reader.GetInt32(5);
                        item.Coin = reader.GetInt32(6);
                        item.Diamond = reader.GetInt32(7);
                        item.Hp = reader.GetInt32(8);
                        item.Mp = reader.GetInt32(9);
                        item.State = reader.GetInt32(10);
                        item.CdTime = reader.GetDouble(11);
                        item.ServerId = reader.GetInt32(12);
                        item.SenceId = reader.GetInt32(13);
                        item.px = reader.GetDouble(14);
                        item.py = reader.GetDouble(15);
                        item.pz = reader.GetDouble(16);
                        item.ax = reader.GetDouble(17);
                        item.ay = reader.GetDouble(18);
                        item.az = reader.GetDouble(19);
                        item.CreateDate = reader.GetString(20);
                    }
                }
                return item;
            }
            catch (Exception)
            {
                Console.WriteLine("查询失败...");
                return null;
            }
            finally
            {
                reader.Close();
            }
        }                       //读取表格//得到id单个角色列表
        internal void InsertItemdb(string name, int soulId, int userid, int exp, int level, int hp, int mp, int coin, int diamond, int senceId, double px, double py, double pz, double ax, double ay, double az, int serverid)
        {
            MySqlCommand mySqlCommand = new MySqlCommand("insert into " + DatabaseFormName + "(name,soulId,userid,exp,level,hp,mp,coin,diamond,senceId,px,py,pz,ax,ay,az,serverid) values('" + name + "','" + soulId + "','" + userid + "','" + exp + "','" + level + "','" + hp + "','" + mp + "','" + coin + "','" + diamond + "','" + senceId + "','" + px + "','" + py + "','" + pz + "','" + ax + "','" + ay + "','" + az + "','" + serverid + "')", XfsMysqlConnection.Connection);  //插入列表行
            try
            {
                mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Console.WriteLine("插入数据失败..." + message);
            }
        }
        internal void UpdateItemdb(int id, int exp, int level, int hp, int mp, int coin, int diamond)
        {
            MySqlCommand mySqlCommand = new MySqlCommand("update " + DatabaseFormName + " set exp = '" + exp + "', level = '" + level + "', hp = '" + hp + "', mp = '" + mp + "', coin = '" + coin + "', diamond = '" + diamond + "' where id = '" + id + "'", XfsMysqlConnection.Connection); //更新列表行
            try
            {
                mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Console.WriteLine("修改数据失败..." + message);
            }
        }
        internal void RemoveItemdb(int id)
        {
            MySqlCommand mySqlCommand = new MySqlCommand("delete from " + DatabaseFormName + " where id = '" + id + "'", XfsMysqlConnection.Connection); //插入用户  
            try
            {
                mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Console.WriteLine("删除数据失败..." + message);
            }
        }
    }
}