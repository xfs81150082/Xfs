namespace Xfs
{
	public interface IXfsMessageDispatcher
	{
		void Dispatch(XfsSession session, int opcode, object message);
	}
}
