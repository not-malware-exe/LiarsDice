using Godot;
using System;
using Godot.Collections;

public partial class PlayerInstance : GodotObject
{
	private long _id = 0;
	private Array<Die> _dice = [];
	private Card _card = null;
	// private Array<StatusEffect> _statusEffects = [];

	public long GetId(){return _id;}
	public void SetId(long id){_id = id;}

	public Array<Die> GetDice(){return _dice;}
	public void SetDice(Array<Die> dice){}

	public Card GetCard(){return _card;}
	public void SetCard(Card card){_card = card;}

	public PlayerInstance(long id, Array<Die> dice, Card card)
	{
		_id = id;
		_dice = dice;
		_card = card;
	}

	public PlayerInstance(StreamPeerBuffer buffer)
	{
		DecodeFromBuffer(buffer);
	}
	
	// Encoding/Decoding ///////////////////////////////////////////
    public StreamPeerBuffer EncodeToBuffer(StreamPeerBuffer buffer)
    {
		buffer.PutU8((byte)_id);

		buffer.PutU8((byte)_dice.Count);
		foreach (Die die in _dice)
		 	die.EncodeToBuffer(buffer);
		
		_card.EncodeToBuffer(buffer);

		return buffer;
    }

	public void DecodeFromBuffer(StreamPeerBuffer buffer)
	{
		_id = (long)buffer.GetU8();

		int diceCount = (int)buffer.GetU8();
		Array<Die> dice = [];
		for (int i = 0; i < diceCount; i++)
			dice.Add(Die.GetDieFromBuffer(buffer));
		_dice = dice;
		
		_card = Card.GetCardFromBuffer(buffer);
	}

}
