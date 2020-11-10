using System;
using System.ComponentModel;
namespace Xfs
{
    public abstract class XfsComponent : XfsObject, IDisposable
    {
        public long InstanceId { get; private set; }     /// 身份证号0
        //public string EcsId { get; set; }                /// 身份证号

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
                    this.InstanceId = XfsIdGenerater.GenerateInstanceId();
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
            this.InstanceId = XfsIdGenerater.GenerateInstanceId();

         
            XfsAwake();
            EndInit();
        }
        public virtual void XfsAwake() { }
        public virtual void OnTransferParameter(object sender, XfsParameter parameter) { }
        #region Dispose
        ///是否已释放了资源，true时方法都不可用了。
        //public virtual void Dispose()
        //{
        //    if (!IsDisposed)
        //    {
        //        XfsObjects.Components.Remove(EcsId);
        //        XfsDispose();   /// 为继承类释放时使用，用抽象方法
        //        GC.SuppressFinalize(this); ///GC不用二次释放this资源   

        //        try
        //        {
        //            if (Parent != null)
        //            {
        //                XfsComponent tm;
        //                (Parent as XfsEntity)..TryGetValue(this.GetType().Name, out tm);
        //                if (tm != null)
        //                {
        //                    (Parent as XfsEntity).Components.Remove(this.GetType().Name);
        //                }
        //                Parent = null;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(XfsTimerTool.CurrentTime() + " ex:" + ex.Message + " TmComponent释放资源异常...");
        //        }

        //        this.IsDisposed = true;
        //    }
        //}

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
            else
            {

            }
        }
        /// 为继承类释放时使用(Note:这儿为什么要写成虚方法呢？)
        /// 1. 为了让派生类清理自已的资源。将销毁和析构的共同工作提取出来，并让派生类也可以释放其自已分配的资源。
        /// 2. 为派生类提供了根据Dispose()或终结器的需要进行资源清理的必要入口。
        #endregion

        public override void EndInit()
        {
            //XfsGame.EventsSystem.Deserialize(this);
        }

        public override string ToString()
        {
            return XfsJsonHelper.ToString(this);
        }


    }
}
