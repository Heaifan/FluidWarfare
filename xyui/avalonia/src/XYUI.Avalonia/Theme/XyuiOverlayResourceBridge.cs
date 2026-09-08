using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;

namespace XYUI.Avalonia.Theme;

internal static class XyuiOverlayResourceBridge
{
    internal static void Attach(Control overlay)
    {
        if (!HasThemeDictionary(overlay)) overlay.Resources.MergedDictionaries.Add(XyuiTheme.CreateThemeDictionaries());
    }

    internal static void AttachPopupRoot(TopLevel root)
    {
        Attach(root);
        if (Application.Current is { } app) root.RequestedThemeVariant = app.ActualThemeVariant;
    }

    static bool HasThemeDictionary(Control overlay) => overlay.Resources.MergedDictionaries.OfType<ResourceDictionary>().Any(x => x.ThemeDictionaries.ContainsKey(ThemeVariant.Light));
}
