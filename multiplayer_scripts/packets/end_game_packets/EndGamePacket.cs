using Godot;
using Godot.Collections;
using System;
using System.Linq;

public class EndGamePacket : Packet
{
	// Packet variables ////////////////////////////////////////////
	private byte _winnerId = 0;

	public byte GetWinnerId(){return _winnerId;}
	// Packet variables ////////////////////////////////////////////

	// Packet constructors /////////////////////////////////////////
	public EndGamePacket(byte winnerId) : base()
	{
		_winnerId = winnerId;
		_packetType = PacketType.AssignId;
		_flags = (int)ENetPacketPeer.FlagReliable;
	}
	
	public EndGamePacket(byte[] data) : base(data){}
	public EndGamePacket(StreamPeerBuffer buffer) : base(buffer){}
	// Packet constructors /////////////////////////////////////////

	// Encoding/Decoding ///////////////////////////////////////////
	public override void EncodeToBuffer(StreamPeerBuffer buffer)
	{
		base.EncodeToBuffer(buffer);
		buffer.PutU8(_winnerId);

	}

	public override void DecodeFromBuffer(StreamPeerBuffer buffer)
	{
		base.DecodeFromBuffer(buffer);
		_winnerId = buffer.GetU8();
		
	}
	// Encoding/Decoding ///////////////////////////////////////////
}
