using PR2Macro.Models;

namespace PR2Macro.Interfaces;

public interface IMacroService
{
    void MouseMove(int x, int y);
    void LeftClick();
    void Click(IntPtr hWnd, Point point);
    Bitmap CaptureWindow(IntPtr hwnd);
    bool Focus(IntPtr hWnd);
    IEnumerable<bool> Focus(string name);
    IEnumerable<IntPtr> GetWinHandles(string name);
    List<Point> Find(Bitmap haystack, Bitmap needle, MacroInfo macroInfo);
    Point GetClosestPoint(List<Point> points, Point point);
}
