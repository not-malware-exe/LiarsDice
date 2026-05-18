using Godot;
using Godot.Collections;

public partial class ServerNetworkHandler : Node
{
	public static ServerNetworkHandler instance = null;

    public override void _Ready()
    {
		instance = this;
    }

	// Server signals /////////////////////////////////////////////////////
	[Signal]
	public delegate void OnPeerConnectedEventHandler(long peerId);
	[Signal]
	public delegate void OnPeerDisconnectedEventHandler(long peerId);
	[Signal]
	public delegate void OnServerPacketEventHandler(long peerId, byte[] packetData);
	// Server signals /////////////////////////////////////////////////////

	// server variables ///////////////////////////////////////////////////
		static Array<long> initAvailablePeerIds(){Array<long>arr=[];for(long i=255;i>-1;i--)arr.Add(i);return arr;}
	Array<long> availablePeerIds = initAvailablePeerIds();
	Dictionary<long, ENetPacketPeer> clientPeers = [];
	// server variables ///////////////////////////////////////////////////

    // network variables //////////////////////////////////////////////////
	private ENetConnection connection = null;
	public ENetConnection GetENetConnection(){return connection;}

	private bool _isActive = false;
	public bool IsActive(){return _isActive;}
    // network variables //////////////////////////////////////////////////

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
					PeerConnected(peer);
					break;
				case ENetConnection.EventType.Disconnect:
					PeerDisonnected(peer);
					break;
				case ENetConnection.EventType.Receive:
					EmitSignal(nameof(OnServerPacket), peer.GetMeta("id").As<long>(), peer.GetPacket());
					break;
			}

			// get next packet
			packetEvent = connection.Service();
			eventType = packetEvent[0].As<ENetConnection.EventType>();
		}
	}
    // Handles incoming packets every frame ////////////////////////////////

    // Creates server on address and port //////////////////////////////////
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
		_isActive = true;

	}
    // Creates server on address and port //////////////////////////////////

    // Handles client peer connection //////////////////////////////////////
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
    // Handles client peer connection //////////////////////////////////////
}
