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
            ////NodtTest(entity);
            ///Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsTestSystem-32,开始打电话给服务器。 ");
           
            TestCall(entity);
            
            ///Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsTestSystem-34,打电话方法的后面一行。 ");
        }
        async void TestCall(XfsEntity entity)
        {
            XfsTest test = entity.GetComponent<XfsTest>();
            test.time += 1;
            if (test.time > test.restime)
            {
                test.restime += 300;

                string tt = test.call;
                XfsParameter parameter = XfsParameterTool.ToParameter(TenCode.Code0004, ElevenCode.Code0004, ElevenCode.Code0004.ToString(), tt);
                parameter.Back = true;
                parameter.EcsId = XfsIdGenerater.GetId();

                XfsTcpClient client = null;
                XfsSockets.XfsTcpClients.TryGetValue(NodeType.Node, out client);
                if (client != null)
                {
                    //(client as XfsTcpClientNodeNet).Send(parameter);
                    Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsTestSystem-54,开始打电话给服务器。 ");

                    //XfsParameter response = await (client as XfsTcpClientNodeNet).Call(parameter);
                    XfsParameter response = await client.Call(parameter);
                    string res = XfsParameterTool.GetValue<string>(response, response.ElevenCode.ToString());

                    Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsTestSystem-59: " + res);

                }

                //Console.WriteLine(XfsTimerTool.CurrentTime() + " 55 XfsTestSystem: " + test.time);
            }
        }
        void DbTest(XfsEntity entity)
        {
            XfsTest text = entity.GetComponent<XfsTest>();
            text.time += 1;
            if (text.time > text.restime)
            {
                text.restime += 300;

                string tt = "客户端-Node-Db";

                XfsParameter parameter = XfsParameterTool.ToParameter(TenCode.Code0002, ElevenCode.Code0002, ElevenCode.Code0002.ToString(), tt);
                parameter.Back = true;
                parameter.EcsId = XfsIdGenerater.GetId();

                XfsTcpClient client = null;
                XfsSockets.XfsTcpClients.TryGetValue(NodeType.Node, out client);
                if (client != null)
                {
                    (client as XfsTcpClientNodeNet).Send(parameter);
                }

                Console.WriteLine(XfsTimerTool.CurrentTime() + " 55 XfsTestSystem: " + text.time);
                Console.WriteLine(XfsTimerTool.CurrentTime() + " 56 XfsTestSystem: " + text.time);
            }
        }
        void NodtTest(XfsEntity entity)
        {
            XfsTest text = entity.GetComponent<XfsTest>();
            text.time += 1;

            //Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsTestSystem: " + text.time);

            if (text.time > text.restime)
            {
                text.restime += 600;

                string tt = text.longin;
                tt += " : " + text.time.ToString();

                XfsParameter parameter = XfsParameterTool.ToParameter(TenCode.Code0001, ElevenCode.Code0001, ElevenCode.Code0001.ToString(), tt);

                Console.WriteLine(XfsTimerTool.CurrentTime() + " 44 XfsTestSystem: " + text.time);

                XfsTcpClient client = null;
                XfsSockets.XfsTcpClients.TryGetValue(NodeType.Db, out client);
                if (client != null)
                {
                    client.Send(parameter);
                }


                //(XfsSockets.GetTcpClient(NodeType.Db) as XfsTcpClientDbNet).Send(parameter, NodeType.Db);
                //(XfsSockets.GetTcpClient(NodeType.Node) as XfsTcpClientNodeNet).Send(parameter, NodeType.Node);


                //XfsGame.XfsSence.GetComponent<XfsTcpClientDbNet>().Send(parameter, NodeType.Db);
                XfsGame.XfsSence.GetComponent<XfsTcpClientNodeNet>().Send(parameter);

                Console.WriteLine(XfsTimerTool.CurrentTime() + " 53 XfsTestSystem: " + text.time);
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
