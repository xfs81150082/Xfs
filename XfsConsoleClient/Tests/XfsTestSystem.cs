using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xfs;

namespace XfsConsoleClient
{
     public class XfsTestSystem :XfsSystem
    {
        public override void XfsAwake()
        {
            base.XfsAwake();
            this.AddComponent(new XfsTest());
        }

        public override void XfsUpdate()
        {
            foreach (XfsEntity entity in GetTmEntities())
            {
                UpdateTest(entity);
            }
        }


        private void UpdateTest(XfsEntity entity)
        {
            XfsTest text = entity.GetComponent<XfsTest>();
            text.time += 1;

            //Console.WriteLine(XfsTimerTool.CurrentTime() + " Time: "+ text.time);

            if (text.time > text.restime)
            {
                text.restime += 600;

                string tt = text.longin;
                tt += " : "+ text.time.ToString();

                XfsParameter parameter = XfsParameterTool.ToParameter(TenCode.Code0001, ElevenCode.Code0001, ElevenCode.Code0001.ToString(), tt);
                
                XfsTcpClient.Instance.Send(parameter, NodeType.Client);
                //XfsGame.XfsSence.GetComponent<XfsTcpClientNodeNet>().Send(parameter, NodeType.Client);
            }

        }



        #region
        void TmDebugLog(XfsTest text)
        {
            text.time += 1;
            if (text.time > text.restime)
            {
                //if (TmObjects.Engineers.Count > 0)
                //{
                //    Console.WriteLine(" TmObjects.Engineers: " + TmObjects.Engineers.Count);
                //}
                text.time = 0;
            }
        }
        #endregion


        #region
        void TestTmUserLogin(XfsTest text)
        {
            Thread.Sleep(1000);
            if (text.IsUserLogin == false)
            {
                text.IsUserLogin = true;
                Console.WriteLine(XfsTimerTool.CurrentTime() + " IsUserLogin:{0}---43", text.IsUserLogin.ToString());
                TmUserLogin(text);
            }
        }
        void TmUserLogin(XfsTest text)
        {
            //TmParameter parameter = TmParameterTool.ToParameter(TenCode.User, ElevenCode.UserLogin);
            //TmParameterTool.AddParameter(parameter, "Username", "Tumo");
            //TmParameterTool.AddParameter(parameter, "Password", "123456");
            //TmTcpSocket.Instance.Send(parameter);

            //Console.WriteLine(TmTimerTool.CurrentTime() + " 用户登录37, Username:{0} Password:{1}", parameter.Parameters["Username"], parameter.Parameters["Password"]);
        }
        void EngineerLogin(XfsTest text, int rolerId)
        {
            //TmParameter parameter = TmParameterTool.ToJsonParameter(TenCode.Engineer, ElevenCode.EngineerLogin, ElevenCode.EngineerLogin.ToString(), rolerId);
            //TmTcpSocket.Instance.Send(parameter);
            //Console.WriteLine(TmTimerTool.CurrentTime() + " 角色登录45，Id:{0}", rolerId);
        }
        #endregion
















    }
}
