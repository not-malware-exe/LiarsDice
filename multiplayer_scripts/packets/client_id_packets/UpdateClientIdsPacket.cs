using Godot;
using Godot.Collections;
using System;
using System.Linq;

public class UpdateClientIdsPacket : Packet
{
	// Packet variables ////////////////////////////////////////////
	private Array<byte> _clientIds = [];
	
	public Array<byte> GetClientIds(){return _clientIds;}
	// Packet variables ////////////////////////////////////////////

	// Packet constructors /////////////////////////////////////////
	public UpdateClientIdsPacket(Array<byte> clientIds) : base()
	{
		_clientIds = clientIds;
		_packetType = PacketType.UpdateClientIds;

	}
	
	public UpdateClientIdsPacket(byte[] data) : base(data){}
	public UpdateClientIdsPacket(StreamPeerBuffer buffer) : base(buffer){}
	// Packet constructors /////////////////////////////////////////

	// Encoding/Decoding ///////////////////////////////////////////
	public override void EncodeToBuffer(StreamPeerBuffer buffer)
	{
		base.EncodeToBuffer(buffer);
		
		buffer.PutU8((byte)_clientIds.Count);
		foreach (byte clientId in _clientIds)
			buffer.PutU8(clientId);
	}

	public override void DecodeFromBuffer(StreamPeerBuffer buffer)
	{
		base.DecodeFromBuffer(buffer);

		byte remoteIdCount = buffer.GetU8();
		for (byte i = 0; i < remoteIdCount; i++)
			_clientIds.Add(buffer.GetU8());
	}
	// Encoding/Decoding ///////////////////////////////////////////
}
