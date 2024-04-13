using PR2Macro.Interfaces;

namespace PR2Macro.Services;

public class HotkeyService : IHotkeyService
{
    public HotkeyService()
    {

    }

    public bool Register(Keys key, Form form)
    {
        IntPtr hWnd = form.Handle;
        int id = GetHashCode((int)key, hWnd);

        return NativeMethods.RegisterHotKey(hWnd, id, 0, (int)key);
    }

    public bool Unregister(Keys key, Form form)
    {
        IntPtr hWnd = form.Handle;
        int id = GetHashCode((int)key, hWnd);

        return NativeMethods.UnregisterHotKey(hWnd, id);
    }

    private static int GetHashCode(int key, IntPtr hWnd)
    {
        return key ^ hWnd.ToInt32();
    }
}
