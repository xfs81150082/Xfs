﻿using System;
using System.IO;

namespace Xfs
{
	public interface IXfsMessagePacker
	{
		byte[] SerializeTo(object obj);
		void SerializeTo(object obj, MemoryStream stream);
		object DeserializeFrom(Type type, byte[] bytes, int index, int count);
		object DeserializeFrom(object instance, byte[] bytes, int index, int count);
		object DeserializeFrom(Type type, MemoryStream stream);
		object DeserializeFrom(object instance, MemoryStream stream);
		
	}
}
