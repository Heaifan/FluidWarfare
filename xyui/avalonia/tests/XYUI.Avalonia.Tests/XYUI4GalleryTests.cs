using XYUI.Avalonia.Gallery;
using XYUI.Avalonia.Gallery.Views;

namespace XYUI.Avalonia.Tests;

[Collection("XyuiHeadless")]
public sealed class XYUI4GalleryTests : IClassFixture<XyuiHeadlessFixture>
{
    readonly XyuiHeadlessFixture _fx;
    public XYUI4GalleryTests(XyuiHeadlessFixture fx) => _fx = fx;

    [Fact]
    public void Gallery_registers_xyui4_components_and_routes_selection() => _fx.Run(() =>
    {
        XyuiBatchTestHost.Prepare();
        var vm = new XYUI1DocumentationViewModel();
        Assert.Equal("2/2", vm.XYUI4CountText);
        Assert.Equal("XYUI-4-4.14", vm.XYUI4Items[0].Id);
        Assert.Equal("XYLoadingIndicator", vm.XYUI4Items[0].CanonicalName);
        Assert.Equal("XYSpinner", vm.XYUI4Items[1].CanonicalName);
        vm.Select("XYUI-4-4.15");
        Assert.Equal("XYUI-4-4.15", vm.SelectedXYUI4Item?.Id);
        Assert.True(vm.IsXYUI4Expanded);
        Assert.IsType<XYUI1ComponentDocumentView>(vm.SelectedDocument);
    });
}
