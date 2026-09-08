# XYUI3 菜单体系开发者快速接入指南 (Developer Quick Start)

## 一、核心能力清单

| 特性 | API / 属性 | 说明 |
| :--- | :--- | :--- |
| **Command** | `XYMenuItem.Command` | 支持标准 `ICommand`，兼容 `Action` / `Delegate`，由 VM 持有命令逻辑 |
| **CommandParameter** | `XYMenuItem.CommandParameter` | 传给命令的参数，支持同一个 `ICommand` 实例复用于多个菜单项 |
| **CanExecute** | `ICommand.CanExecuteChanged` | 自动监听命令可用性变更，驱动 `IsEnabled`，45% 禁用置灰无需 View 手动控色 |
| **IsChecked** | `XYMenuItem.IsChecked` | 双向绑定状态（`Mode=TwoWay`），ViewModel / 外部状态为唯一真源 |
| **Check** | `CheckKind="Check"` | 复选模式，左侧显示标准 Check 图标 |
| **Radio** | `CheckKind="Radio"` | 单选模式，左侧显示单选圆点，同一逻辑分组由外部状态驱动互斥 |
| **Items** | `[Content] IList<Control> Items` | 声明式 AXAML 直接嵌套，无需后台代码动态创建 |
| **Compact** | `Classes="compact"` | 紧凑顶栏模式（高度 34 DIP，透明底），适合桌面游戏/场景编辑器 |

---

## 二、组件选型决策矩阵 (When to Use)

| 场景需求 | 推荐选用组件 | 典型示例 | 不推荐做法 |
| :--- | :--- | :--- | :--- |
| **全局一级横向主菜单** | `XYMenuBar` | 窗口顶栏：文件、编辑、视图、窗口、帮助 | 禁止用厚重按钮边框包装 |
| **局部单一按钮下拉菜单** | `XYDropDownButton` | 工具栏“更多操作”、变换模式下拉、导出下拉 | 不要手写单项 MenuBar |
| **菜单内状态切换** | `XYMenuItem (Check)` | 视图辅助线：构造网格、世界原点、坐标轴 | 不要在菜单内嵌入普通 Checkbox |
| **菜单内互斥模式切换** | `XYMenuItem (Radio)` | 工作区切换：地图编辑 vs 区域编辑；透视 vs 正交 | 不要在菜单内自行维护独立业务状态 |

---

## 三、标准声明式 AXAML 模板 (Ready-to-Copy)

```xml
<xy:XYMenuBar Classes="compact" ShowDivider="False">
  <!-- 1. 命令与参数复用示例 -->
  <xy:XYMenuBarItem Header="文件">
    <xy:XYMenu>
      <xy:XYMenuItem Header="新建项目"
                     Command="{Binding RunCommand}"
                     CommandParameter="新建项目"
                     Shortcut="Ctrl+N" />
      <xy:XYMenuItem Header="打开工程"
                     Command="{Binding RunCommand}"
                     CommandParameter="打开工程"
                     Shortcut="Ctrl+O" />
      <xy:XYMenuItem Header="保存场景"
                     Command="{Binding RunCommand}"
                     CommandParameter="保存场景"
                     Shortcut="Ctrl+S" />
      <xy:XYMenu.Separator />
      <!-- CanExecute=false 自动触发 Disabled 视觉 -->
      <xy:XYMenuItem Header="只读锁定"
                     Command="{Binding DisabledCommand}" />
    </xy:XYMenu>
  </xy:XYMenuBarItem>

  <!-- 2. 互斥单选组示例 (Radio) -->
  <xy:XYMenuBarItem Header="工作区" ShowChevron="True">
    <xy:XYMenu>
      <xy:XYMenuItem Header="地图编辑"
                     CheckKind="Radio"
                     IsChecked="{Binding IsMapEditor, Mode=TwoWay}" />
      <xy:XYMenuItem Header="区域编辑"
                     CheckKind="Radio"
                     IsChecked="{Binding IsRegionEditor, Mode=TwoWay}" />
    </xy:XYMenu>
  </xy:XYMenuBarItem>

  <!-- 3. 复选状态开关示例 (Check) -->
  <xy:XYMenuBarItem Header="视图">
    <xy:XYMenu>
      <xy:XYMenuItem Header="构造网格"
                     CheckKind="Check"
                     IsChecked="{Binding IsGridVisible, Mode=TwoWay}" />
      <xy:XYMenuItem Header="世界原点"
                     CheckKind="Check"
                     IsChecked="{Binding IsOriginVisible, Mode=TwoWay}" />
    </xy:XYMenu>
  </xy:XYMenuBarItem>
</xy:XYMenuBar>
```
