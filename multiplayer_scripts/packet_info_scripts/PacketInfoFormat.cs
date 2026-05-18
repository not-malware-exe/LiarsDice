using Godot;
using Godot.Collections;
using System;
using System.Linq;

public class PacketInfoFormat : PacketInfo
{
	// Packet variables ////////////////////////////////////////////

	// Packet variables ////////////////////////////////////////////

	// Packet constructors /////////////////////////////////////////
	public PacketInfoFormat()
	{

		_packetType = PacketType.IdAssignment;
		_flags = (int)ENetPacketPeer.FlagReliable;
	}
	
	public PacketInfoFormat(byte[] data)
	{
		Decode(data);
	}
	// Packet constructors /////////////////////////////////////////

	// Encoding/Decoding ///////////////////////////////////////////
	public override StreamPeerBuffer EncodeBuffer()
	{
		StreamPeerBuffer buffer = base.EncodeBuffer();
		

		return buffer;
	}

	public override void DecodeBuffer(StreamPeerBuffer buffer)
	{
		base.DecodeBuffer(buffer);
		
		
	}
	// Encoding/Decoding ///////////////////////////////////////////
}
