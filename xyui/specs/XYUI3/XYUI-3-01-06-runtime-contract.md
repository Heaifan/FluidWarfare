# XYUI-3 Round 1 Runtime Contract

状态：TECHNICAL PASS candidate；Presentation 由 Gemini 独立实现。本文只描述 Runtime 公共事实源，禁止 Gallery 通过本地状态模拟。

## 3.01 MenuBar

- Public XAML type: `XYMenuBar`, `XYMenuBarItem`。
- Properties: `Items`, `Label`, `Menu`, `IsActive`, `IsHovered`, `IsEnabled`。
- Events/commands: `Activated`, `OpenMenu`；`Open`、`Close`、`MoveItem`。
- States: closed/open, active, hover, focus, disabled。
- Composition: `XYMenuBar` owns one active `XYMenu`; its popup light-dismiss closes outside and restores focus。
- Variants: top-level menu item; keyboard Alt/Left/Right/Enter/Down/Esc。

## 3.02 Menu

- Public XAML type: `XYMenu`, `XYMenuItem`。
- Properties: `Items`, `Mode`, `IsOpen`, `FocusedIndex`, `FocusRestoreTarget`; item `Id`, `Label`, `Icon`, `Shortcut`, `CheckKind`, `IsChecked`, `IsSelected`, `IsEnabled`, `HasSubMenu`, `SubMenu`。
- Events/commands: `SelectionRequested`, `Invoked`, `SubMenuRequested`, `Opened`, `Closed`；`Open`、`Close`、`MoveFocus`。
- States/variants: Normal, Icon, Shortcut, Checked, Radio, Disabled, Separator, SubMenu；keyboard Up/Down/Left/Right/Enter/Esc。
- Composition: menu item visuals use existing `XYIcon`; nested menus use `XYSubMenu`; `FromModels` is the canonical model adapter。

## 3.03 ContextMenu

- Public XAML type: `XYContextMenu`。
- Properties: `Menu`, `ContextType`, `ContextName`, `Target`/`ContextTarget`。
- Events/commands: `Open(Control)`, `Open()`, `Close`。
- States: closed/open, target-bound, light-dismiss, focus restore。
- Composition: uses the same `XYMenu` infrastructure; target context is the actual `Control`, not a Gallery-only label.

## 3.04 SubMenu

- Public XAML type: `XYSubMenu`。
- Properties: `ParentMenu`, `ChildMenu`, `Trigger`, `ParentSubMenu`, `IsOpen`, `OpenLeft`, `IsPointerTransitioning`。
- Events/commands: `Open`, `Close`, `BeginPointerTransition`, `EndPointerTransition`。
- States: nested open/closed, sibling close, edge flip, parent close, pointer-transition grace period。
- Composition: parent and child are `XYMenu` instances; trigger ownership is explicit or inferred from the parent item.

## 3.05 NavigationMenu

- Public XAML type: `XYNavigationMenu`, `XYNavigationItem`, `XYNavigationGroup`。
- Properties: `Groups`, `Items`, `NavigationState`, `CurrentDestinationId`, compatibility `SelectedId`; item `Id`, `Label`, `Icon`, `Badge`, `Status`, `IsEnabled`, `IsSelected`。
- Events/commands: `NavigationRequested`, `SelectionChanged`, item `Invoked`; `SelectDestination`。
- States/variants: selected, disabled, focus, grouped, badge/status capability。
- Composition: all destination truth lives in shared `XYNavigationState`; `NavigationRequested` can reject before commit.

## 3.06 Sidebar

- Public XAML type: `XYSidebar`。
- Properties: `PrimaryItems`, `ContextItems`, `ContextByNavigationId`, `ContextRegion`, `StickyFooter`, `NavigationState`, `CurrentDestinationId`, `IsCollapsed`, `UserSidebarWidth`, `ExpandedWidth`, `ResizeHandle`。
- Events/commands: `NavigationRequested`, `FooterInvoked`; `Collapse`, `Expand`, `SelectDestination`, `SetUserSidebarWidth`。
- States: expanded, collapsed, resizing, current destination, context region, sticky footer。
- Composition: expanded = primary navigation + context region + sticky footer; collapsed = `XYNavigationRail` projection. Existing `XYNavigationRail` remains the shared state bridge for later 3.07 work; 3.07 is not implemented here.
- Invariant: resize persists `UserSidebarWidth`; collapse uses `CollapsedWidth`; expand restores the user width.

## Known limitations

- Visual fidelity, Gallery sections, XAML documentation layout and human visual acceptance remain outside this Runtime contract.
- Real-window OS focus and touch/real-device acceptance still require user validation.
- No 3.07+ implementation is included.
