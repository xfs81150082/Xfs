using System;
using System.ComponentModel;
namespace Xfs
{
    public abstract class XfsComponent : XfsObject, IDisposable
    {
        public long InstanceId { get; private set; }                                /// 身份证号

        private bool isFromPool;
        public bool IsFromPool
        {
            get
            {
                return this.isFromPool;
            }
            set
            {
                this.isFromPool = value;

                if (!this.isFromPool)
                {
                    return;
                }

                if (this.InstanceId == 0)
                {
                    this.InstanceId = XfsIdGeneraterHelper.GenerateInstanceId();
                }
            }
        }  
        public bool IsDisposed
        {
            get
            {
                return this.InstanceId == 0;
            }
        }

        private XfsComponent parent;  
        public XfsComponent Parent
        {
            get
            {
                return this.parent;
            }
            set
            {
                this.parent = value;
            }
        }

        public T GetParent<T>() where T : XfsComponent
        {
            return this.Parent as T;
        }    
        public XfsEntity Entity
        {
            get
            {
                return this.Parent as XfsEntity;
            }
        }

        public XfsComponent()
        {
            this.InstanceId = XfsIdGeneraterHelper.GenerateInstanceId();       
        }

        public override string ToString()
        {
            return XfsJsonHelper.ToString(this);
        }

        ///是否已释放了资源，true时方法都不可用了。
        public virtual void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }         
            // 触发Destroy事件
            XfsGame.EventSystem.Destroy(this);

            XfsGame.EventSystem.Remove(this.InstanceId);

            this.InstanceId = 0;

            if (this.IsFromPool)
            {
                XfsGame.ObjectPool.Recycle(this);
            }
        }       

    }
}
