using Godot;
using Godot.Collections;
using System;
using System.Linq;

public class StartRoundPacket : Packet
{
	// Packet variables ////////////////////////////////////////////

	// Packet variables ////////////////////////////////////////////

	// Packet constructors /////////////////////////////////////////
	public StartRoundPacket() : base()
	{

		_packetType = PacketType.StartRound;
	}
	
	public StartRoundPacket(byte[] data) : base(data){}
	public StartRoundPacket(StreamPeerBuffer buffer) : base(buffer){}
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
