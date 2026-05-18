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
		NetworkHandler.instance.StartServer();
	}
	
	void ClientButtonPressed()
	{
		NetworkHandler.instance.startClient();
	}
}
