# XYUI-3 Round 2 Runtime Audit

日期：2026-09-06

## Scope

Codex Runtime / Rules / Architecture ownership for 3.07～3.12. Round 1 3.01～3.06 remains frozen.

## Matrix

| Control | Existing | Patch | Runtime evidence |
|---|---|---|---|
| 3.07 NavigationRail | Sidebar projection, shared navigation state, flyout | XAML collections, disabled, keyboard, accessible label, 64 DIP token | targeted tests |
| 3.08 Tabs | XYTab / XYTabs selection and close | Icon, disabled, stable modified slot, declaration collection | targeted tests |
| 3.09 TabBar | XYTabs viewport and actions | declaration collection, SelectedTabId, keyboard contract | single-accent and action tests |
| 3.10 DockTabs | XYTab visual, close and drag shell | ActiveTabId, default/XAML collection, handoff event | selection/close/reorder tests |
| 3.11 Breadcrumb | current/collapsed/dropdown path | default/items contract and NavigationRequested | current guard/dropdown tests |
| 3.12 TreeNavigation | flat compact tree interaction | nested Children, disabled, Badge/Status, derived hierarchy | expand/select/keyboard tests |

## Verification

- Runtime project build: 0 warning / 0 error.
- Round 2 targeted suite: 34/34 passed, 0 failed, 0 skipped.
- Full builds: root solution and `XYUI.Avalonia.slnx`, both 0 warning / 0 error.
- Full tests: XYUI 520/520, Core 339/339, WarCore 22/22, World 1286/1286; total 2167/2167 passed, 0 failed, 0 skipped.
- ARCH-A including 5+100: PASS; `git diff --check`: PASS.
- 3.12 hotfix audit: nested `Children` is a real `[Content]` collection; parent navigation is derived by Children traversal; nested `Depth` is assigned by `Flatten`; `HasChildren` follows `Children.Count` for canonical consumers; `IsEnabled`, Badge and Status reach the real node visual; no Runtime patch was required.
- 3.12 Gallery handoff: Quick Start and Live Example use nested `Children`, remove authored `Depth`/`HasChildren`, and show Disabled/Badge/Status through Runtime.
- 3.12 impacted targeted tests: 29/29 passed; XYUI full suite: 520/520 passed.
- User visual review remains required; vertical alignment and final density are not automated acceptance. Status: `TECHNICAL PASS · READY FOR USER VISUAL REVIEW`.
