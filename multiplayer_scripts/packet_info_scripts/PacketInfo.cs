using Godot;
using Godot.Collections;
using System;
using System.Linq;

public class PacketInfo
{
	// Packet variables ////////////////////////////////////////////
	public enum PacketType : byte
	{
    	IdAssignment = 0,
		PlayerPosTest = 1
	}

	protected PacketType _packetType;
	protected int _flags;
	// Packet variables ////////////////////////////////////////////
	
	// Encoding/Decoding ///////////////////////////////////////////
	// encodes data into byte array
	public byte[] Encode()
	{
		return EncodeBuffer().DataArray;
	}

	// encodes data with StreamPeerBuffer
	public virtual StreamPeerBuffer EncodeBuffer()
	{
		StreamPeerBuffer buffer = new StreamPeerBuffer();
		buffer.PutU8((byte)_packetType);

		return buffer;
	}

	// decodes data into byte array
	public void Decode(byte[] packetData)
	{
		StreamPeerBuffer buffer = new StreamPeerBuffer();
		buffer.DataArray = packetData;

		DecodeBuffer(buffer);
	}

	// decodes data with StreamPeerBuffer
	public virtual void DecodeBuffer(StreamPeerBuffer buffer)
	{
		_packetType = (PacketType)buffer.GetU8();
	}
	// Encoding/Decoding ///////////////////////////////////////////

	// packet sending //////////////////////////////////////////////
	// sends data to specific peer
	public void Send(ENetPacketPeer target)
	{
		target.Send(0, Encode(), _flags);
	}
	
	// sends data to all peers
	public void BroadCast(ENetConnection server)
	{
		server.Broadcast(0, Encode(), _flags);
	}
	// packet sending //////////////////////////////////////////////
}
