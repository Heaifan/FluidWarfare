using Avalonia.Input;

namespace XYUI.Avalonia.Controls;

public sealed partial class XYMenuItem
{
    public event EventHandler? Invoked;
    public event EventHandler? SelectionRequested;
    public event EventHandler? SubMenuRequested;
    public Action? Command { get; set; }
    public bool IsSubMenuOpen { get; private set; }
    void InitializeInteraction()
    {
        Focusable = true; PointerEntered += (_, _) => IsHovered = true; PointerExited += (_, _) => IsHovered = false;
        PointerPressed += OnPointerPressed; KeyDown += OnKeyDown;
    }
    public bool Activate()
    {
        if (!IsEnabled) return false;
        IsSelected = true; SelectionRequested?.Invoke(this, EventArgs.Empty);
        if (CheckKind == XyuiMenuCheckKind.Check) IsChecked = !IsChecked;
        if (CheckKind == XyuiMenuCheckKind.Radio) IsChecked = true;
        if (HasSubMenu || SubMenu is not null)
        {
            if (IsSubMenuOpen) { IsSubMenuOpen = false; SubMenu?.Close(); SubMenuRequested?.Invoke(this, EventArgs.Empty); }
            else { IsSubMenuOpen = true; SubMenu?.Open(); SubMenuRequested?.Invoke(this, EventArgs.Empty); }
            return true;
        }
        Command?.Invoke(); Invoked?.Invoke(this, EventArgs.Empty); return true;
    }
    public void OpenSubMenu() { if (IsEnabled && (HasSubMenu || SubMenu is not null)) { IsSelected = true; IsSubMenuOpen = true; SubMenu?.Open(); SubMenuRequested?.Invoke(this, EventArgs.Empty); } }
    public void CloseSubMenu() { IsSubMenuOpen = false; SubMenu?.Close(); }
    internal void ClearInteractionState() { IsSelected = false; IsSubMenuOpen = false; SubMenu?.Close(); }
    void OnPointerPressed(object? sender, PointerPressedEventArgs e) { if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed) { Activate(); e.Handled = true; } }
    void OnKeyDown(object? sender, KeyEventArgs e) { if (e.Key is Key.Enter or Key.Space or Key.Right) { Activate(); e.Handled = true; } }
}
