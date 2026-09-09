using Avalonia.Controls;
using Avalonia.Interactivity;
using XYUI.Avalonia.Controls;

namespace XuanYu.Editor.UI;

public partial class Left : UserControl
{
    public Left()
    {
        InitializeComponent();
        SelectContentTab("project");
    }

    void ProjectToggle_Click(object? sender, RoutedEventArgs e) => SelectContentTab("project");

    void FileToggle_Click(object? sender, RoutedEventArgs e) => SelectContentTab("file");

    void SelectContentTab(string id)
    {
        var project = id != "file";
        ProjectToggle.IsChecked = project;
        FileToggle.IsChecked = !project;
        ProjectWorkspace.IsVisible = project;
        FileWorkspace.IsVisible = !project;
    }
}
