# XYUI-3 Round 2 Runtime Contract

范围：3.07 NavigationRail、3.08 Tabs、3.09 TabBar、3.10 DockTabs、3.11 Breadcrumb、3.12 TreeNavigation。

## Canonical state

- NavigationRail consumes `XYNavigationState.CurrentDestinationId`; Sidebar and NavigationMenu remain the single navigation truth.
- `XYTabs` owns `Items` and `SelectedTabId`; `XYTab.IsSelected` is synchronized presentation state.
- `XYDockTabs` owns `ActiveTabId` and reuses `XYTab`; it exposes only same-bar close, reorder, drag and `DockHandoffRequested`.
- Tree hierarchy is derived only from `XYTreeNode.Children`; parent lookup traverses that collection and no writable `Parent` property exists.

## XAML consumer contract

| Control | Public declaration | Runtime interaction |
|---|---|---|
| NavigationRail | `NavigationState`, `Items`, `ContextItems`, `Footer`, `ShowExpandButton` | selection, disabled, keyboard, context flyout, expand request |
| Tabs | `Items` with `XYTab` children | selected, modified, closable, close, disabled |
| TabBar | `Items`, `SelectedTabId` with `XYTab` children | previous/next, overflow, new, keyboard selection |
| DockTabs | `Items` with `XYDockTab` / `XYTab` children | active selection, close, drag/reorder, handoff request |
| Breadcrumb | `Items` with `XYBreadcrumbItem` children | ancestor navigation, current guard, dropdown, keyboard |
| TreeNavigation | `Items` with nested `XYTreeNode.Children` | expand/collapse, selection, disabled, keyboard, badge/status |

## Composition

Tabs Family reuses `XYTab`; DockTabs adds only the grip/drop/reorder shell. NavigationRail reuses `XYNavigationState`, `XYNavigationItem`, `XYIcon`, `XYSubMenu` and the existing Popup infrastructure. TreeNavigation uses `XYIcon` and `XYStatusBadge`.

## Known limitations

- Pixel-level vertical alignment and final visual density remain user visual review items.
- Dock handoff is an event boundary only; no Dock Manager, persistence or cross-window engine is implemented.
- TreeNavigation keeps legacy flat `Depth`/`HasChildren` compatibility only; nested `Children` is the canonical hierarchy, and consumers must not author the derived layout values.
