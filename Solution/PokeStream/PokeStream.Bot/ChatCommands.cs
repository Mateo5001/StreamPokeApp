namespace PokeStream.Bot;

public enum ChatCommand { None, PokeStream, Capture, Spawn }

/// <summary>Which command a chat line is.</summary>
public static class ChatCommands
{
    public static ChatCommand Parse(string text, string chatterId, string botId, string broadcasterId)
    {
        if (chatterId == botId)
            return ChatCommand.None;

        return text.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault()?.ToLowerInvariant() switch
        {
            "!pokestream" => ChatCommand.PokeStream,
            "!capturar" => ChatCommand.Capture,
            "!aparecer" when chatterId == broadcasterId => ChatCommand.Spawn, // streamer only, for testing
            _ => ChatCommand.None
        };
    }
}
