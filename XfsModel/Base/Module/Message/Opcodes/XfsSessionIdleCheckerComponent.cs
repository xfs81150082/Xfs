namespace Xfs
{

    public class XfsSessionIdleCheckerComponentAwakeSystem : XfsAwakeSystem<XfsSessionIdleCheckerComponent, int, int, int>
    {
        public override void Awake(XfsSessionIdleCheckerComponent self, int checkInteral, int recvMaxIdleTime, int sendMaxIdleTime)
        {
            self.CheckInterval = checkInteral;
            self.RecvMaxIdleTime = recvMaxIdleTime;
            self.SendMaxIdleTime = sendMaxIdleTime;

            self.RepeatedTimer = XfsTimerComponent.Instance.NewRepeatedTimer(checkInteral, self.Check);
        }
    }


    public class SessionIdleCheckerComponentLoadSystem : XfsLoadSystem<XfsSessionIdleCheckerComponent>
    {
        public override void Load(XfsSessionIdleCheckerComponent self)
        {
            XfsRepeatedTimer repeatedTimer = XfsTimerComponent.Instance.GetRepeatedTimer(self.RepeatedTimer);
            if (repeatedTimer != null)
            {
                repeatedTimer.Callback = self.Check;
            }
        }
    }


    public class SessionIdleCheckerComponentDestroySystem : XfsDestroySystem<XfsSessionIdleCheckerComponent>
    {
        public override void Destroy(XfsSessionIdleCheckerComponent self)
        {
            self.CheckInterval = 0;
            self.RecvMaxIdleTime = 0;
            self.SendMaxIdleTime = 0;
            XfsTimerComponent.Instance.Remove(self.RepeatedTimer);
            self.RepeatedTimer = 0;
        }
    }

    public static class SessionIdleCheckerComponentSystem
    {
        public static void Check(this XfsSessionIdleCheckerComponent self, bool isTimeOut)
        {
            XfsSession session = self.GetParent<XfsSession>();
            long timeNow = XfsTimeHelper.Now();
            if (timeNow - session.LastRecvTime < self.RecvMaxIdleTime && timeNow - session.LastSendTime < self.SendMaxIdleTime)
            {
                return;
            }

            session.Error = XfsErrorCode.ERR_SessionSendOrRecvTimeout;
            session.Dispose();
        }
    }

    public class XfsSessionIdleCheckerComponent : XfsEntity
    {
        public int CheckInterval;
        public int RecvMaxIdleTime;
        public int SendMaxIdleTime;
        public long RepeatedTimer;
    }
}