using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xfs;

namespace XfsGateSever
{
    class XfsUserHandler : XfsComponent
    {
        public void OnTransferParameter(object obj, XfsParameter parameter)
        {
            ElevenCode elevenCode = parameter.ElevenCode;
            switch (elevenCode)
            {
                case (ElevenCode.Code0001):
                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " TmUser: " + elevenCode);
                    CheckUserLoginPassword(parameter);
                    break;
                case (ElevenCode.End):
                    break;
                default:
                    break;
            }
        }
        internal XfsUser User { get; set; }
        private void CheckUserLoginPassword(XfsParameter parameter)
        {
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " to TmUserHandler 30 " + parameter.ElevenCode.ToString());
            //string name = XfsParameterTool.GetValue<string>(parameter, "Username");
            //string word = XfsParameterTool.GetValue<string>(parameter, "Password");
            //Console.WriteLine(XfsTimeHelper.CurrentTime() + " Username:" + name + " Password:" + word);
            //XfsMysqlHandler.Instance.GetComponent<XfsUserMysql>().OnTransferParameter(this, parameter);
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " this.User:" + this.User.Username + " this.User:" + this.User.Password + " this.User.Phone:" + this.User.Phone);
            if (this.User != null)
            {
                //if (User.Password == word)
                //{
                //    //XfsParameterTool.AddParameter(parameter, parameter.ElevenCode.ToString(), this.User.Id);
                //    //parameter.ElevenCode = ElevenCode.Code0001;
                //    //Parent.GetComponent<XfsBookerHandler>().OnTransferParameter(this, parameter);
                //    //Console.WriteLine(XfsTimerTool.CurrentTime() + " Username:" + name + " Password:" + word);
                //}
                //else
                //{
                //    Console.WriteLine("密码不正确");
                //}
            }
            else
            {
                Console.WriteLine("帐号不存在");
            }
        }
        
        
    }
}