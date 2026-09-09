using Avalonia.VisualTree;
using XYUI.Avalonia.Controls;

namespace XuanYu.Editor.UI;

public partial class Left
{
    void ApplyCompactRailBounds()
    {
        foreach (var item in WorkspaceRail.Items)
        {
            item.Width = 46;
            item.Height = 50;
            foreach (var icon in item.GetVisualDescendants().OfType<XYIcon>())
            {
                icon.Width = 14;
                icon.Height = 14;
            }
        }
    }
}
