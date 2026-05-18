using Godot;
using System;

public partial class TestPlayerSpawner : Node
{
	PackedScene _testPlayer = GD.Load<PackedScene>("res://test_scenes/TestPlayer.tscn");

    public override void _Ready()
    {
		NetworkHandler networkHandler = NetworkHandler.instance;
		networkHandler.Connect(nameof(networkHandler.OnPeerConnected),new Callable(this,nameof(SpawnPlayer)));
		ClientNetworkGlobals clientNetwork = ClientNetworkGlobals.instance;
        clientNetwork.Connect(nameof(clientNetwork.HandleLocalIdAssignment),new Callable(this,nameof(SpawnPlayer)));
        // clientNetwork.Connect(nameof(clientNetwork.HandleRemoteIdAssignment),new Callable(this,nameof(SpawnPlayer)));
    }


	public void SpawnPlayer(long id)
	{
		TestPlayer player = _testPlayer.Instantiate<TestPlayer>();

		player.SetOwnerID(id);
		player.Name += id;

		CallDeferred(Node.MethodName.AddChild,player);
	}
}
