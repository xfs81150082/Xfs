using System;
using System.Collections.Generic;

namespace Xfs
{
	[XfsObjectSystem]
	public class XfsOpcodeTypeComponentSystem : XfsAwakeSystem<XfsOpcodeTypeComponent>
	{
		public override void Awake(XfsOpcodeTypeComponent self)
		{
			XfsOpcodeTypeComponent.Instance = self;
			self.Load();
		}
	}
	
	[XfsObjectSystem]
	public class XfsOpcodeTypeComponentLoadSystem : XfsLoadSystem<XfsOpcodeTypeComponent>
	{
		public override void Load(XfsOpcodeTypeComponent self)
		{
			self.Load();
		}
	}
	[XfsObjectSystem]
	public class XfsOpcodeTypeComponentXfsDestroySystem : XfsDestroySystem<XfsOpcodeTypeComponent>
	{
		public override void Destroy(XfsOpcodeTypeComponent self)
		{
			XfsOpcodeTypeComponent.Instance = null;
		}
	}

	public class XfsOpcodeTypeComponent : XfsEntity
	{
		public static XfsOpcodeTypeComponent Instance;

		private readonly XfsDoubleMap<int, Type> opcodeTypes = new XfsDoubleMap<int, Type>();

		private readonly Dictionary<int, object> typeMessages = new Dictionary<int, object>();

		public void Load()
		{
			this.opcodeTypes.Clear();
			this.typeMessages.Clear();

			HashSet<Type> types = XfsEventSystem.Instance.GetTypes(typeof(XfsMessageAttribute));
			foreach (Type type in types)
			{
				object[] attrs = type.GetCustomAttributes(typeof(XfsMessageAttribute), false);
				if (attrs.Length == 0)
				{
					continue;
				}

				XfsMessageAttribute messageAttribute = attrs[0] as XfsMessageAttribute;
				if (messageAttribute == null)
				{
					continue;
				}

				this.opcodeTypes.Add(messageAttribute.Opcode, type);
				this.typeMessages.Add(messageAttribute.Opcode, Activator.CreateInstance(type));
			}
		}

		public int GetOpcode(Type type)
		{
			return this.opcodeTypes.GetKeyByValue(type);
		}

		public Type GetType(int opcode)
		{
			return this.opcodeTypes.GetValueByKey(opcode);
		}

	}
}