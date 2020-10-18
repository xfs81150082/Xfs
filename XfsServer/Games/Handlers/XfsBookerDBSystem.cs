using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xfs;

namespace XfsServer
{
    class XfsBookerDBSystem : XfsSystem
    {
        public override void XfsAwake()
        {
            ValTime = 40000;
            AddComponent(new XfsSession());
        }
        public override void XfsUpdate()
        {
            foreach (XfsEntity entity in GetTmEntities())
            {
                SetBookers(entity);
            }
        }
        
        #region Bookers 每4秒刷新一下，如果死亡重新生成，否则不变
        void SetBookers(XfsEntity entity)
        {
            XfsSession session = entity.GetComponent<XfsSession>();
            //if (TmObjects.Bookers.Count > 0 && session.IsLogin)
            //{
            //    TmParameter response = TmParameterTool.ToJsonParameter(TenCode.Booker, ElevenCode.SetSoulerDBs, ElevenCode.SetSoulerDBs.ToString(), TmObjects.Bookers);
            //    response.Keys.Add(entity.EcsId);
            //    TmTcpSocket.Instance.Send(response);
            //    Console.WriteLine(TmTimerTool.CurrentTime() + " TmBookerDBSystem-Bookers: " + TmObjects.Bookers.Count);
            //}
        }
        #endregion

    }
}
