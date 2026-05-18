using Godot;
using Godot.Collections;
using System;
using System.Linq;

public class IdAssignmentPacketInfo : PacketInfo
{
	private byte _localId;
	private Array<byte> _remoteIds = [];
	
	public byte GetLocalId(){return _localId;}
	public Array<byte> GetRemoteIds(){return _remoteIds;}

	public IdAssignmentPacketInfo(byte localId, Array<byte> remoteIds)
	{
		_localId = localId;
		_remoteIds = remoteIds;
		_packetType = PacketType.IdAssignment;
		_flags = (int)ENetPacketPeer.FlagReliable;

	}

	public IdAssignmentPacketInfo(byte[] data)
	{
		Decode(data);
	}

	public override StreamPeerBuffer EncodeBuffer()
	{
		StreamPeerBuffer buffer = base.EncodeBuffer();
		buffer.PutU8(_localId);
		
		buffer.PutU8((byte)_remoteIds.Count);
		foreach (byte remoteId in _remoteIds)
			buffer.PutU8(remoteId);

		return buffer;
	}

	public override void DecodeBuffer(StreamPeerBuffer buffer)
	{
		base.DecodeBuffer(buffer);
		_localId = buffer.GetU8();

		byte remoteIdCount = buffer.GetU8();
		for (byte i = 0; i < remoteIdCount; i++)
			_remoteIds.Add(buffer.GetU8());
	}
}
