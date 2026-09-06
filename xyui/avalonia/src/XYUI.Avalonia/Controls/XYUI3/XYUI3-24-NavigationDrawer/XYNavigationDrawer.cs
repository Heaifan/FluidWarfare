using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;

namespace XYUI.Avalonia.Controls;

public enum XYNavigationDrawerVariant { FullSidebar, Context }
public sealed class XYNavigationDrawerState
{
    public bool IsOpen { get; private set; } public event EventHandler? Changed;
    public void Open() { if (!IsOpen) { IsOpen = true; Changed?.Invoke(this, EventArgs.Empty); } } public void Close() { if (IsOpen) { IsOpen = false; Changed?.Invoke(this, EventArgs.Empty); } }
}
public sealed class XYNavigationDrawer : Border
{
    readonly Popup _popup = new() { Placement = PlacementMode.Right, IsLightDismissEnabled = true }; readonly Border _backdrop = new() { Classes = { "xyui-navigation-drawer-backdrop" } };
    public XYNavigationState NavigationState { get; } public XYNavigationDrawerState DrawerState { get; } public XYNavigationDrawerVariant Variant { get; } public Popup DrawerPopup => _popup; public Border Backdrop => _backdrop; public XYButton OpenTrigger { get; } = new() { Content = "导航", Variant = XyuiButtonVariant.Secondary, Classes = { "xyui-navigation-drawer-trigger" } };
    public bool IsOpen => DrawerState.IsOpen; public double DrawerWidth { get; set; } = 280; public event EventHandler? Closed;
    public XYNavigationDrawer(XYNavigationState navigationState, XYNavigationDrawerVariant variant = XYNavigationDrawerVariant.FullSidebar, XYNavigationDrawerState? drawerState = null) { NavigationState = navigationState; DrawerState = drawerState ?? new XYNavigationDrawerState(); Variant = variant; Classes.Add("xyui-navigation-drawer"); OpenTrigger.Click += (_, _) => Open(); _backdrop.PointerPressed += (_, _) => Close(); _popup.Closed += (_, _) => { DrawerState.Close(); Closed?.Invoke(this, EventArgs.Empty); }; var content = BuildContent(); content.AddHandler(InputElement.KeyDownEvent, OnKeyDown, RoutingStrategies.Bubble); _popup.Child = new Grid { ColumnDefinitions = new ColumnDefinitions("*,Auto"), Children = { _backdrop, content } }; Grid.SetColumn(content, 1); DrawerState.Changed += (_, _) => Sync(); DetachedFromVisualTree += (_, _) => Close(); Child = OpenTrigger; Sync(); }
    Control BuildContent() => Variant == XYNavigationDrawerVariant.FullSidebar ? new XYNavigationMenu(NavigationState) { Classes = { "xyui-navigation-drawer-sidebar" }, Width = DrawerWidth } : new StackPanel { Spacing = 8, Children = { new XYSearchField { Classes = { "xyui-navigation-drawer-search" } }, new XYNavigationMenu(NavigationState) } };
    void Sync() { _backdrop.IsVisible = DrawerState.IsOpen; _popup.IsOpen = DrawerState.IsOpen; if (!DrawerState.IsOpen) OpenTrigger.Focus(); }
    public void Open() { OpenTrigger.Focus(); DrawerState.Open(); } public void Close() => DrawerState.Close(); public void SelectDestination(string id) => NavigationState.RequestNavigation(id);
    void OnKeyDown(object? sender, KeyEventArgs e) { if (e.Key == Key.Escape && IsOpen) { Close(); e.Handled = true; } }
}
