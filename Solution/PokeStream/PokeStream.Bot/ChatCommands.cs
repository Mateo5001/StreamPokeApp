using PokeStream.Resources.Bot;

namespace PokeStream.Bot;

/// <summary>What the bot answers to a chat line.</summary>
public static class ChatCommands
{
    /// <summary>Reply for <paramref name="text"/>, or null when the bot should stay quiet.</summary>
    public static string? Reply(string text, string chatterId, string botId)
    {
        if (chatterId == botId)
            return null;

        var command = text.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
        return string.Equals(command, "!pokestream", StringComparison.OrdinalIgnoreCase)
            ? BotResource.PokeStreamReply
            : null;
    }
}
