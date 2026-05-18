using Godot;
using System;

public partial class NetworkTestUi : Control
{
	[Export]
	public Button serverButton = null;
	[Export]
	public Button clientButton = null;

    public override void _Ready()
    {
        base._Ready();
		serverButton.Pressed += ServerButtonPressed;
		clientButton.Pressed += ClientButtonPressed;
    }

	void ServerButtonPressed()
	{
		ServerNetworkHandler.instance.StartServer();
		ClientNetworkHandler.instance.startClient();
		QueueFree();
	}
	
	void ClientButtonPressed()
	{
		ClientNetworkHandler.instance.startClient();
		QueueFree();
	}
}
