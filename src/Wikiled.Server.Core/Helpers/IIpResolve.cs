namespace Wikiled.Server.Core.Helpers
{
    public interface IIpResolve
    {
        string GetRequestIp(bool tryUseXForwardHeader = true);
    }
}