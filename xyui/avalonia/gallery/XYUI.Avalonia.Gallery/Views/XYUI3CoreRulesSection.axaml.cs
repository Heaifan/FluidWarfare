using Avalonia;
using Avalonia.Controls;

namespace XYUI.Avalonia.Gallery.Views;

public partial class XYUI3CoreRulesSection : UserControl
{
    public XYUI3CoreRulesSection()
    {
        InitializeComponent();
    }
}

public sealed class XYUI3RuleRow : Panel
{
    const double Breakpoint = 520;
    const double TitleWidth = 210;
    const double Spacing = 16;

    protected override Size MeasureOverride(Size availableSize)
    {
        if (Children.Count < 2) return base.MeasureOverride(availableSize);
        if (availableSize.Width < Breakpoint)
        {
            Children[0].Measure(new Size(availableSize.Width, double.PositiveInfinity));
            Children[1].Measure(new Size(availableSize.Width, double.PositiveInfinity));
            return new Size(availableSize.Width, Children[0].DesiredSize.Height + 6 + Children[1].DesiredSize.Height);
        }
        Children[0].Measure(new Size(TitleWidth, double.PositiveInfinity));
        var contentWidth = Math.Max(0, availableSize.Width - TitleWidth - Spacing);
        Children[1].Measure(new Size(contentWidth, double.PositiveInfinity));
        return new Size(availableSize.Width, Math.Max(Children[0].DesiredSize.Height, Children[1].DesiredSize.Height));
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        if (Children.Count < 2) return base.ArrangeOverride(finalSize);
        if (finalSize.Width < Breakpoint)
        {
            Children[0].Arrange(new Rect(0, 0, finalSize.Width, Children[0].DesiredSize.Height));
            Children[1].Arrange(new Rect(0, Children[0].DesiredSize.Height + 6, finalSize.Width, Children[1].DesiredSize.Height));
            return finalSize;
        }
        Children[0].Arrange(new Rect(0, 0, TitleWidth, Children[0].DesiredSize.Height));
        var contentWidth = Math.Max(0, finalSize.Width - TitleWidth - Spacing);
        Children[1].Arrange(new Rect(TitleWidth + Spacing, 0, contentWidth, Children[1].DesiredSize.Height));
        return finalSize;
    }
}
