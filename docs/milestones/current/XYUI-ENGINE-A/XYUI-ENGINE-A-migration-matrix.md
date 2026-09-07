# XYUI-ENGINE-A Migration Matrix

## Baseline and scope

- Baseline: `feat/XYUI-ENGINE-A` / `93893f8a9e17ec6e9c8a8fb60abd4008f5dca03e`.
- Audit scope: Engine UI integration, XYUI-1~3 migration candidates, legacy style inventory.
- This document is an audit and migration ledger. It does not delete or rewrite legacy UI.
- Agent-B owns only the three first-batch views listed below. Codex does not modify them.

## T1 · Governance and UI integration audit

| Check | Baseline result | Decision |
| --- | --- | --- |
| `XuanYu.Editor.UI` → Runtime | `XuanYu.Editor.UI.csproj` references `..\xyui\avalonia\src\XYUI.Avalonia\XYUI.Avalonia.csproj` | Valid; keep |
| `Editor.App` → Gallery | No direct Gallery project reference; it references `Editor.UI` | Valid; keep |
| `Editor.Win` → Gallery | No Gallery project reference | Valid; keep |
| Root solution | Includes latest Runtime, Gallery, and Tests under `xyui/avalonia` | Valid; keep |
| `XYUIBootstrap.Create()` | Not found in the current Runtime or Engine | GAP; do not invent a second bootstrap |
| Engine XYUI AXAML namespace | No `using:XYUI.Avalonia.Controls` or `xmlns:xy` consumer yet | Migration prerequisite |
| App theme/resource loading | `App.axaml` loads `FluentTheme` and `Editor.UI/Ui.axaml`; it does not load `XyuiTheme` or `XyuiComponentStyles` | GAP; resolve before first live XYUI view |

The reference boundary is present, but the Engine has not started consuming XYUI controls. This is an integration readiness state, not proof that visual migration is complete.

## T2 · Runtime, Theme, and Reference baseline

| Fact | Result |
| --- | --- |
| Canonical Runtime source | `xyui/avalonia/src/XYUI.Avalonia` |
| XYUI-1 public family | 24 components, including `XYText`, `XYLabel`, `XYCaption`, `XYHeading`, `XYSectionTitle`, `XYIcon`, `XYBadge`, `XYSelectableText`, `XYEmptyText`, `XYTruncatedText` |
| XYUI-2 public family | 24 components, including Button, ToggleButton, TextField, ComboBox, Select, and property controls |
| XYUI-3 public family | 24 components, including Menu, ContextMenu, Tabs, TabBar, DockTabs, NavigationMenu, Sidebar, and NavigationRail |
| Gallery dependency from Engine | None |
| Old embedded `XYUI/**` subtree | 34 tracked files; no current solution/project reference |
| Duplicate Runtime/style rule | Do not copy the old subtree or Gallery styles into Engine; migrate through the latest Runtime reference |

## T3 · Engine UI inventory

Counts are opening-element matches in the 44 tracked `XuanYu.Editor.UI/**/*.axaml` files at this baseline.

| Existing element | Count | First migration interpretation |
| --- | ---: | --- |
| `TextBlock` | 178 | XYUI-1 text/label/caption/heading/section/value candidates |
| `Border` | 66 | Keep layout surfaces; migrate only semantic component wrappers |
| `Button` | 63 | XYUI-2 batch; preserve commands and click handlers |
| `ToggleButton` | 13 | XYUI-2 batch; preserve `IsChecked` and command bindings |
| `TextBox` | 13 | XYUI-2 input batch; do not replace in XYUI-1 pass |
| `ComboBox` | 2 | XYUI-2 Select/ComboBox batch |
| `CheckBox` | 0 | No Engine candidate at this baseline |
| `RadioButton` | 0 | No Engine candidate at this baseline |
| `ListBox` | 16 | Keep collection behavior; later compose with relevant XYUI interaction |
| `TreeView` | 0 | No direct TreeView candidate; do not force XYUI-3 TreeNavigation |
| `TabControl` | 3 | XYUI-3 Tabs/TabBar candidates; selection binding must remain canonical |
| `Menu` | 3 | XYUI-3 Menu candidates |
| `ContextMenu` | 1 | XYUI-3 ContextMenu candidate |
| `Slider` | 0 | No Engine candidate at this baseline |
| `Path` | 51 | XYUI-1 Icon candidate where glyph is semantic; preserve layout geometry otherwise |
| `PathIcon` | 8 | XYUI-1 Icon candidate |
| `Style` | 169 | Legacy style inventory; migrate incrementally, do not bulk-delete |
| `ControlTemplate` | 1 | Legacy template; review with XYUI-3 tab migration |
| Hard-coded hex colors | 112 | Legacy token/style inventory; no bulk replacement in R1-A |

