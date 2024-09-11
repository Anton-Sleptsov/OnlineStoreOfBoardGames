using ChatApi.FakeDb;
using Microsoft.AspNetCore.SignalR;

namespace ChatApi.Hubs
{
    public class ChatHub : Hub<IChatHub>
    {
        private ChatRepositoryFake _chatRepositoryFake;

        public ChatHub(ChatRepositoryFake chatRepositoryFake)
        {
            _chatRepositoryFake = chatRepositoryFake;
        }

        public void UserConnectToChat(string userName)
        {
            Clients.All.NotifyAboutNewUser(userName);
        }

        public void AddNewMessage(string userName, string messageText)
        {
            _chatRepositoryFake.AddMessage(userName, messageText);
            Clients.All.NotifyAboutNewMessage(userName, messageText);
        }
    }
}
