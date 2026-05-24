using Godot;
using Godot.Collections;
using System;

public class SendActionPacket : Packet
{
	// Packet variables ////////////////////////////////////////////

	public enum ActionType : byte
	{
    	Bid = 0,
		Challenge = 1,
	}

	private byte _clientId = 0;
	private ActionType _action = ActionType.Bid;
	private BidSubPacket _bid = null;

	public byte GetClientId(){return _clientId;}
	public ActionType GetAction(){return _action;}
	public (byte,byte) GetBid(){return _bid.GetBid();}

	// Packet variables ////////////////////////////////////////////

	// Packet constructors /////////////////////////////////////////
	public SendActionPacket(byte clientId = 0, ActionType action = ActionType.Bid, BidSubPacket bid = null) : base()
	{
		_clientId = clientId;
		_action = action;
		_bid = bid;
		_packetType = PacketType.SendAction;
	}
	
	public SendActionPacket(byte[] data) : base(data){}
	public SendActionPacket(StreamPeerBuffer buffer) : base(buffer){}
	// Packet constructors /////////////////////////////////////////

	// Encoding/Decoding ///////////////////////////////////////////
	public override void EncodeToBuffer(StreamPeerBuffer buffer)
	{
		base.EncodeToBuffer(buffer);
		buffer.PutU8((byte)_clientId);
		buffer.PutU8((byte)_action);

		if (_action == ActionType.Bid)
			_bid.EncodeToBuffer(buffer);
	}

	public override void DecodeFromBuffer(StreamPeerBuffer buffer)
	{
		base.DecodeFromBuffer(buffer);
		_clientId = buffer.GetU8();
		_action = (ActionType)buffer.GetU8();
		
		if (_action == ActionType.Bid)
			_bid = new BidSubPacket(buffer);
	}
	// Encoding/Decoding ///////////////////////////////////////////
}
