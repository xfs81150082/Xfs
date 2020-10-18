using System;
using System.Collections.Generic;
using System.Text;
namespace Xfs
{
    [Serializable]
    public class XfsResponse : XfsParameter
    {
        XfsResponse() { }       
        public XfsResponse(XfsParameter parameter)
        {
            this.EcsId = XfsIdGenerater.GetId();
            this.Back = parameter.Back;
            this.Keys = parameter.Keys;
        }

    }
}