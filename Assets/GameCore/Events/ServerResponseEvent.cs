using Assets.Scripts.Utility;

public class ServerResponseEvent : BaseEventParams
{
    public string ResponseJson { get; }

    public ServerResponseEvent(string responseJson)
    {
        ResponseJson = responseJson;
    }
}
