using ChatApi.FakeDb.Models;

namespace ChatApi.FakeDb
{
    public class ChatRepositoryFake
    {
        public static List<Message> Messages { get; } = new()
        {
            new Message { AuthorId = 1, AuthorName = "admin", Text = "I'm here" },
            new Message { AuthorId = 1, AuthorName = "admin", Text = "who's there?" },
            new Message { AuthorId = 2, AuthorName = "user", Text = "I'm not here" }
        };

        public List<Message> GetLast5Messages()
            => Messages.TakeLast(5).ToList();

        public void AddMessage(string userName, string text)
            => Messages.Add(new Message
            {
                AuthorName = userName,
                Text = text
            });

        public int Count()
            => Messages.Count();
    }
}
