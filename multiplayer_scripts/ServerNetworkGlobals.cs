using Godot;
using Godot.Collections;
using System;

public partial class ServerNetworkGlobals : Node
{
	public static ServerNetworkGlobals instance = null;

	
	[Signal]
	public delegate void HandlePlayerPosTestEventHandler(long id, Vector2 pos);

	private Array<long> _peerIds = [];
	public void SetPeerId(Array<long> peerIds){_peerIds = peerIds;}
	public Array<long> GetPeerId(){return _peerIds;}


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		instance = this;
		NetworkHandler networkHandler = NetworkHandler.instance;

		networkHandler.Connect(nameof(networkHandler.OnPeerConnected),new Callable(this, nameof(OnPeerConnected)));
		networkHandler.Connect(nameof(networkHandler.OnPeerDisconnected),new Callable(this, nameof(OnPeerDisconnected)));
		networkHandler.Connect(nameof(networkHandler.OnServerPacket),new Callable(this, nameof(OnServerPacket)));
	}

	public void OnPeerConnected(long peerId)
	{
		_peerIds.Add(peerId);

		Array<byte> peerIds = [];
		for (byte i = 0; i < _peerIds.Count; i++)
			peerIds.Add((byte)_peerIds[i]);

		
		NetworkHandler networkHandler = NetworkHandler.instance;
		new IdAssignmentPacketInfo((byte)peerId,peerIds).BroadCast(networkHandler.GetENetConnection());
	}

	public void OnPeerDisconnected(long peerId)
	{
		_peerIds.Remove(peerId);

		GD.PushError("Packet for unassigning peers does not exist yet, so yeah, this error is a reminder to do that.");
	}

	public void OnServerPacket(long peerId, byte[] data)
	{
		byte packetType = data[0];

		switch ((PacketInfo.PacketType)packetType)
		{
			case PacketInfo.PacketType.PlayerPosTest:
				PlayerPosTestPacketInfo playerPosTestPacketInfo = new PlayerPosTestPacketInfo(data);
				if (peerId != playerPosTestPacketInfo.GetId())GD.Print(peerId," ",playerPosTestPacketInfo.GetId());
				EmitSignal(nameof(HandlePlayerPosTest),playerPosTestPacketInfo.GetId(),playerPosTestPacketInfo.GetPos());
				break;
			default:
				GD.PushError("This packet type for value ", packetType," is not accounted for in server globals.");
				break;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
