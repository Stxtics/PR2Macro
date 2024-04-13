using PR2Macro.Interfaces;
using PR2Macro.Models;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;

namespace PR2Macro.Services;

public class MacroService : IMacroService
{
    public MacroService()
    {

    }

    public void MouseMove(int x, int y)
    {
        Thread.Sleep(100);
        Cursor.Position = new Point(x, y);
    }

    public void LeftClick()
    {
        NativeMethods.mouse_event(Constants.MOUSEEVENTF_LEFTDOWN, Control.MousePosition.X, Control.MousePosition.Y, 0, IntPtr.Zero);
        NativeMethods.mouse_event(Constants.MOUSEEVENTF_LEFTUP, Control.MousePosition.X, Control.MousePosition.Y, 0, IntPtr.Zero);
    }

    public void Click(IntPtr hWnd, Point point)
    {
        //_ = NativeMethods.SendMessage(hWnd.ToInt32(), Constants.WM_LBUTTONDOWN, 0x00000001, CreateLParam(x, y));
        //_ = NativeMethods.SendMessage(hWnd.ToInt32(), Constants.WM_LBUTTONUP, 0x00000000, CreateLParam(x, y));
        //_ = NativeMethods.PostMessage(hWnd.ToInt32(), Constants.WM_LBUTTONDOWN, 0, MakeLParam(x, y));
        //_ = NativeMethods.PostMessage(hWnd.ToInt32(), Constants.WM_LBUTTONUP, 0x00000000, MakeLParam(x, y));
        var oldPos = Cursor.Position;

        // get screen coordinates
        NativeMethods.ClientToScreen(hWnd, ref point);

        // set cursor on coords, and press mouse
        Cursor.Position = new Point(point.X, point.Y);

        var inputMouseDown = new NativeMethods.INPUT 
        { 
            Type = 0 // input type mouse
        };
        inputMouseDown.Data.Mouse.Flags = 0x0002; // left button down

        var inputMouseUp = new NativeMethods.INPUT 
        { 
            Type = 0 // input type mouse
        };
        inputMouseUp.Data.Mouse.Flags = 0x0004; // left button up

        var inputs = new NativeMethods.INPUT[] { inputMouseDown, inputMouseUp };
        _ = NativeMethods.SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(NativeMethods.INPUT)));

        // return mouse
        Cursor.Position = oldPos;
    }

    public Bitmap CaptureWindow(IntPtr hwnd)
    {
        NativeMethods.GetWindowRect(hwnd, out RECT rc);

        Bitmap bmp = new(rc.Width, rc.Height, PixelFormat.Format32bppArgb);
        Graphics gfxBmp = Graphics.FromImage(bmp);
        IntPtr hdcBitmap = gfxBmp.GetHdc();

        NativeMethods.PrintWindow(hwnd, hdcBitmap, 0);

        gfxBmp.ReleaseHdc(hdcBitmap);
        gfxBmp.Dispose();

        return bmp;
    }

    public bool Focus(IntPtr hWnd)
    {
        _ = NativeMethods.SetForegroundWindow(hWnd);
        if (GetActiveWindowTitle().Contains("4pr2"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public IEnumerable<bool> Focus(string name)
    {
        IEnumerable<IntPtr> hWnds = GetWinHandles(name);
        List<bool> results = new();
        foreach (IntPtr hWnd in hWnds)
        {
            if (hWnd != IntPtr.Zero)
            {
                _ = NativeMethods.SetForegroundWindow(hWnd);
                if (GetActiveWindowTitle().Contains(name))
                {
                    results.Add(true);
                }
                else
                {
                    results.Add(false);
                }
            }
        }
        results.Add(true);
        return results;
    }

    public IEnumerable<IntPtr> GetWinHandles(string name)
    {
        foreach (Process pList in Process.GetProcessesByName(name))
        {
            yield return pList.MainWindowHandle;
        }

        yield return IntPtr.Zero;
    }

    public List<Point> Find(Bitmap haystack, Bitmap needle, MacroInfo macroInfo)
    {
        List<Point> points = new();

        if (haystack == null || needle == null)
        {
            return points;
        }

        if (haystack.Width < needle.Width || haystack.Height < needle.Height)
        {
            return points;
        }

        int[][] haystackArray = GetPixelArray(haystack);
        int[][] needleArray = GetPixelArray(needle);

        foreach (Point firstLineMatchPoint in FindMatch(haystackArray.Take(haystack.Height - needle.Height), needleArray[0]))
        {
            if (IsNeedlePresentAtLocation(haystackArray, needleArray, firstLineMatchPoint, 1))
            {
                Point newPoint = new(firstLineMatchPoint.X + (needle.Width / 2), firstLineMatchPoint.Y + (needle.Height / 2));
                points.Add(newPoint);
            }
        }

        // Needed when mouse clicks were based on monitor
        if (points.Count > 0)
        {
            //adjust points to point on chosen monitor
            for (int i = 0; i < points.Count; i++)
            {
                points[i] = macroInfo.TopLeft.IsEmpty || macroInfo.BottomRight.IsEmpty
                    ? new Point(points[i].X + Screen.AllScreens[macroInfo.Monitor].Bounds.X, points[i].Y + Screen.AllScreens[macroInfo.Monitor].Bounds.Y)
                    : new Point(points[i].X + macroInfo.TopLeft.X, points[i].Y + macroInfo.TopLeft.Y);
            }
        }

        return points;
    }

    public Point GetClosestPoint(List<Point> points, Point point)
    {
        return points.Where(p => p != point).
                       OrderBy(p => Distance(point, p)).
                       Take(1).First();
    }

    #region Private Methods

    private static string GetActiveWindowTitle()
    {
        IntPtr hwnd = NativeMethods.GetForegroundWindow();
        if (hwnd == IntPtr.Zero)
        {
            return "";
        }

        StringBuilder sb = new(700);

        _ = NativeMethods.SendMessage(hwnd, Constants.WM_GETTEXT, sb.Capacity, sb);

        return sb.ToString();
    }

    private static int[][] GetPixelArray(Bitmap bitmap)
    {
        int[][] result = new int[bitmap.Height][];
        BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly,
            PixelFormat.Format32bppArgb);

        for (int y = 0; y < bitmap.Height; ++y)
        {
            result[y] = new int[bitmap.Width];
            Marshal.Copy(bitmapData.Scan0 + (y * bitmapData.Stride), result[y], 0, result[y].Length);
        }

        bitmap.UnlockBits(bitmapData);

        return result;
    }

    private static IEnumerable<Point> FindMatch(IEnumerable<int[]> haystackLines, int[] needleLine)
    {
        int y = 0;
        foreach (int[] haystackLine in haystackLines)
        {
            for (int x = 0, n = haystackLine.Length - needleLine.Length; x < n; ++x)
            {
                if (ContainSameElements(haystackLine, x, needleLine, 0, needleLine.Length))
                {
                    yield return new Point(x, y);
                }
            }
            y += 1;
        }
    }

    private static bool ContainSameElements(int[] first, int firstStart, int[] second, int secondStart, int length)
    {
        for (int i = 0; i < length; ++i)
        {
            if (first[i + firstStart] != second[i + secondStart])
            {
                return false;
            }
        }
        return true;
    }

    private static bool IsNeedlePresentAtLocation(int[][] haystack, int[][] needle, Point point, int alreadyVerified)
    {
        //we already know that "alreadyVerified" lines already match, so skip them
        for (int y = alreadyVerified; y < needle.Length; ++y)
        {
            if (!ContainSameElements(haystack[y + point.Y], point.X, needle[y], 0, needle[y].Length))
            {
                return false;
            }
        }
        return true;
    }

    private static double Distance(Point source, Point target)
    {
        return Math.Pow(target.X - source.X, 2) + Math.Pow(target.Y - source.Y, 2);
    }

    private static IntPtr CreateLParam(int LoWord, int HiWord)
    {
        return (HiWord << 16) | (LoWord & 0xffff);
    }

    private static int MakeLParam(int x, int y) => (y << 16) | (x & 0xFFFF);

    #endregion
}
