using Godot;
using Godot.Collections;
using System;
using System.Linq;

public class PacketFormat : Packet
{
	// Packet variables ////////////////////////////////////////////

	// Packet variables ////////////////////////////////////////////

	// Packet constructors /////////////////////////////////////////
	public PacketFormat() : base()
	{

		_packetType = PacketType.AssignId;
		_flags = (int)ENetPacketPeer.FlagReliable;
	}
	
	public PacketFormat(byte[] data) : base(data){}
	public PacketFormat(StreamPeerBuffer buffer) : base(buffer){}
	// Packet constructors /////////////////////////////////////////

	// Encoding/Decoding ///////////////////////////////////////////
	public override void EncodeToBuffer(StreamPeerBuffer buffer)
	{
		base.EncodeToBuffer(buffer);


	}

	public override void DecodeFromBuffer(StreamPeerBuffer buffer)
	{
		base.DecodeFromBuffer(buffer);
		
		
	}
	// Encoding/Decoding ///////////////////////////////////////////
}
