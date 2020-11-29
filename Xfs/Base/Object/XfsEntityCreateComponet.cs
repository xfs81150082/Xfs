using System;

namespace Xfs
{
    public partial class XfsEntity
    {
        public static XfsEntity Create(Type type, bool isFromPool)
        {
            XfsEntity component;
            if (isFromPool)
            {
                component = (XfsEntity)XfsObjectPool.Instance.Fetch(type);
            }
            else
            {
                component = (XfsEntity)Activator.CreateInstance(type);
            }
            component.IsFromPool = isFromPool;
            component.IsCreate = true;
            component.Id = 0;
            return component;
        }

        private XfsEntity CreateWithComponentParent(Type type, bool isFromPool = true)
        {
            XfsEntity component = Create(type, isFromPool);

            component.Id = parent.Id;
            component.ComponentParent = parent;

            XfsEventSystem.Instance.Awake(component);
            return component;
        }

        private T CreateWithComponentParent<T>(bool isFromPool = true) where T : XfsEntity
        {
            Type type = typeof(T);
            XfsEntity component = Create(type, isFromPool);

            component.Id = this.Id;
            component.ComponentParent = this;

            XfsEventSystem.Instance.Awake(component);
            return (T)component;
        }

        private T CreateWithComponentParent<T, A>(A a, bool isFromPool = true) where T : XfsEntity
        {
            Type type = typeof(T);
            XfsEntity component = Create(type, isFromPool);

            component.Id = this.Id;
            component.ComponentParent = this;

            XfsEventSystem.Instance.Awake(component, a);
            return (T)component;
        }

        private T CreateWithComponentParent<T, A, B>(A a, B b, bool isFromPool = true) where T : XfsEntity
        {
            Type type = typeof(T);
            XfsEntity component = Create(type, isFromPool);

            component.Id = this.Id;
            component.ComponentParent = this;

            XfsEventSystem.Instance.Awake(component, a, b);
            return (T)component;
        }

        private T CreateWithComponentParent<T, A, B, C>(A a, B b, C c, bool isFromPool = true) where T : XfsEntity
        {
            Type type = typeof(T);
            XfsEntity component = Create(type, isFromPool);

            component.Id = this.Id;
            component.ComponentParent = this;

            XfsEventSystem.Instance.Awake(component, a, b, c);
            return (T)component;
        }
    }
}