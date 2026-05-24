using Godot;
using Godot.Collections;
using System;
using System.Linq;

public class PlayerPosTestPacket : Packet
{
	// Packet variables ////////////////////////////////////////////
	private byte _id;
	private Vector2 _pos;

	public byte GetId(){return _id;}
	public Vector2 GetPos(){return _pos;}
	// Packet variables ////////////////////////////////////////////

	// Packet constructors /////////////////////////////////////////
	public PlayerPosTestPacket(byte id, Vector2 pos)
	{
		_id = id;
		_pos = pos;
		_packetType = PacketType.PlayerPosTest;
		_flags = (int)ENetPacketPeer.FlagUnsequenced;

	}

	public PlayerPosTestPacket(byte[] data)
	{
		Decode(data);
	}
	// Packet constructors /////////////////////////////////////////

	// Encoding/Decoding ///////////////////////////////////////////
	public override void EncodeToBuffer(StreamPeerBuffer buffer)
	{
		base.EncodeToBuffer(buffer);
		buffer.PutU8(_id);
		
		buffer.PutFloat(_pos.X);
		buffer.PutFloat(_pos.Y);
	}

	public override void DecodeFromBuffer(StreamPeerBuffer buffer)
	{
		base.DecodeFromBuffer(buffer);
		_id = buffer.GetU8();

		_pos = new Vector2(buffer.GetFloat(),buffer.GetFloat());
	}
	// Encoding/Decoding ///////////////////////////////////////////
}
