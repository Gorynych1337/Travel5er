using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campaign
{
    public short Id { get; private set; }
    public string Name { get; private set; }
    public short OwnerId { get; private set; }
    
    private Campaign(){}

    public Campaign(short id, string name, short ownerId)
    {
        Id = id;
        Name = name;
        OwnerId = ownerId;
    }
}
