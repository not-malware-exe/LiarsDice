using Godot;
using Godot.Collections;
using System;
using System.Linq;

public class BlockClientConnectionPacket : Packet
{
	// Packet variables ////////////////////////////////////////////
	public enum ReasonType : byte
	{
		RoomFull = 0,
		GameStarted = 1
	}

	ReasonType _reasonType = ReasonType.RoomFull;


	// Packet variables ////////////////////////////////////////////

	// Packet constructors /////////////////////////////////////////
	public BlockClientConnectionPacket(ReasonType reasonType = ReasonType.RoomFull) : base()
	{
		_reasonType = reasonType;
		_packetType = PacketType.AssignId;
		_flags = (int)ENetPacketPeer.FlagUnsequenced;
	}
	
	public BlockClientConnectionPacket(byte[] data) : base(data){}
	public BlockClientConnectionPacket(StreamPeerBuffer buffer) : base(buffer){}
	// Packet constructors /////////////////////////////////////////

	// Encoding/Decoding ///////////////////////////////////////////
	public override void EncodeToBuffer(StreamPeerBuffer buffer)
	{
		base.EncodeToBuffer(buffer);
		buffer.PutU8((byte)_reasonType);

	}

	public override void DecodeFromBuffer(StreamPeerBuffer buffer)
	{
		base.DecodeFromBuffer(buffer);
		_reasonType = (ReasonType)buffer.GetU8();
		
	}
	// Encoding/Decoding ///////////////////////////////////////////
}
