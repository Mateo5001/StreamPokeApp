using System.Globalization;
using System.Resources;

namespace PokeStream.Resources.Common;

/// <summary>Texts shared by several screens. Strongly-typed access to <c>CommonResource.resx</c> (add a key there AND here).</summary>
public static class CommonResource
{
    private static readonly ResourceManager Manager = new("PokeStream.Resources.Common.CommonResource", typeof(CommonResource).Assembly);
    public static string InternalServerError => Manager.GetString(nameof(InternalServerError), CultureInfo.CurrentUICulture) ?? nameof(InternalServerError);
    public static string ServerUnreachable => Manager.GetString(nameof(ServerUnreachable), CultureInfo.CurrentUICulture) ?? nameof(ServerUnreachable);
}
