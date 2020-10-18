using System;
using System.Threading;
using Xfs;
namespace XfsClient
{
    class XfsTest : XfsSystem
    {
        private static XfsTest _instance;
        public static XfsTest Instance { get => _instance; }
        public override void XfsAwake() { base.XfsAwake(); _instance = this; }

        private bool IsUserLogin = false;
        public override void XfsUpdate()
        {
            TestTmUserLogin();
            TmDebugLog();
        }
        #region
        float time = 0;
        float restime = 100;
        void TmDebugLog()
        {
            time += 1;
            if (time > restime)
            {
                //if (TmObjects.Engineers.Count > 0)
                //{
                //    Console.WriteLine(" TmObjects.Engineers: " + TmObjects.Engineers.Count);
                //}
                time = 0;
            }
        }
        #endregion


        #region
        void TestTmUserLogin()
        {
            Thread.Sleep(1000);
            if (IsUserLogin == false)
            {
                IsUserLogin = true;
                Console.WriteLine(XfsTimerTool.CurrentTime() + " IsUserLogin:{0}---43", IsUserLogin.ToString());
                TmUserLogin();
            }
        }
        void TmUserLogin()
        {
            //TmParameter parameter = TmParameterTool.ToParameter(TenCode.User, ElevenCode.UserLogin);
            //TmParameterTool.AddParameter(parameter, "Username", "Tumo");
            //TmParameterTool.AddParameter(parameter, "Password", "123456");
            //TmTcpSocket.Instance.Send(parameter);

            //Console.WriteLine(TmTimerTool.CurrentTime() + " 用户登录37, Username:{0} Password:{1}", parameter.Parameters["Username"], parameter.Parameters["Password"]);
        }
        public void EngineerLogin(int rolerId)
        {
            //TmParameter parameter = TmParameterTool.ToJsonParameter(TenCode.Engineer, ElevenCode.EngineerLogin, ElevenCode.EngineerLogin.ToString(), rolerId);
            //TmTcpSocket.Instance.Send(parameter);
            //Console.WriteLine(TmTimerTool.CurrentTime() + " 角色登录45，Id:{0}", rolerId);
        }
        #endregion

    }
}