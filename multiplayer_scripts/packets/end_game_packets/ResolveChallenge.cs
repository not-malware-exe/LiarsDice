using System.Linq;
using Godot;
using Godot.Collections;

public class ResolveChallengePacket : Packet
{
	// Packet variables ////////////////////////////////////////////

	private Array<byte> _dice = [];
	private byte _winnerId = 0;
	private byte _loserId = 0;
	private bool _loserEliminated = false;

	public Array<byte> GetDice(){return _dice;}
	public byte GetWinnerId(){return _winnerId;}
	public byte GetLoserId(){return _loserId;}
	public bool IsLoserEliminated(){return _loserEliminated;}

	// Packet variables ////////////////////////////////////////////

	// Packet constructors /////////////////////////////////////////
	public ResolveChallengePacket(Array<byte> dice, byte winnerId, byte loserId, bool loserEliminated) : base()
	{
		_dice = dice;
		_winnerId = winnerId;
		_loserId = loserId;
		_loserEliminated = loserEliminated;
		_packetType = PacketType.ResolveChallenge;
	}
	
	public ResolveChallengePacket(byte[] data) : base(data){}
	public ResolveChallengePacket(StreamPeerBuffer buffer) : base(buffer){}
	// Packet constructors /////////////////////////////////////////

	// Encoding/Decoding ///////////////////////////////////////////
	public override void EncodeToBuffer(StreamPeerBuffer buffer)
	{
		base.EncodeToBuffer(buffer);

		byte diceCount = (byte)_dice.Count;
		buffer.PutU8(diceCount);
		buffer.PutData(_dice.ToArray());

		buffer.PutU8(_winnerId);
		buffer.PutU8(_loserId);
		buffer.PutU8((byte)(_loserEliminated ? 1 : 0));
	}

	public override void DecodeFromBuffer(StreamPeerBuffer buffer)
	{
		base.DecodeFromBuffer(buffer);

		byte diceCount = buffer.GetU8();
		for (int i = 0; i < diceCount; i++)
			_dice.Add(buffer.GetU8());
		

		_winnerId = buffer.GetU8();
		_loserId = buffer.GetU8();
		_loserEliminated = buffer.GetU8() == 1;
	}
	// Encoding/Decoding ///////////////////////////////////////////
}
