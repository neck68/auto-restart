using System.Runtime.InteropServices;

namespace AutoRestarter.Util;

public static class ConsoleHelper
{
    #region DLLImports
    #pragma warning disable CA1401
    #pragma warning disable SYSLIB1054
    [DllImport("kernel32.dll")]
    public static extern bool AllocConsole();

    [DllImport("kernel32.dll")]
    public static extern nint GetConsoleWindow();

    [DllImport("user32.dll")]
    public static extern bool ShowWindow(nint hWnd, int nCmdShow);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool SetConsoleTitle(string lpConsoleTitle);
    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool SetWindowPos(nint hWnd, nint hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool GetWindowRect(nint hWnd, out RECT lpRect);
#pragma warning restore SYSLIB1054
#pragma warning restore CA1401
    #endregion

    private const int SW_HIDE = 0;
    private const int SW_SHOW = 5;
    private const uint SWP_NOSIZE = 0x0001;
    private const uint SWP_NOMOVE = 0x0002;
    private const uint SWP_NOZORDER = 0x0004;
    private const uint SWP_SHOWWINDOW = 0x0040;

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
    public static void ShowConsoleWindow()
    {
        nint hWndConsole = GetConsoleWindow();
        if (hWndConsole != nint.Zero)
        {
            ShowWindow(hWndConsole, SW_SHOW);
        }
    }

    public static void HideConsoleWindow()
    {
        nint hWndConsole = GetConsoleWindow();
        if (hWndConsole != nint.Zero)
        {
            ShowWindow(hWndConsole, SW_HIDE);
        }
    }

    public static void SetConsoleWindowTitle(string title)
    {
        SetConsoleTitle(title);
    }

    public static void SetConsoleWindowPositionAndSize(Form mainForm, int width, int height)
    {
        nint hWndConsole = GetConsoleWindow();
        if (hWndConsole != nint.Zero)
        {
            // Get the position of the main form
            Rectangle mainFormRect = mainForm.Bounds;

            // Set console window size and position behind the main form
            SetWindowPos(hWndConsole, 0, mainFormRect.Left, mainFormRect.Top, width, height, SWP_SHOWWINDOW);
        }
    }

    public static Rectangle GetConsoleWindowRect()
    {
        nint hWndConsole = GetConsoleWindow();
        if (hWndConsole != nint.Zero && GetWindowRect(hWndConsole, out RECT rect))
        {
            return new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
        }
        return Rectangle.Empty;
    }
}
