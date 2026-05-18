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
	public delegate void HandleRemoteIdAssignmentEventHandler(long remoteId);
	
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
		NetworkHandler networkHandler = NetworkHandler.instance;
		networkHandler.Connect(nameof(networkHandler.OnClientPacket),new Callable(this,nameof(OnClientPacket)));
	}

	void OnClientPacket(byte[] data)
	{
		byte packetType = data[0];

		switch ((PacketInfo.PacketType)packetType)
		{
			case PacketInfo.PacketType.IdAssignment:
				ManageIds(new IdAssignmentPacketInfo(data));
				break;
			case PacketInfo.PacketType.PlayerPosTest:
				PlayerPosTestPacketInfo playerPosTestPacketInfo = new PlayerPosTestPacketInfo(data);
				EmitSignal(nameof(HandlePlayerPosTest),playerPosTestPacketInfo.GetId(),playerPosTestPacketInfo.GetPos());
				break;
			default:
				GD.PushError("This packet type for value ", packetType," is not accounted for in client globals.");
				break;
		}
	}

	void ManageIds(IdAssignmentPacketInfo idPacketInfo)
	{
		if (_localId == -1)
		{
			_localId = idPacketInfo.GetLocalId();
			EmitSignal(nameof(HandleLocalIdAssignment),_localId);

			_remoteIds = [];
			foreach (byte remoteId in idPacketInfo.GetRemoteIds())
			{
				_remoteIds.Add(remoteId);
				if (remoteId != _localId)
					EmitSignal(nameof(HandleRemoteIdAssignment),remoteId);
			}
		}
		else
		{
			_remoteIds.Add(idPacketInfo.GetLocalId());
			EmitSignal(nameof(HandleRemoteIdAssignment),idPacketInfo.GetLocalId());
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
