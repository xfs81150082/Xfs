using System;
using System.Collections.Generic;
namespace Xfs
{
    [Serializable]
    public class XfsParameter
    {
        public string EcsId { get; set; }
        public int RpcId { get; set; }
        public bool Back { get; set; } = false;
        public int StartActorId { get; set; }
        public int GoalActorId { get; set; }
        public short tpye { get; set; }
        public TenCode TenCode { get; set; }
        public ElevenCode ElevenCode { get; set; }
        public List<long> PeerIds { get; set; } = new List<long>();
        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
    }

    [Serializable]
    public enum TenCode
    {
        Zero,          /// 链接心跳包    
        Code0001,
        Code0002,
        Code0003,
        Code0004,
        Code0005,
        Code0006,
        Code0007,
        Code0008,
        All,
        End,
    }

    [Serializable]
    public enum ElevenCode
    {
        Zero,           /// 链接心跳包    
        Code0001,
        Code0002,
        Code0003,
        Code0004,
        Code0005,
        Code0006,
        Code0007,
        Code0008,
        All,
        End,
    }

}