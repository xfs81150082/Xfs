namespace Xfs
{
	// 不需要返回消息
	public interface IXfsActorMessage: IXfsMessage
	{
		long ActorId { get; set; }
	}

	public interface IXfsActorRequest : IXfsRequest , IXfsActorMessage
	{
	}

	public interface IXfsActorResponse : IXfsResponse
	{
	}
}