using System;
using System.Collections.Generic;
namespace Xfs
{
    public abstract class XfsEntity : XfsComponent
    {
        public Dictionary<string, XfsComponent> Components { get; set; } = new Dictionary<string, XfsComponent>();
        public XfsEntity()
        {
            XfsObjects.Entities.Add(this);
        }
        public T GetComponent<T>() where T : class
        {
            string name = typeof(T).Name;
            XfsComponent tem;
            Components.TryGetValue(name, out tem);
            if (tem != null)
            {                
                return tem as T;
            }
            else
            {
                Console.WriteLine(XfsTimerTool.CurrentTime() + tem.GetType().Name + "此类型组件不存在！");
                return null;
            }
        }
        public void AddComponent<T>(T tm) where T : XfsComponent
        {
            XfsComponent tem;
            Components.TryGetValue(typeof(T).Name, out tem);
            if (tem == null)
            {
                tm.Parent = this;
                Components.Add(typeof(T).Name, tm);
                Console.WriteLine(XfsTimerTool.CurrentTime() + " 实例{0},成功添加组件{1}.", this.GetType().Name, typeof(T).Name);
            }
            else
            {
                Console.WriteLine(XfsTimerTool.CurrentTime() + " 此类型组件 {} 已存在！", typeof(T).Name);
            }
        }
        public void RemoveComponent<T>()
        {
            string name = typeof(T).Name;
            XfsComponent tem;
            Components.TryGetValue(name, out tem);
            if (tem != null)
            {
                Components.Remove(name);
                tem.Parent = null;
                Console.WriteLine(XfsTimerTool.CurrentTime() + " 实例 {0} 删除组件 {1}", this.GetType().Name, typeof(T).Name);
                tem.Dispose();
            }
            else
            {
                Console.WriteLine(XfsTimerTool.CurrentTime() + name + "此类型组件不存在！");
            }
        }
        public override void Dispose()
        {
            base.Dispose();
            XfsObjects.Entities.Remove(this);
            try
            {
                if (Components.Count > 0)
                {
                    foreach (var tem in Components.Values)
                    {
                        tem.Dispose();
                    }
                    Console.WriteLine(XfsTimerTool.CurrentTime() + " EcsId:" + EcsId + " TmEntity释放资源");

                }
                Components.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(XfsTimerTool.CurrentTime() + " ex: " + ex.Message + " TmEntity释放资源异常...");
            }
        }
    }
}
