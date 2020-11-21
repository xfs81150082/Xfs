using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Xfs
{
    class TmSoulerInit : XfsComponent
    {       
        void TmSoulerInfo()
        {
            //XfsObjects.Soulers = GetTmSoulers();
        }
        Dictionary<int,TmSouler> GetTmSoulers()
        {
            Dictionary<int, TmSouler> soulers = new Dictionary<int, TmSouler>();
            TmSouler souler11101 = GetTmSouler("小怪", 11101, "headimageboy", "BookerOne", "Icon_Player01", 30, "是最--的生灵", RoleType.Booker, EquipType.Break, InfoType.Bp, Quality.Green, 4);
            soulers.Add(souler11101.Id, souler11101);
            TmSouler souler11102 = GetTmSouler("小怪", 11102, "headimagegirl", "BookerOne", "Icon_Player01", 30, "是最--的生灵", RoleType.Booker, EquipType.Water, InfoType.Bp, Quality.Green, 4);
            soulers.Add(souler11102.Id, souler11102);
            TmSouler souler12101 = GetTmSouler("小怪", 12101, "headimageboy", "BookerOne", "Icon_Player01", 30, "是最--的生灵", RoleType.Booker, EquipType.Break, InfoType.Bp, Quality.Green, 4);
            soulers.Add(souler12101.Id, souler12101);
            TmSouler souler12102 = GetTmSouler("小怪", 12102, "headimagegirl", "BookerOne", "Icon_Player01", 30, "是最--的生灵", RoleType.Booker, EquipType.Water, InfoType.Bp, Quality.Green, 4);
            soulers.Add(souler12102.Id, souler12102);
            TmSouler souler13101 = GetTmSouler("小怪", 13101, "headimageboy", "BookerOne", "Icon_Player01", 30, "是最--的生灵", RoleType.Booker, EquipType.Break, InfoType.Bp, Quality.Green, 4);
            soulers.Add(souler13101.Id, souler13101);
            TmSouler souler13102 = GetTmSouler("小怪", 13102, "headimagegirl", "BookerOne", "Icon_Player01", 30, "是最--的生灵", RoleType.Booker, EquipType.Water, InfoType.Bp, Quality.Green, 4);
            soulers.Add(souler13102.Id, souler13102);
            TmSouler souler14101 = GetTmSouler("小怪", 14101, "headimageboy", "BookerOne", "Icon_Player01", 30, "是最--的生灵", RoleType.Booker, EquipType.Break, InfoType.Bp, Quality.Green, 4);
            soulers.Add(souler14101.Id, souler14101);
            TmSouler souler14102 = GetTmSouler("小怪", 14102, "headimagegirl", "BookerOne", "Icon_Player01", 30, "是最--的生灵", RoleType.Booker, EquipType.Water, InfoType.Bp, Quality.Green, 4);
            soulers.Add(souler14102.Id, souler14102);
            TmSouler souler15101 = GetTmSouler("玩家", 15101, "headimageboy", "EngineerOne", "Icon_Player01", 30, "人族是最智慧的生灵", RoleType.Engineer, EquipType.Break, InfoType.Bp, Quality.Green, 4);
            soulers.Add(souler15101.Id, souler15101);
            TmSouler souler15102 = GetTmSouler("玩家", 15102, "headimagegirl", "EngineerOne", "Icon_Player01", 30, "人族是最智慧的生灵", RoleType.Engineer, EquipType.Break, InfoType.Bp, Quality.Green, 4);
            soulers.Add(souler15102.Id, souler15102);
            TmSouler souler16101 = GetTmSouler("人类", 16101, "headimageboy", "TeacherOne", "Icon_Player01", 30, "是最--的生灵", RoleType.Booker, EquipType.Break, InfoType.Bp, Quality.Green, 4);
            soulers.Add(souler16101.Id, souler16101);
            TmSouler souler16102 = GetTmSouler("人类", 16102, "headimagegirl", "TeacherOne", "Icon_Player01", 30, "是最--的生灵", RoleType.Booker, EquipType.Water, InfoType.Bp, Quality.Green, 4);
            soulers.Add(souler16102.Id, souler16102);
            return soulers;
        }
        TmSouler GetTmSouler(string name,int id,string icon,string avatarname,string chater,int leveluplimit,string does,RoleType roleType,EquipType equipType,InfoType infoType,Quality quality,int maxcoldtime)
        {
            TmSouler souler = new TmSouler();
            souler.Name = name;
            souler.Id = id;
            souler.Icon = icon;
            souler.AvatarName = avatarname;
            souler.Chater = chater;
            souler.Does = does;
            souler.RoleType = roleType;
            souler.EquipType = equipType;
            souler.InfoType = infoType;
            souler.Quality = quality;
            souler.MaxColdTime = maxcoldtime;
            return souler;
        }


    }
}
