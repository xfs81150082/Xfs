using System;
using System.Collections.Generic;

namespace Xfs
{
    #region OutMessages
    [Serializable]
    public partial class C2G_TestRequest : IXfsMessage
    {
        public int RpcId { get; set; }
        public int Opcode { get; set; }
        public string Message { get; set; }

    }
    [Serializable]
    public partial class G2C_TestResponse : IXfsMessage
    {
        private int rpcId_;
        public int RpcId
        {
            get { return rpcId_; }
            set
            {
                rpcId_ = value;
            }
        }

        private int error_;
        public int Error
        {
            get { return error_; }
            set
            {
                error_ = value;
            }
        }

        private string message_ = "";
        public string Message
        {
            get { return message_; }
            set
            {
                message_ = value;
            }
        }    
     
    }
    [Serializable]
    public partial class C4G_Ping : IXfsMessage
    {
        public int RpcId { get; set; }
        public int Opcode { get; set; }
        public string Message { get; set; }       
    }
    [Serializable]
    public partial class G4C_Pong : IXfsMessage
    {
        private int rpcId_;
        public int RpcId
        {
            get { return rpcId_; }
            set
            {
                rpcId_ = value;
            }
        }

        private int error_;
        public int Error
        {
            get { return error_; }
            set
            {
                error_ = value;
            }
        }

        private string message_ = "";
        public string Message
        {
            get { return message_; }
            set
            {
                message_ = value;
            }
        }

    }

  




  

    #endregion

}