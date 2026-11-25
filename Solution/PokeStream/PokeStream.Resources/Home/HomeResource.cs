using System.Globalization;
using System.Resources;

namespace PokeStream.Resources.Home;

/// <summary>Home page. Strongly-typed access to <c>HomeResource.resx</c> (add a key there AND here).</summary>
public static class HomeResource
{
    private static readonly ResourceManager Manager = new("PokeStream.Resources.Home.HomeResource", typeof(HomeResource).Assembly);
    public static string PageTitle => Manager.GetString(nameof(PageTitle), CultureInfo.CurrentUICulture) ?? nameof(PageTitle);
    public static string Welcome => Manager.GetString(nameof(Welcome), CultureInfo.CurrentUICulture) ?? nameof(Welcome);
    public static string SiteName => Manager.GetString(nameof(SiteName), CultureInfo.CurrentUICulture) ?? nameof(SiteName);
    public static string Tagline => Manager.GetString(nameof(Tagline), CultureInfo.CurrentUICulture) ?? nameof(Tagline);
    public static string CreateAccount => Manager.GetString(nameof(CreateAccount), CultureInfo.CurrentUICulture) ?? nameof(CreateAccount);
    public static string SeeFeatures => Manager.GetString(nameof(SeeFeatures), CultureInfo.CurrentUICulture) ?? nameof(SeeFeatures);
    public static string FeaturesTitle => Manager.GetString(nameof(FeaturesTitle), CultureInfo.CurrentUICulture) ?? nameof(FeaturesTitle);
    public static string CaptureTitle => Manager.GetString(nameof(CaptureTitle), CultureInfo.CurrentUICulture) ?? nameof(CaptureTitle);
    public static string CaptureText => Manager.GetString(nameof(CaptureText), CultureInfo.CurrentUICulture) ?? nameof(CaptureText);
    public static string ChatTitle => Manager.GetString(nameof(ChatTitle), CultureInfo.CurrentUICulture) ?? nameof(ChatTitle);
    public static string ChatText => Manager.GetString(nameof(ChatText), CultureInfo.CurrentUICulture) ?? nameof(ChatText);
    public static string CollectionTitle => Manager.GetString(nameof(CollectionTitle), CultureInfo.CurrentUICulture) ?? nameof(CollectionTitle);
    public static string CollectionText => Manager.GetString(nameof(CollectionText), CultureInfo.CurrentUICulture) ?? nameof(CollectionText);
    public static string SyncTitle => Manager.GetString(nameof(SyncTitle), CultureInfo.CurrentUICulture) ?? nameof(SyncTitle);
    public static string SyncText => Manager.GetString(nameof(SyncText), CultureInfo.CurrentUICulture) ?? nameof(SyncText);
    public static string VerifiedTitle => Manager.GetString(nameof(VerifiedTitle), CultureInfo.CurrentUICulture) ?? nameof(VerifiedTitle);
    public static string VerifiedText => Manager.GetString(nameof(VerifiedText), CultureInfo.CurrentUICulture) ?? nameof(VerifiedText);
    public static string RankingsTitle => Manager.GetString(nameof(RankingsTitle), CultureInfo.CurrentUICulture) ?? nameof(RankingsTitle);
    public static string RankingsText => Manager.GetString(nameof(RankingsText), CultureInfo.CurrentUICulture) ?? nameof(RankingsText);
    public static string Terms => Manager.GetString(nameof(Terms), CultureInfo.CurrentUICulture) ?? nameof(Terms);
}
