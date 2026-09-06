using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using XYUI.Avalonia.Vector;

namespace XYUI.Avalonia.Controls;

public sealed partial class XYNavigationItem : Border
{
    public static readonly StyledProperty<string> IdProperty = AvaloniaProperty.Register<XYNavigationItem, string>(nameof(Id), "");
    public static readonly StyledProperty<string> LabelProperty = AvaloniaProperty.Register<XYNavigationItem, string>(nameof(Label), "");
    public static readonly StyledProperty<XyuiVectorIcon> IconProperty = AvaloniaProperty.Register<XYNavigationItem, XyuiVectorIcon>(nameof(Icon), XyuiVectorIcon.Info);
    public static readonly StyledProperty<bool> IsSelectedProperty = AvaloniaProperty.Register<XYNavigationItem, bool>(nameof(IsSelected));
    public static readonly StyledProperty<bool> IsIconOnlyProperty = AvaloniaProperty.Register<XYNavigationItem, bool>(nameof(IsIconOnly));
    public static readonly StyledProperty<string?> BadgeProperty = AvaloniaProperty.Register<XYNavigationItem, string?>(nameof(Badge));
    public static readonly StyledProperty<XyuiStatusState> StatusProperty = AvaloniaProperty.Register<XYNavigationItem, XyuiStatusState>(nameof(Status));
    public string Id { get => GetValue(IdProperty); set => SetValue(IdProperty, value); }
    public string Label { get => GetValue(LabelProperty); set => SetValue(LabelProperty, value); }
    public XyuiVectorIcon Icon { get => GetValue(IconProperty); set => SetValue(IconProperty, value); }
    public bool IsSelected { get => GetValue(IsSelectedProperty); set => SetValue(IsSelectedProperty, value); }
    public bool IsIconOnly { get => GetValue(IsIconOnlyProperty); set => SetValue(IsIconOnlyProperty, value); }
    public string? Badge { get => GetValue(BadgeProperty); set => SetValue(BadgeProperty, value); }
    public XyuiStatusState Status { get => GetValue(StatusProperty); set => SetValue(StatusProperty, value); }
    public XYNavigationItem() { Classes.Add("xyui-navigation-item"); Build(); }
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change) { base.OnPropertyChanged(change); if (change.Property != ChildProperty) Build(); }
    void Build()
    {
        Classes.Set("xyui-navigation-selected", IsSelected);
        Child = IsIconOnly ? CompactVisual() : FullVisual();
        HookInteraction();
    }
    Grid CompactVisual() => new() { Children = { new Border { Classes = { "xyui-navigation-accent" } }, Content(IsIconOnly ? null : Label, true) } };
    Grid FullVisual() => new() { ColumnDefinitions = new ColumnDefinitions("3,Auto,*,Auto"), Children = { new Border { Classes = { "xyui-navigation-accent" } }, IconView(), Content(Label, false), StatusView() } };
    XYIcon IconView() => new() { Icon = Icon, Size = XyuiIconSize.Small, Classes = { "xyui-navigation-icon" }, [Grid.ColumnProperty] = 1 };
    TextBlock Content(string? text, bool centered) => new() { Text = text, Classes = { "xyui-navigation-label" }, [Grid.ColumnProperty] = centered ? 1 : 2, HorizontalAlignment = centered ? HorizontalAlignment.Center : HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Center };
    Control StatusView() => Badge is not null ? new XYStatusBadge { Text = Badge, State = Status, Classes = { "xyui-navigation-badge" }, [Grid.ColumnProperty] = 3 } : new Border { [Grid.ColumnProperty] = 3 };
}
