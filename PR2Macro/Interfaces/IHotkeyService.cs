namespace PR2Macro.Interfaces
{
    public interface IHotkeyService
    {
        bool Register(Keys key, Form form);
        bool Unregister(Keys key, Form form);
    }
}
