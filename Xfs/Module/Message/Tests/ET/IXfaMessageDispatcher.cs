namespace Xfs
{
	public interface IXfsMessageDispatcher
	{
		void Dispatch(XfsSession session, ushort opcode, object message);
	}
}
