using Godot;
using Godot.Collections;
using System;

public partial class TestPlayerSpawner : Node
{
	PackedScene _testPlayer = GD.Load<PackedScene>("res://test_scenes/TestPlayer.tscn");
	private Array<long> _remoteIds = [];

    public override void _Ready()
    {
		ClientNetworkGlobals clientNetwork = ClientNetworkGlobals.instance;
        clientNetwork.Connect(nameof(clientNetwork.HandleLocalIdAssignment),new Callable(this,nameof(SpawnPlayer)));
        clientNetwork.Connect(nameof(clientNetwork.HandleAddRemoteId),new Callable(this,nameof(SpawnPlayer)));
    }


	public void SpawnPlayer(long id)
	{
		TestPlayer player = _testPlayer.Instantiate<TestPlayer>();

		player.SetPlayerID(id);
		player.Name += id;

		CallDeferred(Node.MethodName.AddChild,player);
	}
}
