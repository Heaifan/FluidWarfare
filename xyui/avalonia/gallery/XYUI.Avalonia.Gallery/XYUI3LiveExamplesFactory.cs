using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace XYUI.Avalonia.Gallery;

public static partial class XYUI3LiveExamplesFactory
{
    public static Control? CreateLiveExamples(string id) => id switch
    {
        "XYUI-3-3.01" => CreateMenuBarLiveExamples(),
        "XYUI-3-3.02" => CreateMenuLiveExamples(),
        "XYUI-3-3.03" => CreateContextMenuLiveExamples(),
        "XYUI-3-3.04" => CreateSubMenuLiveExamples(),
        "XYUI-3-3.05" => CreateNavigationMenuLiveExamples(),
        "XYUI-3-3.06" => CreateSidebarLiveExamples(),
        _ => null
    };

    public static Control? CreateComposition(string id) => id switch
    {
        "XYUI-3-3.01" => CreateMenuBarComposition(),
        "XYUI-3-3.02" => CreateMenuComposition(),
        "XYUI-3-3.03" => CreateContextMenuComposition(),
        "XYUI-3-3.04" => CreateSubMenuComposition(),
        "XYUI-3-3.05" => CreateNavigationMenuComposition(),
        "XYUI-3-3.06" => CreateSidebarComposition(),
        _ => null
    };

    internal static Border WrapCard(Control child, string? title = null)
    {
        var panel = new StackPanel { Spacing = 8 };
        if (!string.IsNullOrEmpty(title))
        {
            panel.Children.Add(new TextBlock { Text = title, Classes = { "xyui-text-label" } });
        }
        panel.Children.Add(child);
        return new Border
        {
            Classes = { "xyui-surface-panel" },
            Padding = new global::Avalonia.Thickness(14, 12),
            CornerRadius = new global::Avalonia.CornerRadius(6),
            Child = panel
        };
    }
}
