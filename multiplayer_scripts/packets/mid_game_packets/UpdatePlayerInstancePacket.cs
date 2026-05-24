using Godot;
using Godot.Collections;
using System;
using System.Linq;

public class UpdatePlayerInstancePacket : Packet
{
	// Packet variables ////////////////////////////////////////////
	private byte _clientId = 0;
	private PlayerInstance _playerinstance = null;
	
	public byte GetClientId(){return _clientId;}
	public PlayerInstance GetPlayerInstance(){return _playerinstance;}
	// Packet variables ////////////////////////////////////////////

	// Packet constructors /////////////////////////////////////////
	public UpdatePlayerInstancePacket(byte clientId, PlayerInstance playerInstance) : base()
	{
		_clientId = clientId;
		_playerinstance = playerInstance;
		_packetType = PacketType.UpdatePlayerInstance;

	}
	
	public UpdatePlayerInstancePacket(byte[] data) : base(data){}
	public UpdatePlayerInstancePacket(StreamPeerBuffer buffer) : base(buffer){}
	// Packet constructors /////////////////////////////////////////

	// Encoding/Decoding ///////////////////////////////////////////
	public override void EncodeToBuffer(StreamPeerBuffer buffer)
	{
		base.EncodeToBuffer(buffer);
		
		buffer.PutU8(_clientId);
		_playerinstance.EncodeToBuffer(buffer);
	}

	public override void DecodeFromBuffer(StreamPeerBuffer buffer)
	{
		base.DecodeFromBuffer(buffer);

		_clientId = buffer.GetU8();
		_playerinstance = new PlayerInstance(buffer);
	}
	// Encoding/Decoding ///////////////////////////////////////////
}
