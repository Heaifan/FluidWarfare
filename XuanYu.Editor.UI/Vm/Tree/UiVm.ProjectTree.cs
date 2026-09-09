namespace XuanYu.Editor.UI;

public sealed partial class UiVm
{
    readonly EditorTreeNode _currentSceneNode =
        new("scene:current", "未命名场景", "场景", "未命名场景", 0, "project");

    IReadOnlyList<EditorTreeNode> BuildProjectItems()
    {
        var title = DocumentFileName;
        var path = string.IsNullOrWhiteSpace(CurrentScenePath) ? title : CurrentScenePath;
        _currentSceneNode.Update(title, "场景", path, 0, "project");
        _currentSceneNode.SetTreeState(false, false);
        TreeGuideBuilder.Apply([_currentSceneNode]);
        return [_currentSceneNode];
    }

    IReadOnlyList<InspectorFieldRow> BuildProjectInspectorFields()
    {
        if (!_editorState.Snapshot.HasSelection || _editorState.Snapshot.SelectionKey != _currentSceneNode.Key)
            return [];
        var title = DocumentFileName;
        var path = string.IsNullOrWhiteSpace(CurrentScenePath) ? title : CurrentScenePath;
        return [new("名称", title), new("类型", "场景"), new("路径", path)];
    }
}
