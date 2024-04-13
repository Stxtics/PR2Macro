namespace PR2Macro;

public static class Constants
{
    public const int NOMOD = 0x0000;
    public const int ALT = 0x0001;
    public const int CTRL = 0x0002;
    public const int SHIFT = 0x0004;
    public const int WIN = 0x0008;

    //windows message id for hotkey
    public const int WM_HOTKEY_MSG_ID = 0x0312;

    public const int WM_GETTEXT = 0x000D;

    public const int MOUSEEVENTF_LEFTDOWN = 0x0002;
    public const int MOUSEEVENTF_LEFTUP = 0x0004;

    public const int WM_MOUSEMOVE = 0x0200;

    public const int WM_LBUTTONDOWN = 0x0201;
    public const int WM_LBUTTONUP = 0x0202;

    public const double BigPR2Width = 550.0;
    public const double BigPR2Height = 400.0;
    public const double MediumPR2Width = 352.0;
    public const double MediumPR2Height = 256.0;
    public const double SmallPR2Width = 137.0;
    public const double SmallPR2Height = 100.0;
}
