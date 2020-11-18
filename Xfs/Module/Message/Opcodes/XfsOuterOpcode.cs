using System;

namespace Xfs
{
    #region
    [XfsMessage(XfsOuterOpcode.XfsParameter)]
    public partial class XfsParameter : IXfsRequest { }


    [XfsMessage(XfsOuterOpcode.C2G_TestRequest)]
	public partial class C2G_TestRequest : IXfsRequest { }

    [XfsMessage(XfsOuterOpcode.G2C_TestResponse)]
	public partial class G2C_TestResponse : IXfsResponse { }

    [XfsMessage(XfsOuterOpcode.C4G_Ping)]
	public partial class C4G_Ping : IXfsRequest {}

    [XfsMessage(XfsOuterOpcode.G4C_Ping)]
	public partial class G4C_Ping : IXfsResponse {}


    #endregion


}
namespace Xfs
{
    public static partial class XfsOuterOpcode
    {
        #region
        public const ushort XfsParameter = 222;

        public const ushort C2G_TestRequest = 101;
        public const ushort G2C_TestResponse = 102;
       
        public const ushort C4G_Ping = 111;
        public const ushort G4C_Ping = 112;


        #endregion


    }
}