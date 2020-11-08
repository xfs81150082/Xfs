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
        Login,
        Gate,
        Node1,
        Node2,
        Node3,
        Node4,
        Game,
        Chat,
        War,
        Db,
        Client,
        All,
        None,
    }

}