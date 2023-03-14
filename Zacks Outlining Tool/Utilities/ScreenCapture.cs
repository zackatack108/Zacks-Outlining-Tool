using System;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Zacks_Outlining_Tool.Utilities;

public class ScreenCapture
{
    public Image CaptureWindow(IntPtr handle)
    {
        IntPtr hdcSrc = User32.GetWindowDC(handle);

        User32.RECT windowRect = new User32.RECT();
        User32.GetWindowRect(handle, ref windowRect);

        int width = 400;
        int height = 200;

        IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);

        IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);

        IntPtr hOld = GDI32.SelectObject(hdcDest, hBitmap);

        GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, GDI32.SRCCOPY);
        GDI32.SelectObject(hdcDest, hOld);
        GDI32.DeleteDC(hdcDest);
        User32.ReleaseDC(handle, hdcSrc);

        Image img = Image.FromHbitmap(hBitmap);

        GDI32.DeleteObject(hBitmap);
        return img;
    }
}

class GDI32
{

    public const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter
    [DllImport("gdi32.dll")]
    public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest,
        int nWidth, int nHeight, IntPtr hObjectSource,
        int nXSrc, int nYSrc, int dwRop);
    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth,
        int nHeight);
    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
    [DllImport("gdi32.dll")]
    public static extern bool DeleteDC(IntPtr hDC);
    [DllImport("gdi32.dll")]
    public static extern bool DeleteObject(IntPtr hObject);
    [DllImport("gdi32.dll")]
    public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
}
}
