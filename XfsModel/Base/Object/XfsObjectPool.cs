using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
	public class XfsComponentQueue : XfsObject
	{
		public string TypeName { get; }

		private readonly Queue<XfsObject> queue = new Queue<XfsObject>();

		public XfsComponentQueue(string typeName)
		{
			this.TypeName = typeName;
		}

		public void Enqueue(XfsObject entity)
		{
			this.queue.Enqueue(entity);
		}

		public XfsObject Dequeue()
		{
			return this.queue.Dequeue();
		}

		public XfsObject Peek()
		{
			return this.queue.Peek();
		}

		public Queue<XfsObject> Queue
		{
			get
			{
				return this.queue;
			}
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
			while (this.queue.Count > 0)
			{
				XfsObject component = this.queue.Dequeue();
				component.Dispose();
			}
		}
	}

	public class XfsObjectPool : XfsObject
	{
		private static XfsObjectPool instance;

		public static XfsObjectPool Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new XfsObjectPool();
				}

				return instance;
			}
		}
		public string Name { get; set; }

		private readonly Dictionary<Type, XfsComponentQueue> dictionary = new Dictionary<Type, XfsComponentQueue>();

		public XfsObject Fetch(Type type)
		{
			XfsObject obj;
			if (!this.dictionary.TryGetValue(type, out XfsComponentQueue queue))
			{
				obj = (XfsObject)Activator.CreateInstance(type);
			}
			else if (queue.Count == 0)
			{
				obj = (XfsObject)Activator.CreateInstance(type);
			}
			else
			{
				obj = queue.Dequeue();
			}
			return obj;
		}

		public T Fetch<T>() where T : XfsObject
		{
			T t = (T)this.Fetch(typeof(T));
			return t;
		}

		public void Recycle(XfsObject obj)
		{
			Type type = obj.GetType();
			XfsComponentQueue queue;
			if (!this.dictionary.TryGetValue(type, out queue))
			{
				queue = new XfsComponentQueue(type.Name);
				this.dictionary.Add(type, queue);
			}
			queue.Enqueue(obj);
		}

		public override void Dispose()
		{
			foreach (var kv in this.dictionary)
			{
				kv.Value.Dispose();
			}
			this.dictionary.Clear();
			instance = null;
		}


	}
}
