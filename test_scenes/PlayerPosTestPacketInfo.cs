using Godot;
using Godot.Collections;
using System;
using System.Linq;

public class PlayerPosTestPacketInfo : PacketInfo
{
	private byte _id;
	private Vector2 _pos;

	public byte GetId(){return _id;}
	public Vector2 GetPos(){return _pos;}

	public PlayerPosTestPacketInfo(byte id, Vector2 pos)
	{
		_id = id;
		_pos = pos;
		_packetType = PacketType.PlayerPosTest;
		_flags = (int)ENetPacketPeer.FlagUnsequenced;

	}

	public PlayerPosTestPacketInfo(byte[] data)
	{
		Decode(data);
	}

	public override StreamPeerBuffer EncodeBuffer()
	{
		StreamPeerBuffer buffer = base.EncodeBuffer();
		buffer.PutU8(_id);
		
		buffer.PutFloat(_pos.X);
		buffer.PutFloat(_pos.Y);

		return buffer;
	}

	public override void DecodeBuffer(StreamPeerBuffer buffer)
	{
		base.DecodeBuffer(buffer);
		_id = buffer.GetU8();

		_pos = new Vector2(buffer.GetFloat(),buffer.GetFloat());
	}
}
