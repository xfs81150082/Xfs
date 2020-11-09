using System;
using System.Collections.Generic;
using System.Linq;

namespace Xfs
{
    public abstract class XfsEntity : XfsComponentWithId
    {
        private HashSet<XfsComponent> components = new HashSet<XfsComponent>();
        private Dictionary<Type, XfsComponent> componentDict = new Dictionary<Type, XfsComponent>();

        //public Dictionary<string, XfsComponent> Components { get; set; } = new Dictionary<string, XfsComponent>();
        public XfsEntity()
        {
            //XfsObjects.Entities.Add(this);
        }
        protected XfsEntity(long id) : base(id)
        {
        }
        public T GetComponent<T>() where T : XfsComponent
        {
            XfsComponent component;
            if (!this.componentDict.TryGetValue(typeof(T), out component))
            {
                return default(T);
            }
            return (T)component;
        }
        public XfsComponent GetComponent(Type type)
        {
            XfsComponent component;
            if (!this.componentDict.TryGetValue(type, out component))
            {
                return null;
            }
            return component;
        }
        public virtual XfsComponent AddComponent(XfsComponent component)
        {
            Type type = component.GetType();
            if (this.componentDict.ContainsKey(type))
            {
                throw new Exception($"AddComponent, component already exist, id: {this.Id}, component: {type.Name}");
            }

            component.Parent = this;

            this.componentDict.Add(type, component);

            if (component is IXfsSerializeToEntity)
            {
                this.components.Add(component);
            }

            return component;
        }
        public virtual XfsComponent AddComponent(Type type)
        {
            if (this.componentDict.ContainsKey(type))
            {
                throw new Exception($"AddComponent, component already exist, id: {this.Id}, component: {type.Name}");
            }

            XfsComponent component = XfsComponentFactory.CreateWithParent(type, this, this.IsFromPool);

            this.componentDict.Add(type, component);

            if (component is IXfsSerializeToEntity)
            {
                this.components.Add(component);
            }

            return component;
        }
        public virtual T AddComponent<T>() where T : XfsComponent, new()
        {
            Type type = typeof(T);
            if (this.componentDict.ContainsKey(type))
            {
                Console.WriteLine(XfsTimerTool.CurrentTime() + " 此类型组件 {} 已存在！", typeof(T).Name);
                throw new Exception($"AddComponent, component already exist, id: {this.Id}, component: {typeof(T).Name}");
            }

            T component = XfsComponentFactory.CreateWithParent<T>(this, this.IsFromPool);

            this.componentDict.Add(type, component);
            Console.WriteLine(XfsTimerTool.CurrentTime() + " 实例{0},成功添加组件{1}.", this.GetType().Name, typeof(T).Name);

            if (component is IXfsSerializeToEntity)
            {
                this.components.Add(component);
            }

            return component;           
        }
        public virtual T AddComponent<T, P1>(P1 p1) where T : XfsComponent, new()
        {
            Type type = typeof(T);
            if (this.componentDict.ContainsKey(type))
            {
                throw new Exception($"AddComponent, component already exist, id: {this.Id}, component: {typeof(T).Name}");
            }

            T component = XfsComponentFactory.CreateWithParent<T, P1>(this, p1, this.IsFromPool);

            this.componentDict.Add(type, component);

            if (component is IXfsSerializeToEntity)
            {
                this.components.Add(component);
            }

            return component;
        }


        public virtual void RemoveComponent<K>() where K : XfsComponent
        {
            if (this.IsDisposed)
            {
                return;
            }
            Type type = typeof(K);
            XfsComponent component;
            if (!this.componentDict.TryGetValue(type, out component))
            {
                return;
            }

            this.componentDict.Remove(type);
            this.components.Remove(component);

            component.Dispose();
        }

        public virtual void RemoveComponent(Type type)
        {
            if (this.IsDisposed)
            {
                return;
            }
            XfsComponent component;
            if (!this.componentDict.TryGetValue(type, out component))
            {
                return;
            }

            this.componentDict.Remove(type);
            this.components.Remove(component);

            component.Dispose();
        }
        public XfsComponent[] GetComponents()
        {
            return this.componentDict.Values.ToArray();
        }
        public List<Type> GetComponentKeys()
        {
            List<Type> list = componentDict.Keys.ToList();
            return list;
        }
      

        public override void Dispose()
        {
            base.Dispose();
            //XfsObjects.Entities.Remove(this);
            try
            {
                if (componentDict.Count > 0)
                {
                    foreach (var tem in componentDict.Values)
                    {
                        tem.Dispose();
                    }
                    Console.WriteLine(XfsTimerTool.CurrentTime() + " EcsId:" + InstanceId + " TmEntity释放资源");

                }
                componentDict.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(XfsTimerTool.CurrentTime() + " ex: " + ex.Message + " TmEntity释放资源异常...");
            }
        }
    }
}
