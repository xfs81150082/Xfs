using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public class XfsMailboxComponentSystem : XfsAwakeSystem<XfsMailboxComponent>
    {
        //public override void BeginInit()
        //{
        //    //self.MailboxType = mailboxType;
        //    //self.Queue.Clear();
        //}

        public override void Awake(XfsMailboxComponent self)
        {
            //self.MailboxType = mailboxType;
            //self.Queue.Clear();
            //self.HandleAsync().Coroutine();
        }
	}
    // <summary>
    /// 挂上这个组件表示该Entity是一个Actor, 接收的消息将会队列处理
    /// </summary>
    public static class MailBoxComponentHelper
	{
        //public static async XfsTask AddLocation(this XfsMailboxComponent self)
        //{
        //    await XfsGame.XfsSence.GetComponent<LocationProxyComponent>().Add(self.Entity.Id, self.Entity.InstanceId);
        //}

        //public static async ETTask RemoveLocation(this MailBoxComponent self)
        //{
        //	await Game.Scene.GetComponent<LocationProxyComponent>().Remove(self.Entity.Id);
        //}

        public static void Add(this XfsMailboxComponent self, XfsActorMessageInfo info)
        {
            self.Queue.Enqueue(info);

            if (self.Tcs == null)
            {
                return;
            }

            var t = self.Tcs;
            self.Tcs = null;
            t.SetResult(self.Queue.Dequeue());
        }

        private static XfsTask<XfsActorMessageInfo> GetAsync(this XfsMailboxComponent self)
        {
            if (self.Queue.Count > 0)
            {
                return XfsTask.FromResult(self.Queue.Dequeue());
            }

            self.Tcs = new XfsTaskCompletionSource<XfsActorMessageInfo>();
            return self.Tcs.Task;
        }

        //public static async XfsVoid HandleAsync(this XfsMailboxComponent self)
        //{
        //    XfsMailboxDispatcherComponent mailboxDispatcherComponent = XfsGame.XfsSence.GetComponent<XfsMailboxDispatcherComponent>();

        //    long instanceId = self.InstanceId;

        //    while (true)
        //    {
        //        if (self.InstanceId != instanceId)
        //        {
        //            return;
        //        }
        //        try
        //        {
        //            XfsActorMessageInfo info = await self.GetAsync();
        //            // 返回null表示actor已经删除,协程要终止
        //            if (info.Message == null)
        //            {
        //                return;
        //            }

        //            // 根据这个mailbox类型分发给相应的处理
        //            await mailboxDispatcherComponent.Handle(self, info);
        //        }
        //        catch (Exception e)
        //        {
        //            //Log.Error(e);
        //        }
        //    }
        //}



    }



}