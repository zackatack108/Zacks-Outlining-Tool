using System;
using System.Runtime.InteropServices;

namespace Zacks_Outlining_Tool.Utilities;

public class User32
{

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    [DllImport("User32.dll", SetLastError = true)]
    public static extern IntPtr FindWindow(String IpClassName, String IpWindowName);
    [DllImport("User32.dll", SetLastError = true)]
    public static extern IntPtr SetForegroundWindow(IntPtr hWnd);

    [DllImport("User32.dll", SetLastError = true)]
    public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);

    [DllImport("Users32.dll", SetLastError = true)]
    public static extern IntPtr GetFocus();

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr GetWindowDC(IntPtr hWnd);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern int SendMessage(IntPtr hWnd, int msg, int Param, string text);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
}
