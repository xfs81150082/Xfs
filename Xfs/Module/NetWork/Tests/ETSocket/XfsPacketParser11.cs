using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
	public enum XfsParserState
	{
		PacketSize,
		PacketBody
	}
	public static class XfsPacket
	{
		public const int PacketSizeLength2 = 2;
		public const int PacketSizeLength4 = 4;
		public const int MinPacketSize = 2;
		public const int OpcodeIndex = 0;
		public const int MessageIndex = 2;
	}
	public class XfsPacketParser11
	{
		private readonly XfsCircularBuffer buffer;
		private int packetSize;
		private XfsParserState state;
		public MemoryStream memoryStream;
		private bool isOK;
		private readonly int packetSizeLength;

		public XfsPacketParser11(int packetSizeLength, XfsCircularBuffer buffer, MemoryStream memoryStream)
		{
			this.packetSizeLength = packetSizeLength;
			this.buffer = buffer;
			this.memoryStream = memoryStream;
		}

		public bool Parse()
		{
			if (this.isOK)
			{
				return true;
			}

			bool finish = false;
			while (!finish)
			{
				switch (this.state)
				{
					case XfsParserState.PacketSize:
						if (this.buffer.Length < this.packetSizeLength)
						{
							finish = true;
						}
						else
						{
							this.buffer.Read(this.memoryStream.GetBuffer(), 0, this.packetSizeLength);

							switch (this.packetSizeLength)
							{
								case XfsPacket.PacketSizeLength4:
									this.packetSize = BitConverter.ToInt32(this.memoryStream.GetBuffer(), 0);
									if (this.packetSize > ushort.MaxValue * 16 || this.packetSize < XfsPacket.MinPacketSize)
									{
										throw new Exception($"recv packet size error: {this.packetSize}");
									}
									break;
								case XfsPacket.PacketSizeLength2:
									this.packetSize = BitConverter.ToUInt16(this.memoryStream.GetBuffer(), 0);
									if (this.packetSize > ushort.MaxValue || this.packetSize < XfsPacket.MinPacketSize)
									{
										throw new Exception($"recv packet size error: {this.packetSize}");
									}
									break;
								default:
									throw new Exception("packet size byte count must be 2 or 4!");
							}
							this.state = XfsParserState.PacketBody;
						}
						break;
					case XfsParserState.PacketBody:
						if (this.buffer.Length < this.packetSize)
						{
							finish = true;
						}
						else
						{
							this.memoryStream.Seek(0, SeekOrigin.Begin);
							this.memoryStream.SetLength(this.packetSize);
							byte[] bytes = this.memoryStream.GetBuffer();
							this.buffer.Read(bytes, 0, this.packetSize);
							this.isOK = true;
							this.state = XfsParserState.PacketSize;
							finish = true;
						}
						break;
				}
			}
			return this.isOK;
		}

		public MemoryStream GetPacket()
		{
			this.isOK = false;
			return this.memoryStream;
		}
	}
}
