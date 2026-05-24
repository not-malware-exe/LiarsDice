using Godot;

public partial class Card : GodotObject
{
    
    public enum CardType : byte
    {
        Default = 0
    }

    private CardType _cardType = CardType.Default;

    public Card(StreamPeerBuffer buffer)
    {
        DecodeFromBuffer(buffer);
    }
    
    // Encoding/Decoding ///////////////////////////////////////////
    public virtual void EncodeToBuffer(StreamPeerBuffer buffer)
    {
        buffer.PutU8((byte)_cardType);
    }

    public virtual void DecodeFromBuffer(StreamPeerBuffer buffer)
    {
        _cardType = (CardType)buffer.GetU8();
    }

    public static Card GetCardFromBuffer(StreamPeerBuffer buffer)
    {
        CardType cardType = (CardType)buffer.GetU8();
        buffer.Seek(buffer.GetPosition() - 1); // undoes GetU8()

        switch (cardType)
        {
            case CardType.Default:
                return new Card(buffer);
            default:
                return new Card(buffer); // Shouldn't happen
        }
    }
}