namespace XYUI.Avalonia.Gallery;

public static partial class XYUI3DocumentationCatalog
{
    static XYUI1ComponentDocument BuildMenuBarDoc(string id, string type) => new(
        id, "菜单栏", "MenuBar",
        "文字主导的桌面一级菜单栏，以轻量 Hover 与底部 Accent 状态线建立导航层级，承载全局一级命令入口。",
        "用于桌面编辑器窗口顶部（文件、编辑、视图、窗口、帮助）；支持鼠标悬停切换、点击打开、键盘 Left/Right 导航与 Esc 关闭。",
        () => XYUI3GalleryCatalog.CreatePreview(id),
        ["<c:XYMenuBar><c:XYMenuBarItem Label=\"文件\" Menu=\"...\" /></c:XYMenuBar>"],
        [new("底部状态线型", "32 DIP 紧凑菜单项，Active 态展示底部 2~3 DIP Accent 线", "Desktop Header")],
        [new("Default", "纯文字透明底，依靠自身 Padding 紧凑排列"), new("Hover", "浅色轻量圆角背景，不改变整体布局"), new("Active", "Accent 文字与底部状态线强调"), new("Focus", "XY.Focus.Control 独立无障碍焦点外框")],
        Properties(id),
        [new("XY.Surface.Panel", "Panel", "菜单栏背景"), new("XY.Border.Color.Subtle", "Subtle", "底部分隔线"), new("XY.Brush.Accent.Default", "Accent", "Active 底部状态线")],
        type)
    {
        CanonicalIdentity = "3.01 · MenuBar / 菜单栏",
        Category = "XYUI-3 · 导航与切换",
        Acceptance = "UI IMPLEMENTED · AWAITING USER VISUAL ACCEPTANCE",
        QuickStartXaml = """
<c:XYMenuBar>
    <c:XYMenuBarItem Label="文件" Menu="{Binding FileMenu}" />
    <c:XYMenuBarItem Label="编辑" Menu="{Binding EditMenu}" />
    <c:XYMenuBarItem Label="视图" Menu="{Binding ViewMenu}" />
    <c:XYMenuBarItem Label="帮助" Menu="{Binding HelpMenu}" />
</c:XYMenuBar>
""",
        CoreRules =
        [
            new("文字主导", "一级菜单栏以纯文字为主，严禁使用大面积按钮边框或高饱和色块将其按钮化。"),
            new("状态指示", "Active 态采用底部 2~3 DIP Accent 状态线，宽度贴近文字，保持桌面级高信息密度。"),
            new("无障碍键盘流", "必须支持 Left/Right 切换相邻菜单项、Enter/Down 打开、Esc 关闭的完整键盘生命周期。")
        ],
        DoDonts =
        [
            new("视觉层级", "DO: 使用轻微 Hover 背景与细底线保持菜单栏轻量化。", "DON'T: 给菜单栏项添加厚重卡片边框或立体阴影。", "菜单栏是一级导航入口，过度装饰会喧宾夺主。"),
            new("尺寸规范", "DO: 菜单项高度统一为 32 DIP，宽度由文字内容驱动。", "DON'T: 手写固定 Width 或硬编码像素 Margin。", "不同语言文字长度差异大，必须自适应。"),
            new("状态设计", "DO: 通过独立 Focus Outline 保证键盘可访问性。", "DON'T: 仅使用鼠标 Hover 颜色代替键盘 Focus。", "无法满足视障与纯键盘操作合规要求。")
        ],
        LiveExamplesFactory = () => XYUI3LiveExamplesFactory.CreateLiveExamples(id)!,
        CompositionFactory = () => XYUI3LiveExamplesFactory.CreateComposition(id)!
    };
}
