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
		int Error { get; set; }
		string Message { get; set; }
		int RpcId { get; set; }
	}
	public class XfsResponseMessage : IXfsResponse
	{
		public int Error { get; set; }
		public string Message { get; set; }
		public int RpcId { get; set; }
	}


}
