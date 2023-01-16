using System;

public class SessionInfo
{
    private static Lazy<SessionInfo> _instance = new Lazy<SessionInfo>(() => new SessionInfo());
    public static SessionInfo Instance => _instance.Value;

    private short? _userId;
    public short? UserId => _userId;

    private bool _isUserAdmin;
    public bool IsUserAdmin => _isUserAdmin;

    private CampaignInfo? _campInfo;
    public CampaignInfo? CampInfo => _campInfo;

    public void SetUser(short id, bool isUserAdmin)
    {
        _userId = id;
        _isUserAdmin = isUserAdmin;
    }

    public void SetCampaignInfo(short id, string name)
    {
        _campInfo = new CampaignInfo(id, name);
    }

    public void RemoveCampaignInfo()
    {
        _campInfo = null;
    }
    
    public struct CampaignInfo
    {
        private short _id;
        public short Id => _id;
        private string _name;
        public string Name => _name;

        public CampaignInfo(short id, string name)
        {
            _id = id;
            _name = name;
        }
    };
}
