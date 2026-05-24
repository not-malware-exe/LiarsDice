using Godot;
using Godot.Collections;
using System;
using System.Linq;

public class SubPacket
{
	// Packet variables ////////////////////////////////////////////
	
	// Packet variables ////////////////////////////////////////////

	// Packet constructors /////////////////////////////////////////
	public SubPacket(){}

	public SubPacket(byte[] data)
	{
		Decode(data);
	}
	
	public SubPacket(StreamPeerBuffer buffer)
	{
		DecodeFromBuffer(buffer);
	}
	// Packet constructors /////////////////////////////////////////
	
	// Encoding/Decoding ///////////////////////////////////////////
	// encodes data into byte array
	public byte[] Encode()
	{
		StreamPeerBuffer buffer = new StreamPeerBuffer();
		EncodeToBuffer(buffer);

		return buffer.DataArray;
	}

	// encodes data with StreamPeerBuffer
	public virtual void EncodeToBuffer(StreamPeerBuffer buffer)
	{
	}

	// decodes data into byte array
	public void Decode(byte[] packetData)
	{
		StreamPeerBuffer buffer = new StreamPeerBuffer();
		buffer.DataArray = packetData;

		DecodeFromBuffer(buffer);
	}

	// decodes data with StreamPeerBuffer
	public virtual void DecodeFromBuffer(StreamPeerBuffer buffer)
	{
	}
	// Encoding/Decoding ///////////////////////////////////////////
}
