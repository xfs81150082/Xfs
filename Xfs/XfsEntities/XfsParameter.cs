using System;
using System.Collections.Generic;
namespace Xfs
{
    [Serializable]
    public class XfsParameter
    {
        public string EcsId { get; set; }
        public string Back { get; set; }
        public string AtocrId { get; set; }
        public List<string> Keys { get; set; } = new List<string>();
        public TenCode TenCode { get; set; }
        public ElevenCode ElevenCode { get; set; }
        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();

    }

    [Serializable]
    public enum TenCode
    {
        Zero,    /// 链接心跳包    
        Code0001,
        Code0002,
        Code0003,
        Code0004,
        All,
        End,
    }

    [Serializable]
    public enum ElevenCode
    {
        Zero,    /// 链接心跳包    
        Code0001,
        Code0002,
        Code0003,
        Code0004,

        All,
        End,
    }
  

}
