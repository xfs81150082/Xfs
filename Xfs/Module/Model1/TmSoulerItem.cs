using System;
using System.Collections.Generic;
namespace Xfs
{
    [Serializable]
    public class TmSoulerItem : XfsEntity
    {      
        public TmSoulerItem()
        {
            AddComponent(new TmSouler());
            AddComponent(new TmName());
            AddComponent(new TmProperty());
            //AddComponent(new TmTransform());
            AddComponent(new TmChangeType());
            AddComponent(new XfsCoolDownComponent());
            AddComponent(new XfsAstarComponent());
        }                        ///构造函数 
        public TmSoulerItem(TmSoulerDB itemDB)
        {          
            //TmSouler souler = null;
            //XfsObjects.Soulers.TryGetValue(itemDB.SoulerId, out souler);
            //if (souler != null)
            //{
            //    if (this.GetComponent<TmSouler>() != null)
            //    {
            //        this.RemoveComponent<TmSouler>();
            //    }
            //    this.AddComponent(souler);
            //    if (GetComponent<TmCoolDown>() != null)
            //    {
            //        this.GetComponent<TmCoolDown>().CdTime = itemDB.CdTime;
            //        this.GetComponent<TmCoolDown>().MaxCdTime = souler.MaxColdTime;
            //    }
            //}
            //this.GetComponent<TmName>().Id = itemDB.Id;
            //this.GetComponent<TmName>().Name = itemDB.Name;
            //this.GetComponent<TmName>().ParentId = itemDB.UserId;
            //this.GetComponent<TmChangeType>().Exp = itemDB.Exp;
            //this.GetComponent<TmChangeType>().Level = itemDB.Level;
            //this.GetComponent<TmChangeType>().Coin = itemDB.Coin;
            //this.GetComponent<TmChangeType>().Diamond = itemDB.Diamond;
            //this.GetComponent<TmProperty>().Hp = itemDB.Hp;
            //this.GetComponent<TmProperty>().Mp = itemDB.Mp;
            //this.GetComponent<TmTransform>().px = itemDB.px;
            //this.GetComponent<TmTransform>().py = itemDB.py;
            //this.GetComponent<TmTransform>().pz = itemDB.pz;
            //this.GetComponent<TmTransform>().ax = itemDB.ax;
            //this.GetComponent<TmTransform>().ay = itemDB.ay;
            //this.GetComponent<TmTransform>().az = itemDB.az;          
        }
    }
}