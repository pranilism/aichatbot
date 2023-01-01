namespace AIBasedChat.Models;

public class ChatInput
{
    public string Name { get; set; } = "My Friend";
    public string Message { get; set; }
}

public class ChatViewModel
{
    public string BotSaid { get; set; }
}
