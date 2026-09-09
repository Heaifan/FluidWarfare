using XuanYu.Editor.UI;

namespace XuanYu.World.Tests.World;

public sealed class UiTreeGuideTests
{
    [Fact]
    public void Project_tree_contains_only_the_current_document()
    {
        var vm = new UiVm(null, () => true, seedInitialScene: false);
        var node = Assert.Single(vm.ProjectItems);

        Assert.Equal("scene:current", node.Key);
        Assert.Equal("未命名场景", node.Title);
        Assert.False(node.CanToggle);
        Assert.DoesNotContain(vm.ProjectItems, item => item.Title.Contains("测试"));
    }

    [Fact]
    public void Project_tree_projects_real_scene_objects_under_current_document()
    {
        var vm = new UiVm(null, () => true, seedInitialScene: false);

        vm.RunCommand.Execute("添加立方体");

        var root = Assert.Single(vm.ProjectItems, item => item.Key == "scene:current");
        var cube = Assert.Single(vm.ProjectItems, item => item.IsEntity);
        Assert.True(root.CanToggle);
        Assert.Equal("立方体", cube.Title);
        Assert.Equal(1, cube.Level);
    }

    [Fact]
    public void Project_tree_selection_exposes_current_document_fields()
    {
        var vm = new UiVm(null, () => true, seedInitialScene: false);
        vm.SelectedProjectItem = vm.ProjectItems.Single();

        Assert.Contains(vm.InspectorFields, field => field.Label == "名称" && field.Value == "未命名场景");
        Assert.Contains(vm.InspectorFields, field => field.Label == "类型" && field.Value == "场景");
    }
}
