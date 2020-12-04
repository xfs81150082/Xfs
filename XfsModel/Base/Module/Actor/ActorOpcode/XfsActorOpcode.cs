using System;
using System.Collections.Generic;
using System.Text;

namespace Xfs
{
    public static partial class XfsActorOpcode
    {
        public const int Actor_TestRequest = 1101;
        public const int Actor_TestResponse = 1102;




    }
}

namespace Xfs
{
    #region
    [XfsMessage(XfsActorOpcode.Actor_TestRequest)]
    public partial class Actor_TestRequest : IXfsActorRequest { }

    [XfsMessage(XfsActorOpcode.Actor_TestResponse)]
    public partial class Actor_TestResponse : IXfsActorResponse { }



    #endregion




}

