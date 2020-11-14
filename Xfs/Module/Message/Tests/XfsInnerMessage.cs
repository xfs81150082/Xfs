using System.Collections.Generic;

namespace Xfs
{
    #region ///20190613

    [XfsMessage(XfsInnerOpcode.T2M_CreateUnit)]
    public partial class T2M_CreateUnit : IXfsRequest
    {
        public int RpcId { get; set; }

        public int UnitType { get; set; }

        public long RolerId { get; set; }

        public long UnitId { get; set; }

        public long GateSessionId { get; set; }
    }

    [XfsMessage(XfsInnerOpcode.M2T_CreateUnit)]
    public partial class M2T_CreateUnit : IXfsResponse
    {
        public int RpcId { get; set; }

        public int Error { get; set; }

        public string Message { get; set; }

        // 自己的unit id
        public long UnitId { get; set; }

        // 所有的unit
        //public List<UnitInfo> Units = new List<UnitInfo>();
    }

    [XfsMessage(XfsInnerOpcode.M2M_AddUnits)]
    public partial class M2M_AddUnits : IXfsRequest
    {
        public int RpcId { get; set; }

        public long PlayerUnitId { get; set; }

        public int UnitType { get; set; }

        public HashSet<long> PlayerUnitIds { get; set; } = new HashSet<long>();
        public HashSet<long> UnitIds { get; set; } = new HashSet<long>();
    }

    [XfsMessage(XfsInnerOpcode.M2M_RemoveUnits)]
    public partial class M2M_RemoveUnits : IXfsRequest
    {
        public int RpcId { get; set; }

        public long PlayerUnitId { get; set; }

        public int UnitType { get; set; }

        public HashSet<long> PlayerUnitIds { get; set; } = new HashSet<long>();
        public HashSet<long> UnitIds { get; set; } = new HashSet<long>();
    }    

    #endregion




}
