using Godot;
using Godot.Collections;
using System;

public partial class ServerNetworkGlobals : Node
{
	public static ServerNetworkGlobals instance = null;

	

	private Array<long> _peerIds = [];
	public void SetPeerIds(Array<long> peerIds){_peerIds = peerIds;}
	public Array<long> GetPeerIds(){return _peerIds;}


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		instance = this;
		ServerNetworkHandler serverNetworkHandler = ServerNetworkHandler.instance;

		serverNetworkHandler.Connect(nameof(serverNetworkHandler.OnPeerConnected),new Callable(this, nameof(OnPeerConnected)));
		serverNetworkHandler.Connect(nameof(serverNetworkHandler.OnPeerDisconnected),new Callable(this, nameof(OnPeerDisconnected)));
		serverNetworkHandler.Connect(nameof(serverNetworkHandler.OnServerPacket),new Callable(this, nameof(OnServerPacket)));

		GD.PushWarning("Rename class to better explain that this handles the packets sent to the server, something like ServerPacketHandler"); // is also specific to game
	}

	public void OnPeerConnected(long peerId)
	{
		_peerIds.Add(peerId);

		Array<byte> peerIds = [];
		for (byte i = 0; i < _peerIds.Count; i++)
			peerIds.Add((byte)_peerIds[i]);

		
		ServerNetworkHandler serverNetworkHandler = ServerNetworkHandler.instance;
		new IdAssignmentPacketInfo((byte)peerId,peerIds).BroadCast(serverNetworkHandler.GetENetConnection());
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
				playerPosTestPacketInfo.BroadCast(ServerNetworkHandler.instance.GetENetConnection()); // Sends data to all clients
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
