namespace XuanYu.Editor.UI;

public sealed partial class UiVm
{
    public bool CanTransformSelectedEntity =>
        IsEditMode && IsMapWorkspace && TrySelectedEntityKey(out var key) &&
        _sceneState.TryGetEntity(key, out _);

    bool CanSelectTool(object? value)
    {
        if (!CanUseEditTools) return false;
        var tool = EditorToolText.FromText(value?.ToString() ?? string.Empty);
        return tool is not (EditorToolId.Move or EditorToolId.Rotate or EditorToolId.Scale) ||
            CanTransformSelectedEntity;
    }
}
