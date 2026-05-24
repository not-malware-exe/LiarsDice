using Godot;
using Godot.Collections;
using System;
using System.Linq;

public partial class ClientNetworkGlobals : Node
{
	public static ClientNetworkGlobals instance = null;

	[Signal] 
	public delegate void HandleLocalIdAssignmentEventHandler(long localId);
	[Signal] 
	public delegate void HandleRemoteIdUpdateEventHandler(Array<long> remoteIds);
	[Signal]
	public delegate void HandleAddRemoteIdEventHandler(long remoteId);
	[Signal]
	public delegate void HandleRemoveRemoteIdEventHandler(long remoteId);
	
	[Signal]
	public delegate void HandlePlayerPosTestEventHandler(long id, Vector2 pos);

	private long _localId = -1;
	public void SetLocalId(long localId){_localId = localId;}
	public long GetLocalId(){return _localId;}

	private Array<long> _remoteIds = [];
	public void SetRemoteId(Array<long> remoteIds){_remoteIds = remoteIds;}
	public Array<long> GetRemoteId(){return _remoteIds;}

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		instance = this;
		ClientNetworkHandler clientNetworkHandler = ClientNetworkHandler.instance;
		clientNetworkHandler.Connect(nameof(clientNetworkHandler.OnClientPacket),new Callable(this,nameof(OnClientPacket)));

		GD.PushWarning("Rename class to better explain that this handles the packets sent to the client, something like ClientPacketHandler"); // is also specific to game
	}

	void OnClientPacket(byte[] data)
	{
		byte packetType = data[0];

		switch ((Packet.PacketType)packetType)
		{
			case Packet.PacketType.AssignId: /////////////////////////////////////////////////////
				AssignIdPacket assignIdPacket = new AssignIdPacket(data);
				_localId = assignIdPacket.GetLocalId();
				EmitSignal(nameof(HandleLocalIdAssignment),_localId);
				break;
			case Packet.PacketType.UpdateClientIds: /////////////////////////////////////////////////////
				UpdateClientIdsPacket updateClientIdsPacket = new UpdateClientIdsPacket(data);
				UpdateRemoteIds(updateClientIdsPacket);
				break;
			case Packet.PacketType.PlayerPosTest: /////////////////////////////////////////////////////
				PlayerPosTestPacket playerPosTestPacket = new PlayerPosTestPacket(data);
				EmitSignal(nameof(HandlePlayerPosTest),playerPosTestPacket.GetId(),playerPosTestPacket.GetPos());
				break;
			default:
				GD.PushError("This packet type for value ", packetType," is not accounted for in client globals.");
				break;
		}
	}

	private void UpdateRemoteIds(UpdateClientIdsPacket packet)
	{
		Array<long> remoteIds = [];

		foreach (byte clientId in packet.GetClientIds())
			if ((long)clientId != _localId)
			{
				if (!_remoteIds.Contains(clientId))
					EmitSignal(nameof(HandleAddRemoteId),clientId);
				remoteIds.Add(clientId);
			}

		foreach (long clientId in _remoteIds)
			if (!remoteIds.Contains(clientId))
				EmitSignal(nameof(HandleRemoveRemoteId),clientId);

		_remoteIds = remoteIds;
		EmitSignal(nameof(HandleRemoteIdUpdate),_remoteIds);
	}
}


