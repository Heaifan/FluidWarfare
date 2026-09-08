namespace XYUI.Avalonia.Gallery;

public static partial class XYUI3DocumentationCatalog
{
    static IReadOnlyList<XYUIDocProperty> Properties(string id) => id switch
    {
        "XYUI-3-3.01" => MenuBarProperties(),
        "XYUI-3-3.02" => MenuProperties(),
        "XYUI-3-3.23" => BottomNavProperties(),
        _ => []
    };

    static IReadOnlyList<XYUIDocProperty> MenuBarProperties() =>
    [
        P("XYMenuBar.Items", "IList<XYMenuBarItem>", "[]", "一级菜单项集合，AXAML [Content] 声明式挂载。"),
        P("XYMenuBar.ShowDivider", "bool", "true", "是否渲染底部分隔线，顶栏紧凑场景设为 False。"),
        P("XYMenuBar.Classes=\"compact\"", "string", "—", "紧凑顶栏样式（34 DIP 高度，透明背景）。"),
        P("XYMenuBarItem.Header / Label", "string", "\"\"", "一级菜单项标题文本。"),
        P("XYMenuBarItem.Menu", "XYMenu?", "null", "展开的浮动菜单容器，AXAML [Content] 声明式嵌套。"),
        P("XYMenuBarItem.ShowChevron", "bool", "false", "是否在标题右侧显示 Chevron 下拉箭头。"),
        P("XYMenuBarItem.IsActive", "bool", "false", "当前菜单项是否处于激活状态（底部 Accent 线）。")
    ];

    static IReadOnlyList<XYUIDocProperty> MenuProperties() =>
    [
        P("XYMenu.Items", "IList<Control>", "[]", "菜单命令项集合，AXAML [Content] 声明式挂载。"),
        P("XYMenuItem.Command", "object? (ICommand)", "null", "触发执行命令，支持 ICommand 绑定与 Action 委托。"),
        P("XYMenuItem.CommandParameter", "object?", "null", "命令参数，支持同一 ICommand 复用于多个菜单项。"),
        P("XYMenuItem.IsChecked", "bool", "false", "勾选/单选状态，支持 TwoWay 绑定外部 VM 真源。"),
        P("XYMenuItem.CheckKind", "XyuiMenuCheckKind", "None", "枚举状态：None、Check、Radio。"),
        P("XYMenuItem.Shortcut", "string", "\"\"", "右侧稳定对齐快捷键提示文本（如 Ctrl+S）。"),
        P("XYMenuItem.Icon", "XyuiVectorIcon?", "null", "左侧前缀矢量图标。")
    ];

    static IReadOnlyList<XYUIDocProperty> BottomNavProperties() =>
    [
        P("XYBottomNavigationItem.Id", "string", "必填", "目的地唯一标识；SelectDestination 使用它。"),
        P("XYBottomNavigationItem.Label", "string", "必填", "目的地显示文本。"),
        P("XYBottomNavigationItem.Icon", "XyuiVectorIcon", "必填", "来自 XYUI Vector Icon Registry。"),
        P("XYBottomNavigationItem.Badge", "string?", "null", "可选状态提示文本；null 时不显示 Badge。"),
        P("XYBottomNavigationItem.IsEnabled", "bool", "true", "false 时目的地不可点击。"),
        P("NavigationState", "XYNavigationState", "必填", "共享目的地与当前 SelectedId 的状态源。"),
        P("Items", "IReadOnlyList<XYBottomNavigationItem>", "state.Entries", "只读目的地集合，按等宽 Slot 渲染。")
    ];

    static XYUIDocProperty P(string name, string type, string value, string description) =>
        new(name, type, value, description);
}
