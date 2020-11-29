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


    #endregion


}
