using System;
using UnityEngine;

public class EncounterData
{
    private static Lazy<EncounterData> _instance = new Lazy<EncounterData>(() => new EncounterData());
    public static EncounterData Instance => _instance.Value;
    
    private short _id;
    public short Id => _id;
    
    private byte[] _image;
    public byte[] Image => _image;

    private Vector2 _size;
    public Vector2 Size => _size;

    public void SetData(short id, byte[] image, int width, int height)
    {
        _id = id;
        _image = image;
        _size = new Vector2(width, height);
    }
}