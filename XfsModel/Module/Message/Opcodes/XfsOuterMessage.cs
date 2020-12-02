using System;
using System.Collections.Generic;

namespace Xfs
{
    #region OutMessages
    [Serializable]
    public partial class C2G_TestRequest : IXfsMessage
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

        private long actorId_;
        public long ActorId
        {
            get { return actorId_; }
            set
            {
                actorId_ = value;
            }
        }

        private string Message_ = "";
        public string Message
        {
            get { return Message_; }
            set
            {
                Message_ =  value;
            }
        }
        private string request_ = "";
        public string Request
        {
            get { return request_; }
            set
            {
                request_ = value;
            }
        }

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

        private string response_ = "";
        public string Response
        {
            get { return response_; }
            set
            {
                response_ = value;
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