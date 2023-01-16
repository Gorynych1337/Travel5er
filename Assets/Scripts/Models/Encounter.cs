public class Encounter
{
    private short _id;
    public short Id => _id;
        
    private string _name;
    public string Name => _name;
        
    private short _campaignId;
    public short CampaignId => _campaignId;
        
    private byte[] _mapImage;
    public byte[] MapImage => _mapImage;
        
    private short _height;
    public short Height => _height;
        
    private short _width;
    public short Width => _width;

    private Encounter()
    {
        
    }

    public Encounter(short id, string name, short campaignId, byte[] mapImage, short height, short width)
    {
        _id = id;
        _name = name;
        _campaignId = campaignId;
        _mapImage = mapImage;
        _height = height;
        _width = width;
    }
}