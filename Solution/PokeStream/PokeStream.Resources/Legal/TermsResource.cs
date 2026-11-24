using System.Globalization;
using System.Resources;

namespace PokeStream.Resources.Legal;

/// <summary>Terms and conditions page. Strongly-typed access to <c>TermsResource.resx</c> (add a key there AND here).</summary>
public static class TermsResource
{
    private static readonly ResourceManager Manager = new("PokeStream.Resources.Legal.TermsResource", typeof(TermsResource).Assembly);
    public static string PageTitle => Manager.GetString(nameof(PageTitle), CultureInfo.CurrentUICulture) ?? nameof(PageTitle);
    public static string LastUpdated => Manager.GetString(nameof(LastUpdated), CultureInfo.CurrentUICulture) ?? nameof(LastUpdated);
    public static string Draft => Manager.GetString(nameof(Draft), CultureInfo.CurrentUICulture) ?? nameof(Draft);
    public static string BackHome => Manager.GetString(nameof(BackHome), CultureInfo.CurrentUICulture) ?? nameof(BackHome);
    public static string Section1Title => Manager.GetString(nameof(Section1Title), CultureInfo.CurrentUICulture) ?? nameof(Section1Title);
    public static string Section1Text => Manager.GetString(nameof(Section1Text), CultureInfo.CurrentUICulture) ?? nameof(Section1Text);
    public static string Section2Title => Manager.GetString(nameof(Section2Title), CultureInfo.CurrentUICulture) ?? nameof(Section2Title);
    public static string Section2Text => Manager.GetString(nameof(Section2Text), CultureInfo.CurrentUICulture) ?? nameof(Section2Text);
    public static string Section3Title => Manager.GetString(nameof(Section3Title), CultureInfo.CurrentUICulture) ?? nameof(Section3Title);
    public static string Section3Text => Manager.GetString(nameof(Section3Text), CultureInfo.CurrentUICulture) ?? nameof(Section3Text);
    public static string Section4Title => Manager.GetString(nameof(Section4Title), CultureInfo.CurrentUICulture) ?? nameof(Section4Title);
    public static string Section4Text => Manager.GetString(nameof(Section4Text), CultureInfo.CurrentUICulture) ?? nameof(Section4Text);
    public static string Section5Title => Manager.GetString(nameof(Section5Title), CultureInfo.CurrentUICulture) ?? nameof(Section5Title);
    public static string Section5Text => Manager.GetString(nameof(Section5Text), CultureInfo.CurrentUICulture) ?? nameof(Section5Text);
    public static string Section6Title => Manager.GetString(nameof(Section6Title), CultureInfo.CurrentUICulture) ?? nameof(Section6Title);
    public static string Section6Text => Manager.GetString(nameof(Section6Text), CultureInfo.CurrentUICulture) ?? nameof(Section6Text);
    public static string Section7Title => Manager.GetString(nameof(Section7Title), CultureInfo.CurrentUICulture) ?? nameof(Section7Title);
    public static string Section7Text => Manager.GetString(nameof(Section7Text), CultureInfo.CurrentUICulture) ?? nameof(Section7Text);
    public static string Section8Title => Manager.GetString(nameof(Section8Title), CultureInfo.CurrentUICulture) ?? nameof(Section8Title);
    public static string Section8Text => Manager.GetString(nameof(Section8Text), CultureInfo.CurrentUICulture) ?? nameof(Section8Text);
    public static string Section9Title => Manager.GetString(nameof(Section9Title), CultureInfo.CurrentUICulture) ?? nameof(Section9Title);
    public static string Section9Text => Manager.GetString(nameof(Section9Text), CultureInfo.CurrentUICulture) ?? nameof(Section9Text);
    public static string Section10Title => Manager.GetString(nameof(Section10Title), CultureInfo.CurrentUICulture) ?? nameof(Section10Title);
    public static string Section10Text => Manager.GetString(nameof(Section10Text), CultureInfo.CurrentUICulture) ?? nameof(Section10Text);
    public static string Section11Title => Manager.GetString(nameof(Section11Title), CultureInfo.CurrentUICulture) ?? nameof(Section11Title);
    public static string Section11Text => Manager.GetString(nameof(Section11Text), CultureInfo.CurrentUICulture) ?? nameof(Section11Text);
}
