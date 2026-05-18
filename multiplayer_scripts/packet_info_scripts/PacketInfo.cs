using Godot;
using Godot.Collections;
using System;
using System.Linq;

public class PacketInfo
{
	public enum PacketType : byte
	{
    	IdAssignment = 0,
		PlayerPosTest = 1
	}

	protected PacketType _packetType;
	protected int _flags;

	public byte[] Encode()
	{
		return EncodeBuffer().DataArray;
	}

	public virtual StreamPeerBuffer EncodeBuffer()
	{
		StreamPeerBuffer buffer = new StreamPeerBuffer();
		buffer.PutU8((byte)_packetType);

		return buffer;
	}

	public void Decode(byte[] packetData)
	{
		StreamPeerBuffer buffer = new StreamPeerBuffer();
		buffer.DataArray = packetData;

		DecodeBuffer(buffer);
	}

	public virtual void DecodeBuffer(StreamPeerBuffer buffer)
	{
		_packetType = (PacketType)buffer.GetU8();
	}

	public void Send(ENetPacketPeer target)
	{
		target.Send(0, Encode(), _flags);
	}
	
	public void BroadCast(ENetConnection server)
	{
		server.Broadcast(0, Encode(), _flags);
	}
}
