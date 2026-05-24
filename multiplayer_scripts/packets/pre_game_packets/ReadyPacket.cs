using Godot;
using Godot.Collections;
using System;
using System.Linq;

public class ReadyPacket : Packet
{
	// Packet variables ////////////////////////////////////////////

	private byte _clientId = 0;

	public byte GetClientId(){return _clientId;}

	// Packet variables ////////////////////////////////////////////

	// Packet constructors /////////////////////////////////////////
	public ReadyPacket(byte clientId = 0) : base()
	{
		_clientId = clientId;
		_packetType = PacketType.ReadyClient;
		_flags = (int)ENetPacketPeer.FlagUnsequenced;
	}
	
	public ReadyPacket(byte[] data) : base(data){}
	public ReadyPacket(StreamPeerBuffer buffer) : base(buffer){}
	// Packet constructors /////////////////////////////////////////

	// Encoding/Decoding ///////////////////////////////////////////
	public override void EncodeToBuffer(StreamPeerBuffer buffer)
	{
		base.EncodeToBuffer(buffer);
		buffer.PutU8(_clientId);
	}

	public override void DecodeFromBuffer(StreamPeerBuffer buffer)
	{
		base.DecodeFromBuffer(buffer);
		_clientId = buffer.GetU8();
	}
	// Encoding/Decoding ///////////////////////////////////////////
}
