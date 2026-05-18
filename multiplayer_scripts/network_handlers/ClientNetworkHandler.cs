using Godot;
using Godot.Collections;

public partial class ClientNetworkHandler : Node
{
	public static ClientNetworkHandler instance = null;

    public override void _Ready()
    {
		instance = this;
    }

	// Client signals /////////////////////////////////////////////////////
	[Signal]
	public delegate void OnConnectedToServerEventHandler();
	[Signal]
	public delegate void OnDisconnectedToServerEventHandler();
	[Signal]
	public delegate void OnClientPacketEventHandler(byte[] packetData);
    // Client signals /////////////////////////////////////////////////////

	// client variables ///////////////////////////////////////////////////
	private ENetPacketPeer _serverPeer = null;
	public ENetPacketPeer GetServerpeer(){return _serverPeer;}
	// client variables ///////////////////////////////////////////////////

    // network variables //////////////////////////////////////////////////
	private ENetConnection connection = null;
	public ENetConnection GetENetConnection(){return connection;}
    
	private bool _isActive = false;
	public bool IsActive(){return _isActive;}
    // network variables ///////////////////////////////////////////////////
    
    // Handles incoming packets every frame ////////////////////////////////
    public override void _Process(double delta)
    {
        base._Process(delta);

		if (connection != null)
		{
			HandleEvents();
		}
    }

	void HandleEvents()
	{
		// gets packet
		Array packetEvent = connection.Service();
		ENetConnection.EventType eventType = packetEvent[0].As<ENetConnection.EventType>();

		while (eventType != ENetConnection.EventType.None) // loops until all packets retrieved
		{
			ENetPacketPeer peer = packetEvent[1].As<ENetPacketPeer>();

			switch (eventType)
			{
				case ENetConnection.EventType.Error:
					GD.PushWarning("Package resulted in an error.");
					break;
				case ENetConnection.EventType.Connect:
					ConnectedToServer();
					break;
				case ENetConnection.EventType.Disconnect:
					DisconnectedFromServer();
					return;
				case ENetConnection.EventType.Receive:
					EmitSignal(nameof(OnClientPacket), peer.GetPacket());
					break;
			}

			// get next packet
			packetEvent = connection.Service();
			eventType = packetEvent[0].As<ENetConnection.EventType>();
		}
	}
    // Handles incoming packets every frame ////////////////////////////////

    // Connects/Disconnects client to/from server peer /////////////////////
	public void startClient(string ipAddress = "127.0.0.1", int port = 42069) // The port can pretty much be it's own number
	{
		connection = new ENetConnection();
		Error error = connection.CreateHost(1);

		if (error != 0)
		{
			GD.PushWarning("Server starting failed: ", error.ToString());
			connection = null;
			return;
		}

		GD.Print("Client started");
		_serverPeer = connection.ConnectToHost(ipAddress, port);
		_isActive = true;
	}

	void disconnectClient()
	{
		_serverPeer.PeerDisconnect();
	}
    // Connects/Disconnects client to/from server peer /////////////////////

    // Emits signals upon connection change ////////////////////////////////
	void ConnectedToServer()
	{
		GD.Print("Successfully connected to server.");
		EmitSignal(nameof(OnConnectedToServer));
	}
	void DisconnectedFromServer()
	{
		GD.Print("Successfully disconnected from server.");
		EmitSignal(nameof(OnDisconnectedToServer));
		connection = null;
	}
    // Emits signals upon connection change ////////////////////////////////
}
