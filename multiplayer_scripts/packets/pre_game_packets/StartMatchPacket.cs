using Godot;
using Godot.Collections;
using System;
using System.Linq;

public class StartMatchPacket : Packet
{
	// Packet variables ////////////////////////////////////////////

	// Packet variables ////////////////////////////////////////////

	// Packet constructors /////////////////////////////////////////
	public StartMatchPacket() : base()
	{

		_packetType = PacketType.StartMatch;
	}
	
	public StartMatchPacket(byte[] data) : base(data){}
	public StartMatchPacket(StreamPeerBuffer buffer) : base(buffer){}
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
