using XuanYu.Editor.UI;

namespace XuanYu.World.Tests.World;

public sealed partial class TransformFoundationTests
{
    [Fact]
    public void Inspector_name_edit_reuses_entity_rename_service()
    {
        var vm = new UiVm(null, () => true);
        vm.SelectedHierarchyItem = vm.HierarchyItems.Single(i => i.Key == "EntityId(1)");
        vm.InspectorEntityNameText = "Cube_A";

        Assert.True(vm.CommitInspectorEntityName());
        Assert.Equal("Cube_A", SceneOf(vm).RenderSnapshot.Entity.Name);
        Assert.Contains(vm.InspectorFields, f => f.Label == "名称" && f.Value == "Cube_A");
    }

    [Theory]
    [InlineData("位置", "X", "12.5")]
    [InlineData("位置", "X", "-3.25")]
    [InlineData("位置", "Z", "0.125")]
    [InlineData("旋转", "Y", "1234.5")]
    [InlineData("缩放", "Z", "1.5")]
    public void Inspector_transform_edit_accepts_real_numeric_values(string group, string axis, string text)
    {
        var vm = new UiVm(null, () => true);
        vm.SelectedHierarchyItem = vm.HierarchyItems.Single(i => i.Key == "EntityId(1)");

        Assert.True(vm.TryCommitInspectorTransformValue(group, axis, text));
        var transform = SceneOf(vm).RenderSnapshot.Entity.Transform;
        var value = group switch
        {
            "位置" => axis switch { "X" => transform.Position.X, "Y" => transform.Position.Y, _ => transform.Position.Z },
            "旋转" => axis switch { "X" => transform.Rotation.X, "Y" => transform.Rotation.Y, _ => transform.Rotation.Z },
            _ => axis switch { "X" => transform.Scale.X, "Y" => transform.Scale.Y, _ => transform.Scale.Z }
        };
        Assert.Equal(double.Parse(text, System.Globalization.CultureInfo.InvariantCulture), value);
    }

    [Fact]
    public void Inspector_transform_edit_reuses_history_for_undo_and_redo()
    {
        var vm = new UiVm(null, () => true);
        vm.SelectedHierarchyItem = vm.HierarchyItems.Single(i => i.Key == "EntityId(1)");

        Assert.True(vm.TryCommitInspectorTransformValue("位置", "X", "5"));
        vm.TryUndoFromShortcut();
        Assert.Equal(0, SceneOf(vm).RenderSnapshot.Entity.Transform.Position.X);
        vm.TryRedoFromShortcut();
        Assert.Equal(5, SceneOf(vm).RenderSnapshot.Entity.Transform.Position.X);
    }

    [Fact]
    public void Inspector_input_formats_visible_transform_without_losing_precision()
    {
        var vm = new UiVm(null, () => true);
        vm.SelectedHierarchyItem = vm.HierarchyItems.Single(i => i.Key == "EntityId(1)");

        Assert.True(vm.TryCommitInspectorTransformValue("位置", "X", "3.114397031608849"));
        Assert.True(vm.TryCommitInspectorTransformValue("旋转", "Z", "90"));

        Assert.Contains(vm.InspectorFields, f => f.Label == "位置" && f.Value == "X 3.114397    Y 0    Z 0");
        Assert.Contains(vm.InspectorFields, f => f.Label == "旋转" && f.Value == "X 0°    Y 0°    Z 90°");
        Assert.Equal(3.114397031608849, SceneOf(vm).RenderSnapshot.Entity.Transform.Position.X);
    }

    [Theory]
    [InlineData("缩放", "X", "0")]
    [InlineData("缩放", "X", "-1")]
    [InlineData("旋转", "Z", "NaN")]
    [InlineData("位置", "X", "abc")]
    public void Inspector_invalid_input_does_not_pollute_transform_or_history(
        string group,
        string axis,
        string text)
    {
        var vm = new UiVm(null, () => true);
        vm.SelectedHierarchyItem = vm.HierarchyItems.Single(i => i.Key == "EntityId(1)");
        var before = SceneOf(vm).RenderSnapshot.Entity.Transform;

        Assert.False(vm.TryCommitInspectorTransformValue(group, axis, text));

        Assert.Equal(before, SceneOf(vm).RenderSnapshot.Entity.Transform);
        Assert.Equal(0, vm.TransformHistoryCount);
    }
}
