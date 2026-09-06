using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;
using XYUI.Avalonia.Controls;
using XYUI.Avalonia.Vector;

namespace XYUI.Avalonia.Tests;

[Collection("XyuiHeadless")]
public sealed class XYUI3Round1RuntimeTests : IClassFixture<XyuiHeadlessFixture>
{
    readonly XyuiHeadlessFixture _fx;
    public XYUI3Round1RuntimeTests(XyuiHeadlessFixture fx) => _fx = fx;

    [Fact] public void Menu_runtime_supports_check_radio_disabled_and_keyboard_focus() => _fx.Run(() =>
    {
        var check = new XYMenuItem { CheckKind = XyuiMenuCheckKind.Check }; var radio = new XYMenuItem { CheckKind = XyuiMenuCheckKind.Radio }; var disabled = new XYMenuItem { IsEnabled = false }; var menu = new XYMenu(disabled, check, radio);
        menu.Open(); Assert.Equal(1, menu.FocusedIndex); menu.MoveFocus(1); Assert.Equal(2, menu.FocusedIndex); check.Activate(); Assert.True(check.IsChecked); radio.Activate(); Assert.True(radio.IsChecked); menu.Close(); Assert.False(menu.IsOpen);
    });

    [Fact] public void MenuBar_switches_open_menu_and_restores_focus() => _fx.Run(() =>
    {
        var first = new XYMenuBarItem { Label = "文件", Menu = new XYMenu() }; var second = new XYMenuBarItem { Label = "编辑", Menu = new XYMenu() }; var bar = new XYMenuBar(first, second);
        bar.Open(first); bar.Open(second); Assert.Same(second.Menu, bar.OpenMenu); Assert.False(first.IsActive); Assert.True(second.IsActive); bar.Close(); Assert.Null(bar.OpenMenu);
    });

    [Fact] public void Context_menu_keeps_real_target_and_focus_contract() => _fx.Run(() =>
    {
        var target = new Border(); var context = new XYContextMenu { Menu = new XYMenu(new XYMenuItem { Label = "复制" }) }; context.AttachTo(target); context.Open(target); Assert.Same(target, context.ContextTarget); Assert.True(context.IsOpen); context.Close(); Assert.False(context.IsOpen);
    });

    [Fact] public void SubMenu_shares_menu_items_and_preserves_pointer_transition() => _fx.Run(() =>
    {
        var trigger = new XYMenuItem { Label = "导出", HasSubMenu = true }; var sub = new XYSubMenu { ParentMenu = new XYMenu(trigger), ChildMenu = new XYMenu(new XYMenuItem { Label = "图片" }) }; sub.Close(); trigger.Activate(); Assert.True(sub.IsOpen); sub.BeginPointerTransition(); Assert.True(sub.IsPointerTransitioning); sub.EndPointerTransition(); Assert.False(sub.IsPointerTransitioning); trigger.Activate(); Assert.False(sub.IsOpen);
    });

    [Fact] public void Navigation_menu_uses_one_state_and_rejectable_request() => _fx.Run(() =>
    {
        var state = new XYNavigationState([new("home", "首页", XyuiVectorIcon.Locate), new("data", "数据", XyuiVectorIcon.Code), new("locked", "锁定", XyuiVectorIcon.Info, IsEnabled: false)]); var menu = new XYNavigationMenu(state); var requests = 0; menu.NavigationRequested += (_, request) => { requests++; request.Reject(); };
        Assert.False(menu.SelectDestination("locked")); Assert.False(menu.SelectDestination("data")); Assert.Equal(1, requests); Assert.Equal("home", menu.CurrentDestinationId);
    });

    [Fact] public void Sidebar_restores_user_width_and_keeps_slots() => _fx.Run(() =>
    {
        var state = new XYNavigationState([new("home", "首页", XyuiVectorIcon.Locate)]); var sidebar = new XYSidebar { NavigationState = state, ContextRegion = new Border { Classes = { "context" } }, StickyFooter = new Border { Classes = { "footer" } } };
        sidebar.SetUserSidebarWidth(310); sidebar.Collapse(); Assert.Equal(XYSidebar.CollapsedWidth, sidebar.Width); sidebar.Expand(); Assert.Equal(310, sidebar.Width); Assert.Same(state, sidebar.NavigationState); Assert.Contains(sidebar.GetVisualDescendants(), x => x.Classes.Contains("context")); Assert.Contains(sidebar.GetVisualDescendants(), x => x.Classes.Contains("footer"));
    });
}
