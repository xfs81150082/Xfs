using System;
namespace Xfs
{
    [Serializable]
    public class TmSoulerDB
    {    
        public int Id { get; set; } = 100001;           //Id
        public string Name { get; set; } = "tumo";      //用户名
        public int UserId { get; set; } = 0;            //UserId
        public int SoulerId { get; set; } = 15101;      //SoulerId
        public int Exp { get; set; } = 0;               //经验数
        public int Level { get; set; } = 0;             //等级
        public int Coin { get; set; } = 0;              //金币数  
        public int Diamond { get; set; } = 0;           //钻石数
        public int Hp { get; set; } = 0;                //血量
        public int Mp { get; set; } = 0;                //法量          
        public int State { get; set; } = 0;             //角色死亡状态，0为死亡，1为活着
        public double CdTime { get; set; } = 0;         //角色刷新时间还余多久
        public int ServerId { get; set; } = 0;          //服务Id
        public int SenceId { get; set; } = 0;           //场景id
        public double px { get; set; } = 0.0;           //坐标x
        public double py { get; set; } = 0.0;           //坐标x
        public double pz { get; set; } = 0.0;           //坐标x  
        public double ax { get; set; } = 0.0;           //坐标w-朝向
        public double ay { get; set; } = 0.0;           //坐标x  
        public double az { get; set; } = 0.0;           //坐标w-朝向
        public string CreateDate { get; set; } = "20170408";      //角色创建日期
    }
}