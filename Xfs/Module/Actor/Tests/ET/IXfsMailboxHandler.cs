namespace Xfs
{
	public interface IXfsMailboxHandler
	{
		XfsTask Handle(XfsSession session, XfsEntity entity, object actorMessage);
	}
}