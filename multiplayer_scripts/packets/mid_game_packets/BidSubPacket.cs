using Godot;
using Godot.Collections;
using System;
using System.Linq;

public class BidSubPacket : SubPacket
{
	// Packet variables ////////////////////////////////////////////
	private byte _quantity = 1;
	private byte _value = 1;

	public (byte,byte)GetBid(){return (_quantity,_value);}
	// Packet variables ////////////////////////////////////////////

	// Packet constructors /////////////////////////////////////////
	public BidSubPacket(byte quantity, byte value) : base()
	{
		_quantity = quantity;
		_value = value;
	}
	
	public BidSubPacket(byte[] data) : base(data){}
	public BidSubPacket(StreamPeerBuffer buffer) : base(buffer){}

	// public BidSubPacket(StreamPeerBuffer buffer)
	// {
	// 	DecodeFromBuffer(buffer);
	// }
	// Packet constructors /////////////////////////////////////////

	// Encoding/Decoding ///////////////////////////////////////////
	public override void EncodeToBuffer(StreamPeerBuffer buffer)
	{
		base.EncodeToBuffer(buffer);
		buffer.PutU8(_quantity);
		buffer.PutU8(_value);
	}

	public override void DecodeFromBuffer(StreamPeerBuffer buffer)
	{
		base.DecodeFromBuffer(buffer);
		_quantity = buffer.GetU8();
		_value = buffer.GetU8();
	}
	// Encoding/Decoding ///////////////////////////////////////////
}
