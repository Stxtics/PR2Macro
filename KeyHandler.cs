using System;
using System.Windows.Forms;

namespace PR2Macro
{
    public class KeyHandler
    {
        private readonly int key;
        private readonly IntPtr hWnd;
        private readonly int id;

        public KeyHandler(Keys key, Form form)
        {
            this.key = (int)key;
            hWnd = form.Handle;
            id = GetHashCode();
        }

        public override int GetHashCode()
        {
            return key ^ hWnd.ToInt32();
        }

        public bool Register()
        {
            return NativeMethods.RegisterHotKey(hWnd, id, 0, key);
        }

        public bool Unregiser()
        {
            return NativeMethods.UnregisterHotKey(hWnd, id);
        }
    }
}
