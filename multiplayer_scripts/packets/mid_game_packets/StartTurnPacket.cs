using Godot;
using Godot.Collections;
using System;
using System.Linq;

public class StartTurn : Packet
{
	// Packet variables ////////////////////////////////////////////
    private byte _idTurn = 0;
    
    public byte GetIdTurn(){return _idTurn;}
	// Packet variables ////////////////////////////////////////////

	// Packet constructors /////////////////////////////////////////
	public StartTurn(byte idTurn) : base()
	{
        _idTurn = idTurn;
		_packetType = PacketType.StartTurn;
		_flags = (int)ENetPacketPeer.FlagReliable;
	}
	
	public StartTurn(byte[] data) : base(data){}
	public StartTurn(StreamPeerBuffer buffer) : base(buffer){}
	// Packet constructors /////////////////////////////////////////

	// Encoding/Decoding ///////////////////////////////////////////
	public override void EncodeToBuffer(StreamPeerBuffer buffer)
	{
		base.EncodeToBuffer(buffer);
        buffer.PutU8(_idTurn);

	}

	public override void DecodeFromBuffer(StreamPeerBuffer buffer)
	{
		base.DecodeFromBuffer(buffer);
		_idTurn = buffer.GetU8();
		
	}
	// Encoding/Decoding ///////////////////////////////////////////
}
