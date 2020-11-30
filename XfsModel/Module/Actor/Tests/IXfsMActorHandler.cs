using System;

namespace Xfs
{
	public interface IXfsMActorHandler
	{
		XfsTask Handle(XfsSession session, XfsEntity entity, object actorMessage);
		Type GetMessageType();
    }
}