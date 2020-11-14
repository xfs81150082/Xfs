using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
	public class XfsComponentQueue : XfsComponent
	{
		public string TypeName { get; }

		private readonly Queue<XfsComponent> queue = new Queue<XfsComponent>();

		public XfsComponentQueue(string typeName)
		{
			this.TypeName = typeName;
		}

		public void Enqueue(XfsComponent component)
		{
			component.Parent = this;
			this.queue.Enqueue(component);
		}

		public XfsComponent Dequeue()
		{
			return this.queue.Dequeue();
		}

		public XfsComponent Peek()
		{
			return this.queue.Peek();
		}
		public int Count
		{
			get
			{
				return this.queue.Count;
			}
		}
		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}
			base.Dispose();

			while (this.queue.Count > 0)
			{
				XfsComponent component = this.queue.Dequeue();
				component.IsFromPool = false;
				component.Dispose();
			}
		}
	}

	public class XfsObjectPool : XfsComponent
    {
		public string Name { get; set; }

		private readonly Dictionary<Type, XfsComponentQueue> dictionary = new Dictionary<Type, XfsComponentQueue>();

		public XfsComponent Fetch(Type type)
		{
			XfsComponent obj;
			if (!this.dictionary.TryGetValue(type, out XfsComponentQueue queue))
			{
				obj = (XfsComponent)Activator.CreateInstance(type);
			}
			else if (queue.Count == 0)
			{
				obj = (XfsComponent)Activator.CreateInstance(type);
			}
			else
			{
				obj = queue.Dequeue();
			}

			obj.IsFromPool = true;
			return obj;
		}

		public T Fetch<T>() where T : XfsComponent
		{
			T t = (T)this.Fetch(typeof(T));
			return t;
		}

		public void Recycle(XfsComponent obj)
		{
			obj.Parent = this;
			Type type = obj.GetType();
			XfsComponentQueue queue;
			if (!this.dictionary.TryGetValue(type, out queue))
			{
				queue = new XfsComponentQueue(type.Name);
				queue.Parent = this;
				this.dictionary.Add(type, queue);
			}
			queue.Enqueue(obj);
		}

		public void Clear()
		{
			foreach (var kv in this.dictionary)
			{
				kv.Value.IsFromPool = false;
				kv.Value.Dispose();
			}
			this.dictionary.Clear();
		}


	}



}
