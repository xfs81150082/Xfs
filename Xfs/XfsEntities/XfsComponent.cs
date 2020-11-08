﻿using System;
using System.ComponentModel;
namespace Xfs
{
    public abstract class XfsComponent : XfsObject, IDisposable
    {
        public string EcsId { get; set; }           /// 身份证号
        public XfsEntity Parent { get; set; }
        public XfsComponent()
        {
            this.EcsId = XfsIdGenerater.GetId();
            XfsComponent component;
            XfsObjects.Components.TryGetValue(EcsId, out component);
            if (component != null)
            {
                EcsId += 2600;
            }
            XfsObjects.Components.Add(EcsId, this);
            BeginInit();
            XfsAwake();
            EndInit();
        }
        public virtual void BeginInit() { }
        public virtual void XfsAwake() { }
        public virtual void EndInit() { }
        public virtual void OnTransferParameter(object sender, XfsParameter parameter) { }
        #region Dispose
        ///是否已释放了资源，true时方法都不可用了。
        public bool IsDisposed { get; private set; } = false;
        ///供程序员显式调用的Dispose方法
        public virtual void Dispose()
        {
            if (!IsDisposed)
            {
                XfsObjects.Components.Remove(EcsId);
                XfsDispose();   /// 为继承类释放时使用，用抽象方法
                GC.SuppressFinalize(this); ///GC不用二次释放this资源   

                try
                {
                    if (Parent != null)
                    {
                        XfsComponent tm;
                        Parent.Components.TryGetValue(this.GetType().Name, out tm);
                        if (tm != null)
                        {
                            Parent.Components.Remove(this.GetType().Name);
                        }
                        Parent = null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(XfsTimerTool.CurrentTime() + " ex:" + ex.Message + " TmComponent释放资源异常...");
                }

                this.IsDisposed = true;
            }
        }
        /// 为继承类释放时使用(Note:这儿为什么要写成虚方法呢？)
        /// 1. 为了让派生类清理自已的资源。将销毁和析构的共同工作提取出来，并让派生类也可以释放其自已分配的资源。
        /// 2. 为派生类提供了根据Dispose()或终结器的需要进行资源清理的必要入口。
        public virtual void XfsDispose() { }
        #endregion

    }
}
