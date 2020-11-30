using System;
namespace Xfs
{
    class TmSoulerUpdateSystem : XfsUpdateSystem<TmSouler>
    {       
        public override void Update(TmSouler self)
        {
            
                PropertyInit(self);
           
        }
        TmSouler souler { get; set; }
        TmProperty property { get; set; }
        TmChangeType changeType { get; set; }
        void PropertyInit(TmSouler self)
        {
            //if (entity.GetComponent<TmSouler>() != null && entity.GetComponent<TmProperty>() != null && entity.GetComponent<TmChangeType>() != null)
            //{
            //    TmChangeType changeType = entity.GetComponent<TmChangeType>();
            //    TmProperty property = entity.GetComponent<TmProperty>();
            //    if (property.Hp != changeType.changeCount)
            //    {
            //        InitProperty(entity);
            //        changeType.changeCount = property.Hp;
            //    }              
            //}
        }
        void InitProperty(XfsEntity soulerItem)
        {
            UpdateLevel(soulerItem);
            InitPropertyOne(soulerItem);
            InitPropertyTwo(soulerItem);
            InitPropertyThree(soulerItem);
        }  //更新属性
        void InitPropertyOne(XfsEntity soulerItem)
        {
            soulerItem.GetComponent<TmProperty>().Stamina = soulerItem.GetComponent<TmSouler>().Stamina + soulerItem.GetComponent<TmChangeType>().Level ;
            soulerItem.GetComponent<TmProperty>().Brains = soulerItem.GetComponent<TmSouler>().Brains + soulerItem.GetComponent<TmChangeType>().Level;
            soulerItem.GetComponent<TmProperty>().Power = soulerItem.GetComponent<TmSouler>().Power + soulerItem.GetComponent<TmChangeType>().Level;
            soulerItem.GetComponent<TmProperty>().Agility = soulerItem.GetComponent<TmSouler>().Agility + soulerItem.GetComponent<TmChangeType>().Level ;
        }
        void InitPropertyTwo(XfsEntity soulerItem)
        { 
            soulerItem.GetComponent<TmProperty>().MaxHp = (int)Math.Round(soulerItem.GetComponent<TmProperty>().Stamina * soulerItem.GetComponent<TmSouler>().StaminaRate);                  //Hp与根骨成正比；暂定10倍；
            soulerItem.GetComponent<TmProperty>().MaxMp = (int)Math.Round(soulerItem.GetComponent<TmChangeType>().Level * soulerItem.GetComponent<TmSouler>().StaminaRate);                  //Mp与等级有关；暂定10倍；
            soulerItem.GetComponent<TmProperty>().Bp = (int)Math.Round(soulerItem.GetComponent<TmProperty>().Brains * soulerItem.GetComponent<TmSouler>().BrainsRate);                       //Bp与智力成正比；暂定1倍；
            soulerItem.GetComponent<TmProperty>().Ap = (int)Math.Round(soulerItem.GetComponent<TmProperty>().Power * soulerItem.GetComponent<TmSouler>().PowerRate);                         //Ap与力量成正比；暂定1倍；
        }
        void InitPropertyThree(XfsEntity soulerItem)
        {
            soulerItem.GetComponent<TmProperty>().Sp = Math.Round(soulerItem.GetComponent<TmSouler>().Sp  + soulerItem.GetComponent<TmProperty>().Agility * soulerItem.GetComponent<TmSouler>().AgilityRate / (100 * soulerItem.GetComponent<TmChangeType>().Level), 4);   //sp暴击率与敏捷成正比。
            soulerItem.GetComponent<TmProperty>().Hr = Math.Round(soulerItem.GetComponent<TmSouler>().Hr + soulerItem.GetComponent<TmProperty>().Agility * soulerItem.GetComponent<TmSouler>().AgilityRate / (100 * soulerItem.GetComponent<TmChangeType>().Level), 4);    //hr命中率与敏捷成正比。
            soulerItem.GetComponent<TmProperty>().Cr = Math.Round(soulerItem.GetComponent<TmSouler>().Cr + soulerItem.GetComponent<TmProperty>().Agility * soulerItem.GetComponent<TmSouler>().AgilityRate / (100 * soulerItem.GetComponent<TmChangeType>().Level), 4);    //cr暴击率与敏捷成正比。
        }
        void UpdateLevel(XfsEntity soulerItem)
        {
            if (soulerItem.GetComponent<TmChangeType>().Level == soulerItem.GetComponent<TmSouler>().LevelUpLimit) return;
            int expTem = (int)Math.Round((soulerItem.GetComponent<TmChangeType>().Level + 1.0) * (soulerItem.GetComponent<TmChangeType>().Level + 1.0) + 10.0);
            if (soulerItem.GetComponent<TmChangeType>().Exp >= expTem)
            {
                soulerItem.GetComponent<TmChangeType>().Exp -= expTem;
                soulerItem.GetComponent<TmChangeType>().Level++;
                if (soulerItem.GetComponent<TmChangeType>().Level >= soulerItem.GetComponent<TmSouler>().LevelUpLimit)
                {
                    soulerItem.GetComponent<TmChangeType>().Level = soulerItem.GetComponent<TmSouler>().LevelUpLimit;
                    soulerItem.GetComponent<TmChangeType>().Exp = 0;
                }
                soulerItem.GetComponent<TmProperty>().Hp = soulerItem.GetComponent<TmProperty>().MaxHp;
                soulerItem.GetComponent<TmProperty>().Mp = soulerItem.GetComponent<TmProperty>().MaxMp;
            }
            //1-59级：经验值 = ((8 × 角色等级) + 难度系数(角色等级)) × 基础经验值(角色等级) × 经验系数(角色等级)
            //60级：经验值 = 155 + 基础经验值(角色等级) × (1275 - ((69 - 角色等级) × (3 + (69 - 角色等级) × 4)));
            //61-69级：经验值 = 155 + 基础经验值(角色等级) × (1344 - ((69 - 角色等级) × (3 + (69 - 角色等级) × 4)));
            //基础经验值(角色等级) = 45 + (5 × 角色等级) (适用于艾泽拉斯的怪物) 基础经验值(角色等级) = 235 + (5 × 角色等级) (适用于外域的怪物) 基础经验值(角色等级) = 626 + (5 × 角色等级) (适用于诺森德的怪物);
            //难度系数(角色等级) = 0 (角色等级 <= 28) 难度系数(角色等级) = 1 (角色等级 = 29) 难度系数(角色等级) = 3 (角色等级 = 30) 难度系数(角色等级) = 6 (角色等级 = 31) 难度系数(角色等级) = 5 × (角色等级 - 30) (角色等级 >= 32, <= 59);
            //经验系数(角色等级) = 1(角色等级 <= 10) 经验系数(角色等级) = (1 - (角色等级 - 10) / 100)(角色等级 >= 11, <= 27) 经验系数(角色等级) = 0.82(角色等级 >= 28, <= 59);
        }          //等级更新

    }
}