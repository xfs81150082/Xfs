namespace Xfs
{
	public struct XfsActorTask
	{
		public IXfsActorRequest ActorRequest;
		
		public XfsTaskCompletionSource<IXfsActorLocationResponse> Tcs;

		public XfsActorTask(IXfsActorLocationMessage actorRequest)
		{
			this.ActorRequest = actorRequest;
			this.Tcs = null;
		}
		
		public XfsActorTask(IXfsActorLocationRequest actorRequest, XfsTaskCompletionSource<IXfsActorLocationResponse> tcs)
		{
			this.ActorRequest = actorRequest;
			this.Tcs = tcs;
		}
	}
}