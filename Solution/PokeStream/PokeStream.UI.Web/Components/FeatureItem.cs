namespace PokeStream.UI.Web.Components;

/// <summary>
/// Shared feature item model used by FeatureCarousel and FeatureCard components.
/// </summary>
public class FeatureItem
{
    public string Icon { get; set; } = "";
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Gradient { get; set; } = "";
    public string IconColor { get; set; } = "";
}