using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
	public enum DLLType
	{
		Xfs,
		XfsConsoleClient,
		XfsConsoleTest,
		XfsDbServer,
		XfsGateSever,
		XfsWinFormsClient,
		XfsWinFormsServer,
		//Model,
		//Hotfix,
		Editor,
	}
	public sealed class XfsEventSystem
    {
		private readonly Dictionary<long, XfsComponent> allComponents = new Dictionary<long, XfsComponent>();

		private readonly Dictionary<DLLType, Assembly> assemblies = new Dictionary<DLLType, Assembly>();
		private readonly XfsUnOrderMultiMap<Type, Type> types = new XfsUnOrderMultiMap<Type, Type>();

		private readonly Dictionary<string, List<IXfsEvent>> allEvents = new Dictionary<string, List<IXfsEvent>>();

		private readonly XfsUnOrderMultiMap<Type, IXfsAwakeSystem> awakeSystems = new XfsUnOrderMultiMap<Type, IXfsAwakeSystem>();

		private readonly XfsUnOrderMultiMap<Type, IXfsStartSystem> startSystems = new XfsUnOrderMultiMap<Type, IXfsStartSystem>();

		private readonly XfsUnOrderMultiMap<Type, IXfsDestroySystem> destroySystems = new XfsUnOrderMultiMap<Type, IXfsDestroySystem>();

		private readonly XfsUnOrderMultiMap<Type, IXfsLoadSystem> loadSystems = new XfsUnOrderMultiMap<Type, IXfsLoadSystem>();

		private readonly XfsUnOrderMultiMap<Type, IXfsUpdateSystem> updateSystems = new XfsUnOrderMultiMap<Type, IXfsUpdateSystem>();

		private readonly XfsUnOrderMultiMap<Type, IXfsLateUpdateSystem> lateUpdateSystems = new XfsUnOrderMultiMap<Type, IXfsLateUpdateSystem>();

		private readonly XfsUnOrderMultiMap<Type, IXfsChangeSystem> changeSystems = new XfsUnOrderMultiMap<Type, IXfsChangeSystem>();

		private readonly XfsUnOrderMultiMap<Type, IXfsDeserializeSystem> deserializeSystems = new XfsUnOrderMultiMap<Type, IXfsDeserializeSystem>();

		private Queue<long> updates = new Queue<long>();
		private Queue<long> updates2 = new Queue<long>();

		private readonly Queue<long> starts = new Queue<long>();

		private Queue<long> loaders = new Queue<long>();
		private Queue<long> loaders2 = new Queue<long>();

		private Queue<long> lateUpdates = new Queue<long>();
		private Queue<long> lateUpdates2 = new Queue<long>();

	}
}
