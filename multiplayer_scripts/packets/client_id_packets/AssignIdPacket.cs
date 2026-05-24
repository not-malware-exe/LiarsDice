using Godot;
using Godot.Collections;
using System;
using System.Linq;

public class AssignIdPacket : Packet
{
	// Packet variables ////////////////////////////////////////////
	private byte _localId;
	
	public byte GetLocalId(){return _localId;}
	// Packet variables ////////////////////////////////////////////

	// Packet constructors /////////////////////////////////////////
	public AssignIdPacket(byte localId) : base()
	{
		_localId = localId;
		_packetType = PacketType.AssignId;

	}
	
	public AssignIdPacket(byte[] data) : base(data){}
	public AssignIdPacket(StreamPeerBuffer buffer) : base(buffer){}
	// Packet constructors /////////////////////////////////////////

	// Encoding/Decoding ///////////////////////////////////////////
	public override void EncodeToBuffer(StreamPeerBuffer buffer)
	{
		base.EncodeToBuffer(buffer);
		buffer.PutU8(_localId);
	}

	public override void DecodeFromBuffer(StreamPeerBuffer buffer)
	{
		base.DecodeFromBuffer(buffer);
		_localId = buffer.GetU8();
	}
	// Encoding/Decoding ///////////////////////////////////////////
}
