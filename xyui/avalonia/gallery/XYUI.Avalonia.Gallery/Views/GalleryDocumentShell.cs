using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace XYUI.Avalonia.Gallery.Views;

public class GalleryDocumentShell : ScrollViewer
{
    public GalleryDocumentShell()
    {
        HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
    }

    public void ScrollTo(double y)
    {
        Offset = new global::Avalonia.Vector(0, y);
    }
}

public class UniversalDocumentShell : GalleryDocumentShell {}
