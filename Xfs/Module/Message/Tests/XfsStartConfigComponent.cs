using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Xfs
{
	[XfsObjectSystem]
	public class XfsStartConfigComponentAwakeSystem : XfsAwakeSystem<XfsStartConfigComponent>
	{
		public override void Awake(XfsStartConfigComponent self)
		{
			self.Awake();
		}
	}

	public class XfsStartConfigComponent : XfsEntity
	{
		public static XfsStartConfigComponent Instance { get; private set; }

		private Dictionary<int, XfsStartConfig> configDict = new Dictionary<int, XfsStartConfig>();

		private Dictionary<int, IPEndPoint> innerAddressDict = new Dictionary<int, IPEndPoint>();

		public XfsStartConfig StartConfig { get; private set; }

		public XfsStartConfig DbConfig { get; private set; }
		public XfsStartConfig GateConfig { get; private set; }

		public XfsStartConfig RealmConfig { get; private set; }

		public XfsStartConfig LocationConfig { get; private set; }

		public List<XfsStartConfig> MapConfigs { get; private set; } = new List<XfsStartConfig>();

		public List<XfsStartConfig> GateConfigs { get; private set; } = new List<XfsStartConfig>();
		public void Awake()
		{
			Instance = this;

			this.GateConfig = new XfsStartConfig() { SenceType = XfsSceneType.Gate, ServerIP = "127.0.0.1", Port = 2001 };
			this.configDict.Add((int)this.GateConfig.SenceType,this.GateConfig);
			this.GateConfigs.Add(this.GateConfig);

			this.RealmConfig = new XfsStartConfig();
			this.LocationConfig = new XfsStartConfig();
			this.MapConfigs.Add(new XfsStartConfig());
		}
		public void Awake(string path, int appId)
		{
			Instance = this;

			//string[] ss = File.ReadAllText(path).Split('\n');
			//foreach (string s in ss)
			//{
			//	string s2 = s.Trim();
			//	if (s2 == "")
			//	{
			//		continue;
			//	}
			//	try
			//	{
			//		//XfsStartConfig startConfig = XfsJsonHelper.FromJson<XfsStartConfig>(s2);
			//		//this.configDict.Add(startConfig.AppId, startConfig);

			//		//InnerConfig innerConfig = startConfig.GetComponent<InnerConfig>();
			//		if (innerConfig != null)
			//		{
			//			//this.innerAddressDict.Add(startConfig.AppId, innerConfig.IPEndPoint);
			//		}

			//		if (startConfig.AppType.Is(XfsSenceType.Realm))
			//		{
			//			this.RealmConfig = startConfig;
			//		}

			//		if (startConfig.AppType.Is(XfsSenceType.Location))
			//		{
			//			this.LocationConfig = startConfig;
			//		}

			//		if (startConfig.AppType.Is(XfsSenceType.DB))
			//		{
			//			this.DBConfig = startConfig;
			//		}

			//		if (startConfig.AppType.Is(XfsSenceType.Map))
			//		{
			//			this.MapConfigs.Add(startConfig);
			//		}

			//		if (startConfig.AppType.Is(XfsSenceType.Gate))
			//		{
			//			this.GateConfigs.Add(startConfig);
			//		}
			//	}
			//	catch (Exception e)
			//	{
			//		//Log.Error($"config错误: {s2} {e}");
			//	}
			//}

			//this.StartConfig = this.Get(appId);
		}

		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}
			base.Dispose();

			Instance = null;
		}

		public XfsStartConfig Get(int id)
		{
			try
			{
				return this.configDict[id];
			}
			catch (Exception e)
			{
				throw new Exception($"not found startconfig: {id}", e);
			}
		}

		public IPEndPoint GetInnerAddress(int id)
		{
			try
			{
				return this.innerAddressDict[id];
			}
			catch (Exception e)
			{
				throw new Exception($"not found innerAddress: {id}", e);
			}
		}

		public XfsStartConfig[] GetAll()
		{
			return this.configDict.Values.ToArray();
		}

		public int Count
		{
			get
			{
				return this.configDict.Count;
			}
		}
	}
}
