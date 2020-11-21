namespace Xfs
{
    public sealed class XfsSence : XfsEntity
    {
        public XfsSenceType Type { get; set; }
        public XfsSence() { }
        public string Name { get; set; }
        public XfsSence(long id) : base(id)
        {
        }
        public XfsSence(XfsSenceType type)
        {
            this.Type = type;           
        }
    }
    public enum XfsSenceType
    {
        Client,
        Login,
        Chat,
        War,
        All,

        None,
        Manager,
        Realm,
        Gate,
        Http,
        Db,
        Location,
        Map,

        BenchmarkWebsocketServer = 1 << 26,
        BenchmarkWebsocketClient = 1 << 27,
        Robot = 1 << 28,
        Benchmark = 1 << 29,
        // 客户端Hotfix层
        ClientH = 1 << 30,
        // 客户端Model层
        ClientM = 1 << 31,

        // 7
        AllServer = Manager | Realm | Gate | Http | Db | Location | Map | BenchmarkWebsocketServer
 
        //None = 0,
        //Manager = 1,
        //Realm = 1 << 1,
        //Gate = 1 << 2,
        //Http = 1 << 3,
        //DB = 1 << 4,
        //Location = 1 << 5,
        //Map = 1 << 6,

        //BenchmarkWebsocketServer = 1 << 26,
        //BenchmarkWebsocketClient = 1 << 27,
        //Robot = 1 << 28,
        //Benchmark = 1 << 29,
        //// 客户端Hotfix层
        //ClientH = 1 << 30,
        //// 客户端Model层
        //ClientM = 1 << 31,

        //// 7
        //AllServer = Manager | Realm | Gate | Http | DB | Location | Map | BenchmarkWebsocketServer
    }

}