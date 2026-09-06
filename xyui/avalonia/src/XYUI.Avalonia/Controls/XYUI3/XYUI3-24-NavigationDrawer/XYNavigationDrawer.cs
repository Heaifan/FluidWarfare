using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.VisualTree;

namespace XYUI.Avalonia.Controls;

public enum XYNavigationDrawerVariant { FullSidebar, Context }
public sealed class XYNavigationDrawerState
{
    public bool IsOpen { get; private set; } public event EventHandler? Changed;
    public void Open() { if (!IsOpen) { IsOpen = true; Changed?.Invoke(this, EventArgs.Empty); } } public void Close() { if (IsOpen) { IsOpen = false; Changed?.Invoke(this, EventArgs.Empty); } }
}
public sealed class XYNavigationDrawer : Border
{
    readonly Popup _popup = new() { Placement = PlacementMode.Center, IsLightDismissEnabled = true }; readonly Border _backdrop = new() { Classes = { "xyui-navigation-drawer-backdrop" }, HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch };
    readonly Grid _overlay = new(); readonly Control _content;
    public XYNavigationState NavigationState { get; } public XYNavigationDrawerState DrawerState { get; } public XYNavigationDrawerVariant Variant { get; } public Popup DrawerPopup => _popup; public Border Backdrop => _backdrop; public XYButton OpenTrigger { get; } = new() { Content = "☰ 导航", Variant = XyuiButtonVariant.Secondary, Classes = { "xyui-navigation-drawer-trigger" }, HorizontalAlignment = HorizontalAlignment.Left };
    public bool IsOpen => DrawerState.IsOpen; public double DrawerWidth { get; set; } = 280; public event EventHandler? Closed;
    public XYNavigationDrawer(XYNavigationState navigationState, XYNavigationDrawerVariant variant = XYNavigationDrawerVariant.FullSidebar, XYNavigationDrawerState? drawerState = null)
    {
        NavigationState = navigationState; DrawerState = drawerState ?? new XYNavigationDrawerState(); Variant = variant; Classes.Add("xyui-navigation-drawer"); OpenTrigger.Click += (_, _) => Open(); _backdrop.PointerPressed += (_, e) => { Close(); e.Handled = true; }; _popup.Closed += (_, _) => { DrawerState.Close(); Closed?.Invoke(this, EventArgs.Empty); OpenTrigger.Focus(); }; _content = BuildContent(); _content.Width = DrawerWidth; _content.Focusable = true; _content.AddHandler(InputElement.KeyDownEvent, OnKeyDown, RoutingStrategies.Bubble); _overlay.ColumnDefinitions = new ColumnDefinitions("Auto,*"); _overlay.Children.Add(_backdrop); _overlay.Children.Add(_content); Grid.SetColumnSpan(_backdrop, 2); Grid.SetColumn(_content, 0); _popup.Child = _overlay; DrawerState.Changed += (_, _) => Sync(); DetachedFromVisualTree += (_, _) => Close(); Child = new Grid { Children = { OpenTrigger, _popup } }; Sync();
    }
    Control BuildContent()
    {
        var surface = new Border { Classes = { "xyui-navigation-drawer-surface" }, Width = DrawerWidth, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Stretch };
        var stack = new Grid { RowDefinitions = new RowDefinitions("Auto,*,Auto") };
        var header = new StackPanel { Margin = new Thickness(16, 16, 16, 12), Spacing = 4, Children = { new TextBlock { Text = "玄域引擎 XuanYu", Classes = { "xyui-navigation-drawer-kicker" } }, new TextBlock { Text = "导航抽屉", Classes = { "xyui-navigation-drawer-title" } } } };
        stack.Children.Add(header);
        Control middle = Variant == XYNavigationDrawerVariant.FullSidebar ? new XYNavigationMenu(NavigationState) { Classes = { "xyui-navigation-drawer-sidebar" } } : new StackPanel { Spacing = 8, Children = { new XYSearchField { Classes = { "xyui-navigation-drawer-search" } }, new XYNavigationMenu(NavigationState) } };
        Grid.SetRow(middle, 1); stack.Children.Add(middle);
        var footerBtn = new XYButton { Content = "收起抽屉", Variant = XyuiButtonVariant.Secondary, Classes = { "xyui-navigation-drawer-footer" }, HorizontalAlignment = HorizontalAlignment.Stretch, Margin = new Thickness(12) };
        footerBtn.Click += (_, _) => Close(); Grid.SetRow(footerBtn, 2); stack.Children.Add(footerBtn);
        surface.Child = stack; return surface;
    }
    void ResizeOverlay()
    {
        var host = this.GetVisualAncestors().OfType<Control>().FirstOrDefault(x => x.Classes.Contains("xyui-navigation-drawer-host")) ?? VisualRoot as Control ?? this;
        _popup.PlacementTarget = host;
        var w = host.Bounds.Width > 0 ? host.Bounds.Width : 480;
        var h = host.Bounds.Height > 0 ? host.Bounds.Height : 400;
        _popup.Width = w; _popup.Height = h; _overlay.Width = w; _overlay.Height = h;
    }
    void Sync()
    {
        _backdrop.IsVisible = DrawerState.IsOpen;
        if (DrawerState.IsOpen) { ResizeOverlay(); _popup.IsOpen = true; _content.Focus(); }
        else { _popup.IsOpen = false; OpenTrigger.Focus(); }
    }
    public void Open() { OpenTrigger.Focus(); DrawerState.Open(); } public void Close() => DrawerState.Close(); public void SelectDestination(string id) => NavigationState.RequestNavigation(id);
    void OnKeyDown(object? sender, KeyEventArgs e) { if (e.Key == Key.Escape && IsOpen) { Close(); e.Handled = true; } }
}
