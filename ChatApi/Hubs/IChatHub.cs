namespace ChatApi.Hubs
{
    public interface IChatHub
    {
        Task NotifyAboutNewMessage(string userName, string messageText);
        Task NotifyAboutNewUser(string userName);
    }
}
