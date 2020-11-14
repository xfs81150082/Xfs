using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public interface IXfsMessage
    {
    }
	public interface IXfsRequest : IXfsMessage
	{
		int RpcId { get; set; }
	}
	public interface IXfsResponse : IXfsMessage
	{
		int RpcId { get; set; }
		int Error { get; set; }
		string Message { get; set; }
	}
	public class XfsResponseMessage : IXfsResponse
	{
		public int RpcId { get; set; }
		public int Error { get; set; }
		public string Message { get; set; }
	}


}
