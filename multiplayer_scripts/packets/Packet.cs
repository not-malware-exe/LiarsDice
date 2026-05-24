using Godot;
using Godot.Collections;
using System;
using System.Linq;

public class Packet : SubPacket
{
	// Packet variables ////////////////////////////////////////////
	public enum PacketType : byte
	{
    	AssignId = 0,
		UpdateClientIds = 1,
		ReadyClient = 2,
		StartMatch = 3,
		StartRound = 4,
		UpdatePlayerInstance = 5,
		StartTurn = 6,
		SendAction = 7,

		ResolveChallenge = 10,



		PlayerPosTest = 255
	}

	protected PacketType _packetType;
	protected int _flags;
	// Packet variables ////////////////////////////////////////////

	// Packet constructors /////////////////////////////////////////
	public Packet()
	{

		_packetType = PacketType.AssignId;
		_flags = (int)ENetPacketPeer.FlagReliable;
	}
	
	public Packet(byte[] data) : base(data){}
	public Packet(StreamPeerBuffer buffer) : base(buffer){}
	// Packet constructors /////////////////////////////////////////
	
	// Encoding/Decoding ///////////////////////////////////////////
	// encodes data with StreamPeerBuffer
	public override void EncodeToBuffer(StreamPeerBuffer buffer)
	{
		buffer.PutU8((byte)_packetType);
	}

	// decodes data with StreamPeerBuffer
	public override void DecodeFromBuffer(StreamPeerBuffer buffer)
	{
		_packetType = (PacketType)buffer.GetU8();
	}
	// Encoding/Decoding ///////////////////////////////////////////

	// packet sending //////////////////////////////////////////////
	// sends data to specific peer
	public void Send(ENetPacketPeer target)
	{
		target.Send(0, Encode(), _flags);
	}
	
	// sends data to all peers
	public void BroadCast(ENetConnection server)
	{
		server.Broadcast(0, Encode(), _flags);
	}
	// packet sending //////////////////////////////////////////////
}
