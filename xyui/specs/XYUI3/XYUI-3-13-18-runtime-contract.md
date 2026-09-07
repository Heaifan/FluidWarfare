# XYUI-3 Round 3 Runtime Contract

状态：Runtime API READY，供 Gallery XAML 消费。

## Navigation Progress

| Control | Public XAML API | Events / interaction | Composition |
|---|---|---|---|
| `XYPagination` | `CurrentPage`/`PageCount`/`TotalItems`, `ShowTotalItems` | `PageChanged`, `InvalidPageRequested`; Previous/Next/First/Last, Left/Right/Home/End, page buttons and Enter/Space activation | `XYIconButton`, `XYNumberField`, `XYSeparator` |
| `XYSteps` | `Items`, `Orientation`, `IsAdaptive`, `IsClickable` | `StepRequested`, `StepChanged`; node `State`, `IsClickable`, pointer activation | `XYStepNode`, `XYIcon`, shared status classes |

`XYStepState` supports `Completed`, `Current`, `Pending`, `Warning`, `Error`; disabled is represented by `IsClickable=false`. Current and focus remain separate facts.

## Command Surfaces

| Control | Public XAML API | Events / interaction | Composition |
|---|---|---|---|
| `XYToolbar` | `Items`, `OverflowItems`, `IsCompact` | tool selection, keyboard navigation, real overflow migration | `XYToolbarTool`, `XYMenu`, `XYIconButton` |
| `XYToolbarTool` | `ToolId`, `Label`, `Icon`, `IsToggle`, `IsSelected`, `IsEnabled`, `Shortcut`, `Tooltip`, `DropdownMenu` | `SelectionRequested`, `DropdownRequested`; Invoke, Enter/Space, disabled guard | `XYIconButton`, `XYMenu` |
| `XYToolGroup` | `Items`, `GroupLabel`, `OwnsSeparator`, `IsCollapsed` | collapsed trigger restores the active tool; group is semantic spacing/separator owner | `XYToolbarTool`, `XYSeparator` |
| `XYCommandBar` | `Items`, `SecondaryCommands`, `OverflowCommands`, `Variant`, `ContextIdentity`; item `CommandId`, `Role`, `Icon`, `Label`, `Shortcut`, `IsDestructive` | `CommandRequested`, `CommandExecuted`; primary/secondary/overflow/destructive paths | `XYButton`, `XYMenu`, `XYIconButton` |

## Command Discovery

`XYCommandPalette` consumes the single `XYPaletteCommand` model through `Items`/`Commands` and optional `RecentItems`. It exposes `OpenShortcut` (default `Ctrl+K`), `Open`, `Close`, `SearchBox`, `ScopeMenu`, `FilteredCommands`, `SelectedCommand`, `IsOpen`, and `ExecuteRequested`. It supports scope filtering, Up/Down/Escape/Enter, disabled and empty/no-result states, shortcut hints, recent commands, and light-dismiss popup behavior.

## Carry-over 3.12

`XYTreeNode` keeps `Children` as the hierarchy source. Round 3 only adds/validates Warning/Status badge rendering and disabled opacity/class. No tree contract or hierarchy rewrite is included.

Known limitation: Toolbar and CommandBar use their existing compact visual styles; persistence, docking, and command registry services remain outside Round 3.
