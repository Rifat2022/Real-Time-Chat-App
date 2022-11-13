namespace LetsChatApplication.Hub
{
    public interface IHubClient
    {
        Task BroadcastMessage();
    }
}
