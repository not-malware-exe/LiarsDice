using Godot;
using Godot.Collections;

public partial class NetworkHandler : Node
{
	public static NetworkHandler instance = null;

    public override void _Ready()
    {
		instance = this;
    }

	// Server signals
	[Signal]
	public delegate void OnPeerConnectedEventHandler(long peerId);
	[Signal]
	public delegate void OnPeerDisconnectedEventHandler(long peerId);
	[Signal]
	public delegate void OnServerPacketEventHandler(long peerId, byte[] packetData);

	// Client signals
	[Signal]
	public delegate void OnConnectedToServerEventHandler();
	[Signal]
	public delegate void OnDisconnectedToServerEventHandler();
	[Signal]
	public delegate void OnClientPacketEventHandler(byte[] packetData);

	// server variables
		static Array<long> initAvailablePeerIds(){Array<long>arr=[];for(long i=255;i>-1;i--)arr.Add(i);return arr;}
	Array<long> availablePeerIds = initAvailablePeerIds();
	Dictionary<long, ENetPacketPeer> clientPeers = [];

	// client variables
	private ENetPacketPeer _serverPeer = null;
	public ENetPacketPeer GetServerpeer(){return _serverPeer;}

	// general variables
	private ENetConnection connection = null;
	public ENetConnection GetENetConnection(){return connection;}

	private bool _isServer = false;
	public bool IsServer(){return _isServer;}

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
					if (_isServer)
						PeerConnected(peer);
					else
						ConnectedToServer();
					break;
				case ENetConnection.EventType.Disconnect:
					if (_isServer)
						PeerDisonnected(peer);
					else
					{
						DisconnectedFromServer();
						return;
					}
					break;
				case ENetConnection.EventType.Receive:
					if (_isServer)
						EmitSignal(nameof(OnServerPacket), peer.GetMeta("id").As<long>(), peer.GetPacket());
					else
						EmitSignal(nameof(OnClientPacket), peer.GetPacket());
					break;
			}

			// get next packet
			packetEvent = connection.Service();
			eventType = packetEvent[0].As<ENetConnection.EventType>();
		}
	}


	public void StartServer(string ipAddress = "127.0.0.1", int port = 42069)
	{
		connection = new ENetConnection();
		Error error = connection.CreateHostBound(ipAddress, port);

		if (error != 0)
		{
			GD.PushWarning("Server starting failed: ", error.ToString());
			connection = null;
			return;
		}

		GD.Print("Server started");
		_isServer = true;

	}

	void PeerConnected(ENetPacketPeer peer)
	{
		long peerId = availablePeerIds[availablePeerIds.Count - 1];
		availablePeerIds.RemoveAt(availablePeerIds.Count - 1);

		peer.SetMeta("id", peerId);
		clientPeers[peerId] = peer; // if key does not exist, it will write a new key value pair

		GD.Print("Peer connected with assigned id: ", peerId);
		EmitSignal(nameof(OnPeerConnected), peerId);
	}
	void PeerDisonnected(ENetPacketPeer peer)
	{
		long peerId = peer.GetMeta("id").As<long>();
		availablePeerIds.Add(peerId);
		clientPeers.Remove(peerId);

		GD.Print("Successfully disconnected ", peerId, " from server.");
		EmitSignal(nameof(OnPeerDisconnected), peerId);
	}


	public void startClient(string ipAddress = "127.0.0.1", int port = 42069)
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

	}

	void disconnectClient()
	{
		if (_isServer)
			return;

		_serverPeer.PeerDisconnect();
	}

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
}
