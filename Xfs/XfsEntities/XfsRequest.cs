using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
namespace Xfs
{
    [Serializable]
    public class XfsRequest : XfsParameter
    {
        public XfsRequest()
        {
            this.EcsId = XfsIdGenerater.GetId();
            this.Back = "";

        }
        public void BackInit()
        {
            this.EcsId = XfsIdGenerater.GetId();
            this.Back = this.EcsId;
        }

    }
}