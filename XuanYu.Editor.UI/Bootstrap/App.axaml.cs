using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using XuanYu.Render.Abstractions;
using XYUI.Avalonia.Controls;
using XYUI.Avalonia.Interaction;
using XYUI.Avalonia.Spatial;
using XYUI.Avalonia.Theme;
using XYUI.Avalonia.Typography;
using XYUI.Avalonia.Vector;

namespace XuanYu.Editor.UI;

public sealed class App : Application
{
    readonly INativeHostSurfaceBridgeFactory? _surfaceBridgeFactory;

    public App() { }

    public App(INativeHostSurfaceBridgeFactory surfaceBridgeFactory) =>
        _surfaceBridgeFactory = surfaceBridgeFactory;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        Resources.MergedDictionaries.Add(XyuiTheme.CreateThemeDictionaries());
        Resources.MergedDictionaries.Add(XyuiVectorIcons.CreateResources());
        Styles.Add(XyuiTextStyles.Create());
        Styles.Add(XyuiShapeStyles.Create());
        Styles.Add(XyuiInteractionStyles.Create());
        Styles.Add(XyuiControlStyles.Create());
        Styles.Add(XyuiComponentStyles.Create());
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.ShutdownMode = ShutdownMode.OnMainWindowClose;
            var window = new UiWin();
            var vm = new UiVm(_surfaceBridgeFactory, seedInitialScene: false, dialogService: window);
            window.DataContext = vm;
            desktop.MainWindow = window;
        }

        base.OnFrameworkInitializationCompleted();
    }
}
