namespace Xfs
{
    public sealed class XfsScene : XfsEntity
    {
        public int Zone { get; }
        public XfsSceneType SceneType { get; }
        public string Name { get; }

        public XfsScene(long id, int zone, XfsSceneType sceneType, string name)
        {
            this.Id = id;
            this.InstanceId = id;
            this.Zone = zone;
            this.SceneType = sceneType;
            this.Name = name;
            this.IsCreate = true;
        }

        public XfsScene Get(long id)
        {
            return (XfsScene)this.Children?[id];
        }

        public new XfsEntity Domain
        {
            get
            {
                return this.domain;
            }
            set
            {
                this.domain = value;
            }
        }

        public new XfsEntity Parent
        {
            get
            {
                return this.parent;
            }
            set
            {
                if (value == null)
                {
                    this.parent = this;
                    return;
                }
                this.parent = value;
                this.parent.Children.Add(this.Id, this);
            }
        }

    }
}