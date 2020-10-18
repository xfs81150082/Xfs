using System;
using System.Collections.Generic;
using Xfs;

namespace XfsServer
{
    public class XfsBookerHandler : XfsEntity
    {      
        public override void OnTransferParameter(object obj , XfsParameter parameter)
        {
            ElevenCode elevenCode = parameter.ElevenCode;
            switch (elevenCode)
            {
                case (ElevenCode.Code0001):
                    Console.WriteLine(XfsTimerTool.CurrentTime() + " TmStatusSyncHandler: " + elevenCode);

                    //Parent.GetComponent<TmStatusSyncHandler>().OnTransferParameter(this, parameter);

                    break;
                case (ElevenCode.Code0002):
                    break;
                default:
                    break;
            }
        }
        internal Dictionary<int, TmSoulerDB> Bookers { get; set; }
        void GetRolersByRolerId(XfsParameter parameter)
        {
            bool yes = false;
            int count = 0;
            while (!yes)
            {
                if (this.Bookers != null)
                {
                    XfsParameter response = XfsParameterTool.ToJsonParameter(TenCode.Code0001, ElevenCode.Code0001, ElevenCode.Code0001.ToString(), this.Bookers);
                    response.Keys.Add(parameter.Keys[0]);
                    XfsGame.XfsSence.GetComponent<XfsTcpServer>().Send(response);
                    yes = true;
                }
                else
                {
                    XfsMysqlHandler.Instance.GetComponent<XfsBookerMysql>().OnTransferParameter(this, parameter);
                    Console.WriteLine(XfsTimerTool.CurrentTime() + " Bookers:" + Bookers.Count);
                    count += 1;
                }
                if (count > 4)
                {
                    yes = true;
                    break;
                }
            }
        }  
        
        void DiethHandler(XfsParameter parameter)
        {
            TmSoulerDB soulerDB = XfsParameterTool.GetJsonValue<TmSoulerDB>(parameter, ElevenCode.Code0001.ToString());

            parameter.ElevenCode = ElevenCode.Code0001;
            foreach(var tem in XfsGame.XfsSence.GetComponent<XfsTcpServer>().TPeers.Keys)
            {
                parameter.Keys.Add(tem);
            }
            XfsGame.XfsSence.GetComponent<XfsTcpServer>().Send(parameter);
        }

    }
}