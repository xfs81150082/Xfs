using System;
namespace Xfs
{
    [Serializable]
    public enum Quality
    {
        White,          //白字装备-低端-商人卖
        Green,          //绿字装备-普通-小怪掉
        Blue,           //蓝字装备-中端-精英怪掉
        Violet,         //紫字装备-高端-Boss怪掉
        Orange,         //橙字装备-稀少-特殊任务掉
    }
    [Serializable]
    public enum EquipType
    {
        Weapon,                //武器1
        Helm,                  //头盔2
        Necklace,              //项链3  
        Cloth,                 //衣服4
        Bracelet,              //手镯5
        Shoes,                 //鞋子6
        Ring,                  //戒指7
        Wing,                  //翅膀8
        Break,
        Water,
        Box
    }
    [Serializable]
    public enum InfoType
    {
        Name,
        HeadPortrait,
        Exp,
        Level,
        Diamond,
        Coin,
        Stamina,
        Brains,
        Power,
        Agility,
        Hp,
        Mp,
        MaxHp,
        MaxMp,
        Bp,
        Ap,
        Sp,
        Hr,
        Cr,
        Hp2,
        Mp2,
        Ap2,
        Sp2,
        Equip,
        Drug,
        Box,
        Skill,
        All,
    }
    public enum PatrolState
    {
        CorePoint,
        RandomPoint,
        IndexPoint,
    }
    [Serializable]
    public enum RoleType
    {
        Engineer,
        Booker,
        Teacher,
        Administration,
        Valuation,
        Metering,
        Case,
    }
    [Serializable]
    public enum Tribe
    {
        Administration,
        Valuation,
        Metering,
        Case,
    }
    [Serializable]
    public enum EffectType
    {
        BloodSplat, //掉血
        Crowstorm,  //龙卷风
        DevilHand,  //鬼手
        DustStorm,  //尘暴
        FirePhoenix,//火凤凰
        HolyFire,   //圣火
        IceStrike,  //冰击
        Weapon,     //武器电芒
    }
    [Serializable]
    public enum TaskType
    {
        Main,          //主线任务
        Reward,        //赏金任务
        Daily,         //日常任务
    }
    [Serializable]
    public enum TaskState
    {
        NoAccept,        //不能领取    /**0 不可接状态*/  
        NoStart,         //没开始      /**1 可接  但还未接的状态*/ 
        Accept,          //接受        /**2 已接  正在进行中*/ 
        Reward,          //奖金        /**4 完成  未领奖*/  
        Complete,        //完成        /**3 完成  结束*/ 
    }
}
