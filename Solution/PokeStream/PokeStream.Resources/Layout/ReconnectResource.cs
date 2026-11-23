using System.Globalization;
using System.Resources;

namespace PokeStream.Resources.Layout;

/// <summary>Blazor reconnection dialog. Strongly-typed access to <c>ReconnectResource.resx</c> (add a key there AND here).</summary>
public static class ReconnectResource
{
    private static readonly ResourceManager Manager = new("PokeStream.Resources.Layout.ReconnectResource", typeof(ReconnectResource).Assembly);
    public static string Rejoining => Manager.GetString(nameof(Rejoining), CultureInfo.CurrentUICulture) ?? nameof(Rejoining);
    public static string RepeatedAttemptPrefix => Manager.GetString(nameof(RepeatedAttemptPrefix), CultureInfo.CurrentUICulture) ?? nameof(RepeatedAttemptPrefix);
    public static string RepeatedAttemptSuffix => Manager.GetString(nameof(RepeatedAttemptSuffix), CultureInfo.CurrentUICulture) ?? nameof(RepeatedAttemptSuffix);
    public static string RejoinFailed => Manager.GetString(nameof(RejoinFailed), CultureInfo.CurrentUICulture) ?? nameof(RejoinFailed);
    public static string RetryOrReload => Manager.GetString(nameof(RetryOrReload), CultureInfo.CurrentUICulture) ?? nameof(RetryOrReload);
    public static string Retry => Manager.GetString(nameof(Retry), CultureInfo.CurrentUICulture) ?? nameof(Retry);
    public static string Paused => Manager.GetString(nameof(Paused), CultureInfo.CurrentUICulture) ?? nameof(Paused);
    public static string ResumeFailed => Manager.GetString(nameof(ResumeFailed), CultureInfo.CurrentUICulture) ?? nameof(ResumeFailed);
    public static string Resume => Manager.GetString(nameof(Resume), CultureInfo.CurrentUICulture) ?? nameof(Resume);
}
