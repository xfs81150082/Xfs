using System;
using System.Collections;
using System.Collections.Generic;

namespace Xfs
{/// <summary>
 /// 一般使用事件名+变量名
 /// </summary>
	public enum XfsEnvKey
	{
		ChannelError,
		Call,
		Send,
		Recv,
		Lingin,

	}
	public class XfsEnv
	{
		private Dictionary<XfsEnvKey, object> values = new Dictionary<XfsEnvKey, object>();

		public object this[XfsEnvKey key]
		{
			get
			{
				return this.values[key];
			}
			set
			{
				if (this.values == null)
				{
					this.values = new Dictionary<XfsEnvKey, object>();
				}
				this.values[key] = value;
			}
		}

		public T Get<T>(XfsEnvKey key)
		{
			if (this.values == null || !this.values.ContainsKey(key))
			{
				return default(T);
			}
			object value = values[key];
			try
			{
				return (T)value;
			}
			catch (InvalidCastException e)
			{
				throw new Exception($"不能把{value.GetType()}转换为{typeof(T)}", e);
			}
		}

		public void Set(XfsEnvKey key, object obj)
		{
			if (this.values == null)
			{
				this.values = new Dictionary<XfsEnvKey, object>();
			}
			this.values[key] = obj;
		}

		public bool ContainKey(XfsEnvKey key)
		{
			if (this.values == null)
			{
				return false;
			}
			return this.values.ContainsKey(key);
		}

		public void Remove(XfsEnvKey key)
		{
			if (this.values == null)
			{
				return;
			}
			this.values.Remove(key);
			if (this.values.Count == 0)
			{
				this.values = null;
			}
		}

		public void Add(XfsEnvKey key, object value)
		{
			if (this.values == null)
			{
				this.values = new Dictionary<XfsEnvKey, object>();
			}
			this.values[key] = value;
		}

		public IEnumerator GetEnumerator()
		{
			return this.values.GetEnumerator();
		}
	}
}