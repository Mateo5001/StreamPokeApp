using System.Globalization;
using System.Resources;

namespace PokeStream.Resources.Bot;

/// <summary>Chat bot messages. Strongly-typed access to <c>BotResource.resx</c> (add a key there AND here).</summary>
public static class BotResource
{
    private static readonly ResourceManager Manager = new("PokeStream.Resources.Bot.BotResource", typeof(BotResource).Assembly);
    public static string PokeStreamReply => Manager.GetString(nameof(PokeStreamReply), CultureInfo.CurrentUICulture) ?? nameof(PokeStreamReply);
}
