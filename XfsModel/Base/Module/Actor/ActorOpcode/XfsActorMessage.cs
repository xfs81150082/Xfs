﻿using System;
using System.Collections.Generic;

namespace Xfs
{
    #region ActorMessages 
    [Serializable]
    public partial class Actor_TestRequest : IXfsMessage
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
    public partial class Actor_TestResponse : IXfsMessage
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
    public partial class Actor_TransferRequest : IXfsMessage
    {
        //private static readonly pb::MessageParser<Actor_TransferRequest> _parser = new pb::MessageParser<Actor_TransferRequest>(() => (Actor_TransferRequest)MessagePool.Instance.Fetch(typeof(Actor_TransferRequest)));
        //public static pb::MessageParser<Actor_TransferRequest> Parser { get { return _parser; } }

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

        private int mapIndex_;
        public int MapIndex
        {
            get { return mapIndex_; }
            set
            {
                mapIndex_ = value;
            }
        }

        public void WriteTo(/*pb::CodedOutputStream output*/)
        {
            //if (MapIndex != 0)
            //{
            //    output.WriteRawTag(8);
            //    output.WriteInt32(MapIndex);
            //}
            //if (RpcId != 0)
            //{
            //    output.WriteRawTag(208, 5);
            //    output.WriteInt32(RpcId);
            //}
            //if (ActorId != 0L)
            //{
            //    output.WriteRawTag(232, 5);
            //    output.WriteInt64(ActorId);
            //}
        }

        public int CalculateSize()
        {
            int size = 0;
            //if (RpcId != 0)
            //{
            //    size += 2 + pb::CodedOutputStream.ComputeInt32Size(RpcId);
            //}
            //if (ActorId != 0L)
            //{
            //    size += 2 + pb::CodedOutputStream.ComputeInt64Size(ActorId);
            //}
            //if (MapIndex != 0)
            //{
            //    size += 1 + pb::CodedOutputStream.ComputeInt32Size(MapIndex);
            //}
            return size;
        }

        public void MergeFrom(/*pb::CodedInputStream input*/)
        {
            //mapIndex_ = 0;
            //rpcId_ = 0;
            //actorId_ = 0;
            //uint tag;
            //while ((tag = input.ReadTag()) != 0)
            //{
            //    switch (tag)
            //    {
            //        default:
            //            input.SkipLastField();
            //            break;
            //        case 8:
            //            {
            //                MapIndex = input.ReadInt32();
            //                break;
            //            }
            //        case 720:
            //            {
            //                RpcId = input.ReadInt32();
            //                break;
            //            }
            //        case 744:
            //            {
            //                ActorId = input.ReadInt64();
            //                break;
            //            }
            //    }
            //}
        }

    }
    [Serializable]
    public partial class Actor_TransferResponse : IXfsMessage
    {
        //private static readonly pb::MessageParser<Actor_TransferResponse> _parser = new pb::MessageParser<Actor_TransferResponse>(() => (Actor_TransferResponse)MessagePool.Instance.Fetch(typeof(Actor_TransferResponse)));
        //public static pb::MessageParser<Actor_TransferResponse> Parser { get { return _parser; } }

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

        public void WriteTo(/*pb::CodedOutputStream output*/)
        {
            //if (RpcId != 0)
            //{
            //    output.WriteRawTag(208, 5);
            //    output.WriteInt32(RpcId);
            //}
            //if (Error != 0)
            //{
            //    output.WriteRawTag(216, 5);
            //    output.WriteInt32(Error);
            //}
            //if (Message.Length != 0)
            //{
            //    output.WriteRawTag(226, 5);
            //    output.WriteString(Message);
            //}
        }

        public int CalculateSize()
        {
            int size = 0;
            //if (RpcId != 0)
            //{
            //    size += 2 + pb::CodedOutputStream.ComputeInt32Size(RpcId);
            //}
            //if (Error != 0)
            //{
            //    size += 2 + pb::CodedOutputStream.ComputeInt32Size(Error);
            //}
            //if (Message.Length != 0)
            //{
            //    size += 2 + pb::CodedOutputStream.ComputeStringSize(Message);
            //}
            return size;
        }

        public void MergeFrom(/*pb::CodedInputStream input*/)
        {
            //rpcId_ = 0;
            //error_ = 0;
            //message_ = "";
            //uint tag;
            //while ((tag = input.ReadTag()) != 0)
            //{
            //    switch (tag)
            //    {
            //        default:
            //            input.SkipLastField();
            //            break;
            //        case 720:
            //            {
            //                RpcId = input.ReadInt32();
            //                break;
            //            }
            //        case 728:
            //            {
            //                Error = input.ReadInt32();
            //                break;
            //            }
            //        case 738:
            //            {
            //                Message = input.ReadString();
            //                break;
            //            }
            //    }
            //}
        }

    }




    #endregion

}