using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
namespace Xfs
{
    public abstract class XfsSystem : XfsComponent
    {
        #region TmSystem Update    
        public int ValTime = 50;
        private Timer Timer;
        public XfsSystem()
        {
            Timer = new Timer(ValTime);                                     ///实例化Timer类，在括号里设置间隔时间,单位为毫秒；
            Open();
        }       
        private void Open()
        {
            Timer.Elapsed += new ElapsedEventHandler(OnTimerEvent);      ///到达时间的时候执行事件；
            Timer.Interval = ValTime;                                    ///事件执行间隔时间1000毫秒；
            Timer.Enabled = true;                                        ///是否执行事件System.Timers.Timer.Elapsed；
            Timer.AutoReset = true;                                      ///设置是否循环执行，是执行一次（false）还是一直执行(true)；
        }
        /// 当时间发生的时候需要进行的逻辑处理等    /// 在这里仅仅是一种方式，可以实现这样的方式很多    
        private void OnTimerEvent(object source, ElapsedEventArgs time)
        {
            XfsUpdate();
        }
        public virtual void XfsUpdate() { }
        private void Close()
        {
            Timer.AutoReset = false;                                      ///设置是否循环执行，是执行一次（false）还是一直执行(true)；
            Timer.Enabled = false;                                        ///是否执行事件System.Timers.Timer.Elapsed；
            Timer.Elapsed -= new ElapsedEventHandler(OnTimerEvent);       ///到达时间的时候执行事件；
            Timer.Close();
        }
        #endregion
        #region GetTmEntities
        private Dictionary<string, XfsComponent> Comopnents { get; set; } = new Dictionary<string, XfsComponent>();
        public void AddComponent<T>(T tm) where T : XfsComponent
        {
            XfsComponent com;
            bool have = Comopnents.TryGetValue(typeof(T).Name, out com);
            if (have)
            {
                Console.WriteLine(XfsTimerTool.CurrentTime() + typeof(T).Name + "此类组件已添加");
            }
            else
            {
                Comopnents.Add(typeof(T).Name, tm);
            }
        }
        public ArrayList GetTmEntities()
        {
            ArrayList tms = new ArrayList();
            List<string> coms = new List<string>(Comopnents.Keys);
            if (Comopnents.Count > 0)
            {
                for (int i = 0; i < XfsObjects.Entities.Count; i++)
                {
                    XfsEntity entity = (XfsEntity)XfsObjects.Entities[i];
                    List<string> entC = new List<string>(entity.Components.Keys);
                    if (coms.Except(entC).ToList().Count == 0)
                    {
                        tms.Add(entity);
                    }
                }
            }
            return tms;
        }
        #endregion
        #region Dispose
        public override void Dispose()
        {
            base.Dispose();
            Close();       ///关闭Timer时钟
            Console.WriteLine(XfsTimerTool.CurrentTime() + " EcsId:" + EcsId + " TmSystem释放资源");
        }
        #endregion
    }
}