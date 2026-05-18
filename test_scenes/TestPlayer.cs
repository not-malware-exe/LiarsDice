using Godot;
using System;

public partial class TestPlayer : CharacterBody2D
{
	const float SPEED = 500.0f;

	private long _ownerId = -1;
	public void SetOwnerID(long ownerId){_ownerId = ownerId;}

	public bool IsAuthority()
	{
		return !NetworkHandler.instance.IsServer() && _ownerId == ClientNetworkGlobals.instance.GetLocalId();
	}

    public override void _EnterTree()
    {
		ServerNetworkGlobals serverNetwork = ServerNetworkGlobals.instance;
        serverNetwork.Connect(nameof(serverNetwork.HandlePlayerPosTest),new Callable(this,nameof(ServerHandlePlayerPosTest)));
		ClientNetworkGlobals clientNetwork = ClientNetworkGlobals.instance;
        clientNetwork.Connect(nameof(clientNetwork.HandlePlayerPosTest),new Callable(this,nameof(ClientHandlePlayerPosTest)));
    }
	
    public override void _ExitTree()
    {
		ServerNetworkGlobals serverNetwork = ServerNetworkGlobals.instance;
        serverNetwork.Disconnect(nameof(serverNetwork.HandlePlayerPosTest),new Callable(this,nameof(ServerHandlePlayerPosTest)));
		ClientNetworkGlobals clientNetwork = ClientNetworkGlobals.instance;
        clientNetwork.Disconnect(nameof(clientNetwork.HandlePlayerPosTest),new Callable(this,nameof(ClientHandlePlayerPosTest)));
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

			PlayerPosTestPacketInfo playerPosTestPacketInfo = new PlayerPosTestPacketInfo((byte)_ownerId,GlobalPosition);
			playerPosTestPacketInfo.Send(NetworkHandler.instance.GetServerpeer());
		}
	}

	public void ServerHandlePlayerPosTest(long id, Vector2 pos)
	{
		if (_ownerId == id)
		{
			GlobalPosition = pos;
			new PlayerPosTestPacketInfo((byte)id,GlobalPosition).BroadCast(NetworkHandler.instance.GetENetConnection());
		}
	}
	public void ClientHandlePlayerPosTest(long id, Vector2 pos)
	{
		if (!IsAuthority() && _ownerId == id)
		{
			GlobalPosition = pos;
		}
	}
}
