using System;
using System.Collections;
using System.Collections.Generic;
using Xfs;
namespace XfsConsoleClient
{
    class XfsBookerController : XfsComponent
    {
        public override void OnTransferParameter(object obj, XfsParameter parameter)
        {
            ElevenCode elevenCode = parameter.ElevenCode;
            switch (elevenCode)
            {
                case (ElevenCode.Code0001):
                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " TmBookerController: " + elevenCode);

                    SetSoulerDBs(parameter);

                    break;
                default:
                    break;
            }
        }
        void SetSoulerDBs(XfsParameter parameter)
        {
            //Dictionary<int, XfsSoulerDB> bookers = XfsParameterTool.GetJsonValue<Dictionary<int, TmSoulerDB>>(parameter, parameter.ElevenCode.ToString());
            //if (bookers.Count > 0)
            //{
            //    XfsObjects.Bookers = bookers;
            //    Console.WriteLine(XfsTimerTool.CurrentTime() + " TmObjects.Bookers: " + XfsObjects.Bookers.Count);
            //}

        }
      

    }
}
