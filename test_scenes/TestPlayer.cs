using Godot;
using System;

public partial class TestPlayer : CharacterBody2D
{
	const float SPEED = 500.0f;

	private long _playerId = -1; // what player does this node belong to
	public void SetPlayerID(long playerId){_playerId = playerId;}

	public bool IsAuthority() // does this player own this node
	{
		return _playerId == ClientNetworkGlobals.instance.GetLocalId();
	}

    public override void _EnterTree()
    {
		ClientNetworkGlobals clientNetwork = ClientNetworkGlobals.instance;
        clientNetwork.Connect(nameof(clientNetwork.HandlePlayerPosTest),new Callable(this,nameof(ClientHandlePlayerPosTest)));
		clientNetwork.Connect(nameof(clientNetwork.HandleRemoveRemoteId),new Callable(this,nameof(ClientDisconnected)));
    }
	
    public override void _ExitTree()
    {
		ClientNetworkGlobals clientNetwork = ClientNetworkGlobals.instance;
        clientNetwork.Disconnect(nameof(clientNetwork.HandlePlayerPosTest),new Callable(this,nameof(ClientHandlePlayerPosTest)));
		clientNetwork.Disconnect(nameof(clientNetwork.HandleRemoveRemoteId),new Callable(this,nameof(ClientDisconnected)));
    }


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (IsAuthority())
		{
			Velocity = Input.GetVector("A","D","W","S") * SPEED;
			MoveAndSlide();

			PlayerPosTestPacket playerPosTestPacket = new PlayerPosTestPacket((byte)_playerId,GlobalPosition);
			playerPosTestPacket.Send(ClientNetworkHandler.instance.GetServerpeer());
		}
	}

	public void ClientDisconnected(long id)
	{
		if (!IsAuthority() && _playerId == id)
		{
			QueueFree();
		}
	}

	public void ClientHandlePlayerPosTest(long id, Vector2 pos)
	{
		if (!IsAuthority() && _playerId == id)
		{
			GlobalPosition = pos;
		}
	}
}
