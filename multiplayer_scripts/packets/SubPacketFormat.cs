using Godot;
using Godot.Collections;
using System;
using System.Linq;

public class SubPacketFormat : SubPacket
{
	// Packet variables ////////////////////////////////////////////
	
	// Packet variables ////////////////////////////////////////////

	// Packet constructors /////////////////////////////////////////
	public SubPacketFormat() : base(){}

	public SubPacketFormat(byte[] data) : base(data){}
	public SubPacketFormat(StreamPeerBuffer buffer) : base(buffer){}
	// Packet constructors /////////////////////////////////////////
	
	// Encoding/Decoding ///////////////////////////////////////////
	// encodes data with StreamPeerBuffer
	public override void EncodeToBuffer(StreamPeerBuffer buffer)
	{
		base.EncodeToBuffer(buffer);


	}

	// decodes data with StreamPeerBuffer
	public override void DecodeFromBuffer(StreamPeerBuffer buffer)
	{
		base.DecodeFromBuffer(buffer);

		
	}
	// Encoding/Decoding ///////////////////////////////////////////
}
