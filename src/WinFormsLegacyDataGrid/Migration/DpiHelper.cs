using System.Drawing;

internal static class DpiHelper
{
    internal static Bitmap GetBitmapFromIcon(Type t, string name)
    {
        using Icon icon = new Icon(t, name);
        return icon.ToBitmap();
    }
}
