using System;

namespace Xfs
{
	public interface IXfsMHandler
	{
		void Handle(XfsSession session, object message);
		Type GetMessageType();
	}
}