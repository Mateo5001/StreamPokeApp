using System.Globalization;
using System.Resources;

namespace PokeStream.Resources.Account;

/// <summary>Account page. Strongly-typed access to <c>AccountResource.resx</c> (add a key there AND here).</summary>
public static class AccountResource
{
    private static readonly ResourceManager Manager = new("PokeStream.Resources.Account.AccountResource", typeof(AccountResource).Assembly);
    public static string PageTitle => Manager.GetString(nameof(PageTitle), CultureInfo.CurrentUICulture) ?? nameof(PageTitle);
    public static string Title => Manager.GetString(nameof(Title), CultureInfo.CurrentUICulture) ?? nameof(Title);
    public static string Username => Manager.GetString(nameof(Username), CultureInfo.CurrentUICulture) ?? nameof(Username);
    public static string Email => Manager.GetString(nameof(Email), CultureInfo.CurrentUICulture) ?? nameof(Email);
    public static string TeamTitle => Manager.GetString(nameof(TeamTitle), CultureInfo.CurrentUICulture) ?? nameof(TeamTitle);
    public static string TeamHint => Manager.GetString(nameof(TeamHint), CultureInfo.CurrentUICulture) ?? nameof(TeamHint);
    public static string EmptySlot => Manager.GetString(nameof(EmptySlot), CultureInfo.CurrentUICulture) ?? nameof(EmptySlot);
    public static string LevelPrefix => Manager.GetString(nameof(LevelPrefix), CultureInfo.CurrentUICulture) ?? nameof(LevelPrefix);
    public static string TwitchHint => Manager.GetString(nameof(TwitchHint), CultureInfo.CurrentUICulture) ?? nameof(TwitchHint);
}
