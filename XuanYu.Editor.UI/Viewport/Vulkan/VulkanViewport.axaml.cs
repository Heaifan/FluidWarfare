using Avalonia.Controls;
using Avalonia.VisualTree;

namespace XuanYu.Editor.UI;

public partial class VulkanViewport : UserControl
{
    public VulkanViewport()
    {
        InitializeComponent();
        this.GetVisualDescendants().OfType<VulkanNativeHost>().Single().RendererReady += (_, _) => HideFallback();
        AttachedToVisualTree += (_, _) => SetFallback("Vulkan 正在初始化...");
    }

    internal void SetFallback(string message)
    {
        FallbackLayer.IsVisible = true;
        FallbackText.Text = message;
    }

    internal void HideFallback() => FallbackLayer.IsVisible = false;
}
