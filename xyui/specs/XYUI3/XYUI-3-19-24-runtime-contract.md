# XYUI-3 Round 4 Runtime Contract

状态：Runtime API READY，供 Gallery XAML 消费与最终技术验收。

## 3.19 BackForwardNavigation

| Control | Public XAML API | Events / Interaction | Composition |
|---|---|---|---|
| `XYBackForwardNavigation` | `CurrentLocation`, `CurrentIndex`, `CanGoBack`, `CanGoForward`, `History` | `LocationChanged`, `Back()`, `Forward()`, `Navigate(string)`, right-click history popup, Alt+Left / Alt+Right | `XYIconButton`, `XYMenu`, `Border`, `Grid` |

- 线性历史单项事实源，当前索引到达起点禁用 Back，到达终点禁用 Forward。
- 在中间历史节点发起新导航时截断后续所有旧 Forward 分支。

## 3.20 WorkspaceSwitcher

| Control | Public XAML API | Events / Interaction | Composition |
|---|---|---|---|
| `XYWorkspaceSwitcher` | `Workspaces`, `State`, `CurrentWorkspace`, `Trigger`, `WorkspacePopup` | `WorkspaceChangeRequested`, `WorkspaceChanged`, `ManageRequested`, `SelectWorkspace(id)`, `CommitWorkspace(id)`, `Open()`, `ClosePopup()` | `XYButton`, `XYMenu`, `XYMenuItem`, `Popup` |
| `XYWorkspaceItem` | `Id`, `Label`, `IsEnabled` (default `true`), `Icon` (optional `XyuiVectorIcon?`) | Immutable record; disabled items suppress pointer/keyboard selection and emit no change requests | `XYMenuItem` binding |

- 下拉浮层宽度严格对齐触发按钮。
- Request-Commit 事务机制，支持在有未保存变更时拦截并保持原状态。

## 3.21 ViewSwitcher

| Control | Public XAML API | Events / Interaction | Composition |
|---|---|---|---|
| `XYViewSwitcher` | `State`, `Variant` (`Segmented`, `Dropdown`, `PrimaryMore`), `CurrentViewId` | `ViewChangeRequested`, `SelectView(id)`, `Open()`, `ClosePopup()` | `XYButton`, `XYIconButton`, `XYMenu`, `Popup` |

- 同一业务数据不同呈现视角切换，严禁与独立文档生命周期的 Tabs 混淆。
- 所有变体共享同一 `XYViewState`。`PrimaryMore` 按 Priority 分流高频视口与溢出项。

## 3.22 TableOfContents

| Control | Public XAML API | Events / Interaction | Composition |
|---|---|---|---|
| `XYTableOfContents` | `State`, `Variant` (`Hierarchical`, `Compact`), `CurrentSectionId` | `SectionRequested`, `SelectSection(id)`, `OpenPopup()`, `ClosePopup()` | `XYButton`, `XYTocItem`, `Popup`, continuous level guide line |

- 限深两级（Level 1 根章节，Level 2 子章节），禁止更深层级退化为业务树。
- 二级子章节左侧由连续 `xyui-toc-level-guide` 线贯穿，当前子章节高亮时对应父章节保持微高亮状态。

## 3.23 BottomNavigation

| Control | Public XAML API | Events / Interaction | Composition |
|---|---|---|---|
| `XYBottomNavigation` | `NavigationState`, `Items`, `PrimaryAction`, `CurrentDestinationId`, `SafeAreaBottom` | `DestinationRequested`, `PrimaryActionRequested`, `DestinationChanged`, `SelectDestination(id)`, `CommitDestination(id)` | `XYBottomDestination`, `XYIcon`, `XYStatusBadge`, `XYButton` |

- 3~5 个核心目的地等宽平分槽位，当前选中态切换绝不改变任何槽位宽度。
- 图标在上、标签在下垂直排列。支持角标。中央 `PrimaryAction` 语义解耦，不占目的地状态。
- `SafeAreaBottom` 属性增加宿主底部安全区并保留目的地触控高度，不压缩核心命中区。

## 3.24 NavigationDrawer

| Control | Public XAML API | Events / Interaction | Composition |
|---|---|---|---|
| `XYNavigationDrawer` | `NavigationState`, `DrawerState`, `Variant` (`FullSidebar`, `Context`), `DrawerWidth`, `IsOpen` | `Open()`, `Close()`, `SelectDestination(id)`, `Closed` | `XYNavigationMenu`, `XYSearchField`, `Border` (Backdrop), `Popup` |

- 模态弹出容器，具备半透明 Backdrop 遮罩、Light Dismiss、Esc 键退出与卸载关闭生命周期。
- 内部复用 `XYNavigationMenu` 与外部共享 `XYNavigationState`，保持双向同步。
