namespace XYUI.Avalonia.Gallery;

public static partial class XYUI3DocumentationCatalog
{
    static XYUI1ComponentDocument BuildTreeNavigationDoc(string id, string type) => new(
        id, "树状导航", "TreeNavigation",
        "弱默认引导线与强化祖先链的高密度紧凑树导航，行高 28 DIP，缩进 16 DIP，支持节点展开折叠与精确定位。",
        "用于工程资产、地图要素、Hierarchy 节点等层级数据的快速浏览与单选导航；支持鼠标与键盘箭头展开/折叠。",
        () => XYUI3GalleryCatalog.CreatePreview(id),
        ["<c:XYTreeNavigation><c:XYTreeNode Label=\"地图\" Depth=\"0\" IsSelected=\"True\" /></c:XYTreeNavigation>"],
        [new("Compact Guided Tree", "28 DIP 行高，16 DIP 缩进，包含引导线与折叠箭头", "Desktop Dense Tree")],
        [new("Selected", "左侧 3 DIP Accent Bar + Selected Surface 高亮"), new("Expanded", "旋转 Chevron 下展并展现下级子项"), new("Focused", "键盘操作外框指示")],
        Properties(id),
        [new("XY.Surface.Panel", "Panel", "树容器底色"), new("XY.Brush.Accent.Default", "Accent", "选中指示条与高亮引导线"), new("XY.Border.Color.Subtle", "Subtle", "弱化默认引导线")],
        type)
    {
        CanonicalIdentity = "3.12 · TreeNavigation / 树状导航",
        Category = "XYUI-3 · 导航与切换",
        Acceptance = "UI + INTERACTION IMPLEMENTED · AWAITING USER VISUAL ACCEPTANCE · AWAITING USER INTERACTION ACCEPTANCE",
        QuickStartXaml = """
<c:XYTreeNavigation>
    <c:XYTreeNode Label="地图系统" Depth="0" HasChildren="True" IsExpanded="True" Icon="Locate" />
    <c:XYTreeNode Label="基础网格" Depth="1" IsSelected="True" Icon="Section" />
    <c:XYTreeNode Label="环境要素" Depth="1" HasChildren="True" Icon="Eye" />
    <c:XYTreeNode Label="数据集合" Depth="0" Icon="Code" />
</c:XYTreeNavigation>
""",
        CoreRules =
        [
            new("高密度与低装饰", "行高严格统一为 28 DIP，引导线细密浅淡，不做大面积粗糙卡片或文件树过度装饰。"),
            new("祖先导线高亮机制", "当前选中项通过 ActiveGuideDepth 向上高亮直系父级导线，便于在深层树中快速辨识归属。"),
            new("键盘与鼠标同权", "支持 Left/Right 收起与展开、Up/Down 邻近遍历，交互生命周期完全闭环。")
        ],
        DoDonts =
        [
            new("信息密度控制", "DO: 严格使用 28 DIP 紧凑行高，保持桌面引擎级高信息承载量。", "DON'T: 随意放大行高至 40+ DIP 将其按钮化。", "稀疏树结构会极大地削弱深层级结构的纵览效率。"),
            new("动态收拢性", "DO: 父节点收起时，子孙节点自动隐藏并同步维护焦点。", "DON'T: 仅转动箭头图标而子级内容仍然残留展现。", "伪折叠会让界面失去折叠功能应有的空间释放价值。"),
            new("语义定位", "DO: 将全局层级与场景结构导航使用 TreeNavigation。", "DON'T: 把普通扁平列表强行套用为树导航。", "无层级关系的内容使用普通菜单或列表更为高效。")
        ],
        LiveExamplesFactory = () => XYUI3LiveExamplesFactory.CreateLiveExamples(id)!,
        CompositionFactory = () => XYUI3LiveExamplesFactory.CreateComposition(id)!
    };
}