## T4 · Migration matrix

| Path | Existing | XYUI target | Module | Category | Risk | Business binding |
| --- | --- | --- | --- | --- | --- | --- |
| `XuanYu.Editor.UI/Foot/NotificationBar.axaml` | PathIcon, TextBlock, count TextBlock, close icon | `XYIcon`, `XYTruncatedText`, `XYBadge`, `XYIcon` | XYUI-1 | A | Low | `HasNotification`, type visibility, text/count bindings, `Dismiss_Click` |
| `XuanYu.Editor.UI/Foot/LogDetailPanel.axaml` | TextBlock labels/values, read-only TextBox | `XYHeading`, `XYEmptyText`, `XYCaption`, `XYLabel`, `XYSelectableText` | XYUI-1 + XYUI-2 later | A | Medium | Selected log visibility, copy action, message/detail/context bindings |
| `XuanYu.Editor.UI/Right/LayerInspectorPanel.axaml` | TextBlock labels/values, separator Border, TextBox, Button | `XYSectionTitle`, `XYSeparator`, `XYLabel`, `XYText`, `XYSelectableText` | XYUI-1 + XYUI-2 later | A | Medium | Layer name edit, current-layer command, layer property bindings |
| `XuanYu.Editor.UI/Right/InspectorPanel.axaml` | Inspector title, labels, values, empty state, icons | XYUI-1 text/icon/empty family | XYUI-1 | A / Batch 2 | Medium | Inspector selection and property rows; frozen this round |
| `XuanYu.Editor.UI/Foot/Foot.axaml` | Log filters, ListBox, TextBlock rows, buttons | XYUI-1 display + XYUI-2 Button/Select later | XYUI-1/2 | B | High | Log filters, selection, scroll, clear-filter commands |
| `XuanYu.Editor.UI/Left/Left.axaml` | Search TextBox, ListBox, TabControl, Path icons | XYUI-1 display + XYUI-2 Search/Field + XYUI-3 Tabs | XYUI-1/2/3 | B | High | Project tree selection, expansion, left tab state |
| `XuanYu.Editor.UI/Right/EditorRightTabs.axaml` | TabControl and debug data rows | XYUI-3 Tabs/TabBar + XYUI-1 display | XYUI-1/3 | B | High | `RightTabIndex`, debug context and interaction state |
| `XuanYu.Editor.UI/Right/TopTabStripTemplate.axaml` | Custom tab strip template and navigation buttons | XYUI-3 TabBar/DockTabs handoff | XYUI-3 | B/D | High | Active document tab, overflow, scroll, close behavior |
| `XuanYu.Editor.UI/Right/MapEditorPanel.axaml` | TabControl and map panels | XYUI-3 Tabs | XYUI-3 | B | Medium | Map editor tab selection |
| `XuanYu.Editor.UI/Top/Top.axaml` | Menu, ToggleButton, Path icons, text | XYUI-3 Menu + XYUI-2 ToggleButton + XYUI-1 display | XYUI-1/2/3 | B | High | Editor mode, snap, environment commands |
| `XuanYu.Editor.UI/Workspace/WorkspaceSelector.axaml` | Buttons and Menu | XYUI-2 Button + XYUI-3 Menu | XYUI-2/3 | B | Medium | Manage/edit mode switching |
| `XuanYu.Editor.UI/Right/LayerPanel.axaml` | ListBox, TextBox, ToggleButtons, Path icons | XYUI-1 display + XYUI-2 inputs/toggles | XYUI-1/2 | B | High | Layer reorder, rename, visibility, lock state |
| `XuanYu.Editor.UI/Right/MapFormPanel.axaml` | TextBox, Button, labels, error text | XYUI-1 display + XYUI-2 property/input controls | XYUI-1/2 | B | High | Map edit/validate/apply/undo/redo commands |
| `XuanYu.Editor.UI/Right/MapPagePanel.axaml` | Labels, values, buttons, copy action | XYUI-1 display + XYUI-2 Button/SelectableText | XYUI-1/2 | B | Medium | Map manifest and map command bindings |
| `XuanYu.Editor.UI/Right/DatasetPanel.axaml` | ComboBox, labels, buttons | XYUI-1 display + XYUI-2 Select/ComboBox | XYUI-1/2 | B | High | Dataset create/select actions |
| `XuanYu.Editor.UI/Right/DatasetLayerPanel.axaml` | Layer collection and action controls | XYUI-1 display + XYUI-2 controls | XYUI-1/2 | B | High | Dataset layer operations |
| `XuanYu.Editor.UI/Right/EditorLayerDock.axaml` | Dock/layout shell and controls | XYUI-1 display + XYUI-3 DockTabs only if contract matches | XYUI-1/3 | B | High | Editor layer dock state |
| `XuanYu.Editor.UI/Left/RegionPanel.axaml` | Region list/form primitives | XYUI-1 display + XYUI-2 controls | XYUI-1/2 | B | Medium | Region authoring state |
| `XuanYu.Editor.UI/Left/RoadPanel.axaml` | Road list/form primitives | XYUI-1 display + XYUI-2 controls | XYUI-1/2 | B | Medium | Road authoring state |
| `XuanYu.Editor.UI/Left/MarkerPanel.axaml` | Marker list/form primitives | XYUI-1 display + XYUI-2 controls | XYUI-1/2 | B | Medium | Marker authoring state |
| `XuanYu.Editor.UI/Left/RegionalAuthoringPanel.axaml` | Region authoring display | XYUI-1 display family | XYUI-1 | B | Medium | Region authoring mode |
| `XuanYu.Editor.UI/Root/UiRoot.axaml` | Grid, Border, GridSplitter shell | No direct XYUI-1~3 target | Foundation/layout | C | High | Window geometry and resize behavior |
| `XuanYu.Editor.UI/Viewport/Vulkan/VulkanViewport.axaml` | Native viewport host and fallback surface | No direct XYUI-1~3 target | Engine/render | C | High | Vulkan host and fallback behavior |

