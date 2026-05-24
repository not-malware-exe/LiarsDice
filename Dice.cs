using System;
using System.ComponentModel.DataAnnotations;
using Godot;

public partial class Die : GodotObject
{

    public enum DieType : byte
    {
        Default = 0
    }

    private DieType _dieType = DieType.Default;

    private int _value = 1;
    private int _max = 6;
    public int GetValue(){return _value;}
    public void Roll(){_value = GD.RandRange(1,_max);}
    public Die(int max = 6)
    {
        _max = max;
        Roll();
    }

    public Die(int value, int max = 6)
    {
        _value = Math.Min(value,max);
        _max = max;
    }

    public Die(StreamPeerBuffer buffer)
    {
        DecodeFromBuffer(buffer);
    }

    // Encoding/Decoding ///////////////////////////////////////////

    public virtual void EncodeToBuffer(StreamPeerBuffer buffer)
    {
        buffer.PutU8((byte)_dieType);
        buffer.PutU8((byte)_value);
        buffer.PutU8((byte)_max);
    }

    public virtual void DecodeFromBuffer(StreamPeerBuffer buffer)
    {
        _dieType = (DieType)buffer.GetU8();
        _value = (int)buffer.GetU8();
        _max = (int)buffer.GetU8();
    }

    public static Die GetDieFromBuffer(StreamPeerBuffer buffer)
    {
        DieType dieType = (DieType)buffer.GetU8();
        buffer.Seek(buffer.GetPosition() - 1); // undoes GetU8()

        switch (dieType)
        {
            case DieType.Default:
                return new Die(buffer);
            default:
                return new Die(buffer); // Shouldn't happen
        }
        
    }
}