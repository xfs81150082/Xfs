using System;

namespace Xfs
{
    public static partial class XfsOuterOpcode
    {
        #region
        public const int C4G_Ping = 111;
        public const int G4C_Pong = 112;

        public const int C2G_TestRequest = 401;
        public const int G2C_TestResponse = 402;





        public const int Actor_TestRequest = 1101;
        public const int Actor_TestResponse = 1102;


        #endregion


    }
}

namespace Xfs
{
    #region
    [XfsMessage(XfsOuterOpcode.C2G_TestRequest)]
	public partial class C2G_TestRequest : IXfsRequest { }

    [XfsMessage(XfsOuterOpcode.G2C_TestResponse)]
	public partial class G2C_TestResponse : IXfsResponse { }

    [XfsMessage(XfsOuterOpcode.C4G_Ping)]
	public partial class C4G_Ping : IXfsRequest {}

    [XfsMessage(XfsOuterOpcode.G4C_Pong)]
	public partial class G4C_Pong : IXfsResponse {}





    [XfsMessage(XfsOuterOpcode.Actor_TestRequest)]
    public partial class Actor_TestRequest : IXfsActorRequest { }

    [XfsMessage(XfsOuterOpcode.Actor_TestResponse)]
    public partial class Actor_TestResponse : IXfsActorResponse { }


    #endregion


}