### Candidate summary

- XYUI-1 direct first batch: 3 views, frozen to Agent-B.
- XYUI-1 Batch 2 candidate: `Right/InspectorPanel.axaml`; explicitly not touched in R1-A.
- XYUI-1 additional candidates: Foot, Left, Top, Right display/value surfaces, and authoring panels.
- XYUI-2 candidates: Button/ToggleButton/TextBox/ComboBox and property/input surfaces in the mixed views above.
- XYUI-3 candidates: 3 `Menu`, 1 `ContextMenu`, 3 `TabControl`, plus the custom tab strip contract.
- GAP: no direct Engine candidates for CheckBox, RadioButton, Slider, TreeView; no `XYUIBootstrap.Create()`; App theme/style loading is not wired.
- Legacy duplicate: old `XYUI/**` subtree (34 tracked files), Engine `Design/UiStyles*.axaml`, `Ui.axaml`, per-view styles, and custom tab template. Inventory only; no bulk deletion in R1-A.

## T5 · Legacy / duplicate style inventory

| Inventory | Evidence | R1-A treatment |
| --- | --- | --- |
| Legacy global styles | `XuanYu.Editor.UI/Ui.axaml` and `Design/UiStyles.D4F1.axaml`, `Design/UiStyles.D5.axaml` | Record; do not replace globally |
| Per-view styles | `Foot`, `Left`, `Right`, `Top`, `Workspace`, and state AXAML files | Migrate only with the owning view |
| Hard-coded typography | `FontSize`/`FontWeight` and semantic TextBlock classes across Engine views | Candidate ledger; no bulk rewrite |
| Hard-coded status colors | 112 hex color occurrences in Engine UI AXAML | Candidate ledger; preserve business semantics |
| Unicode/path glyph layer | 51 `Path` and 8 `PathIcon` elements plus `Design/UiTokens.Icons.axaml` | Replace only with matching `XYIcon` contracts |
| Simulated tabs/navigation | `EditorRightTabs`, `TopTabStripTemplate`, `TabControl`, Menu/ToggleButton compositions | Migrate by interaction contract, not visual replacement only |
| Old XYUI implementation | `XYUI/**` 34 tracked files, not referenced by current solution | Keep for history; never consume or copy |

## T6 · Ownership freeze

Agent-B owns and may modify only:

1. `XuanYu.Editor.UI/Foot/NotificationBar.axaml`
2. `XuanYu.Editor.UI/Foot/LogDetailPanel.axaml`
3. `XuanYu.Editor.UI/Right/LayerInspectorPanel.axaml`

Codex must not modify those three AXAML views during R1-A. `XuanYu.Editor.UI/Right/InspectorPanel.axaml` is Batch 2 and remains untouched. Runtime, Gallery, App.axaml, csproj, and global Theme changes are outside Agent-B's page migration scope.

## Audit status

`T1` through `T6`: COMPLETE. No Engine View or XYUI Runtime source was modified by this audit. Final gates: Engine/XYUI Build 0W0E, 2293/2293 tests, ARCH-A and 5+100 PASS, `git diff --check` PASS.
