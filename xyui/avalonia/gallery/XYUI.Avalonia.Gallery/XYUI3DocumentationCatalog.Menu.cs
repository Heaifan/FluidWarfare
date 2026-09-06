namespace XYUI.Avalonia.Gallery;

public static partial class XYUI3DocumentationCatalog
{
    static XYUI1ComponentDocument BuildMenuDoc(string id, string type) => new(
        id, "菜单", "Menu",
        "标准桌面级浮动命令面板，严格提供 Leading、Label、Shortcut、Chevron 稳定四列布局与命令分组。",
        "作为 MenuBar、ContextMenu、SubMenu 的底层命令容器；支持鼠标悬浮、点击执行、Up/Down 键盘导航与 Esc 关闭。",
        () => XYUI3GalleryCatalog.CreatePreview(id),
        ["<c:XYMenu><c:XYMenuItem Label=\"保存\" Shortcut=\"Ctrl+S\" /></c:XYMenu>"],
        [new("标准桌面型", "浮动 Overlay 面板，紧凑行高 (28~32 DIP)，细边框与投影", "Overlay Surface"), new("嵌入型 (Embedded)", "作为工具箱或右键面板内部嵌入使用", "In-Panel")],
        [new("Normal", "标准文本命令项"), new("Icon", "左侧展示矢量小图标"), new("Shortcut", "右侧稳定对齐快捷键"), new("Checked / Radio", "左侧对齐勾选标记或互斥单选圆点"), new("Disabled", "低对比度置灰，禁止响应交互"), new("Destructive", "低饱和危险警示文字")],
        Properties(id),
        [new("XY.Surface.Overlay", "Overlay", "菜单浮动面板背景"), new("XY.Shadow.Popup", "Popup", "浮层阴影"), new("XY.Border.Color.Subtle", "Subtle", "面板边框与分隔线")],
        type)
    {
        CanonicalIdentity = "3.02 · Menu / 菜单",
        Category = "XYUI-3 · 导航与切换",
        Acceptance = "UI IMPLEMENTED · AWAITING USER VISUAL ACCEPTANCE",
        QuickStartXaml = """
<c:XYMenu>
    <c:XYMenuItem Label="新建项目" Shortcut="Ctrl+N" Icon="Add" />
    <c:XYMenuItem Label="打开工程" Shortcut="Ctrl+O" Icon="Browse" />
    <c:XYMenu.Separator />
    <c:XYMenuItem Label="显示网格" CheckKind="Check" IsChecked="True" />
    <c:XYMenuItem Label="删除图层" IsDestructive="True" />
</c:XYMenu>
""",
        CoreRules =
        [
            new("稳定四列结构", "Leading (24 DIP) + Label (*) + Shortcut (Auto) + Chevron (24 DIP) 纵向绝对对齐，禁止行间漂移。"),
            new("结构化分组", "使用 XYMenu.Separator() 明确划分命令逻辑簇，避免长列表无断点堆积。"),
            new("危险命令隔离", "删除、格式化等危险操作使用 Destructive 语义置于独立末尾分组，降低误触风险。")
        ],
        DoDonts =
        [
            new("列对齐", "DO: 所有项共享统一的 Leading 与 Chevron 槽位宽度。", "DON'T: 某一行动态缩进导致文字左右参差不齐。", "视觉漂移会严重降低视线扫描效率。"),
            new("快捷键呈现", "DO: 快捷键统一右对齐，与主命令文本保持充分空白。", "DON'T: 快捷键紧贴命令文本尾部排版。", "右对齐是桌面编辑器菜单的标准视觉认知。"),
            new("真实 Runtime", "DO: 使用真实 XYMenu 与 XYMenuItem 组件构建交互。", "DON'T: 使用普通 ListBox 或 StackPanel 手绘假菜单。", "假菜单无法继承无障碍与键盘交互规范。")
        ],
        LiveExamplesFactory = () => XYUI3LiveExamplesFactory.CreateLiveExamples(id)!,
        CompositionFactory = () => XYUI3LiveExamplesFactory.CreateComposition(id)!
    };
}
