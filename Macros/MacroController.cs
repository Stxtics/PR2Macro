using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Forms;

namespace PR2Macro
{
    public class MacroController
    {
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;

        public static void MouseMove(int x, int y)
        {
            Thread.Sleep(100);
            Cursor.Position = new Point(x, y);
        }
        public static void LeftClick()
        {
            NativeMethods.mouse_event(MOUSEEVENTF_LEFTDOWN, Control.MousePosition.X, Control.MousePosition.Y, 0, IntPtr.Zero);
            NativeMethods.mouse_event(MOUSEEVENTF_LEFTUP, Control.MousePosition.X, Control.MousePosition.Y, 0, IntPtr.Zero);
        }

        public static void LeftDown()
        {
            NativeMethods.mouse_event(MOUSEEVENTF_LEFTDOWN, Control.MousePosition.X, Control.MousePosition.Y, 0, IntPtr.Zero);
        }

        public static void LeftUp()
        {
            NativeMethods.mouse_event(MOUSEEVENTF_LEFTUP, Control.MousePosition.X, Control.MousePosition.Y, 0, IntPtr.Zero);
        }

        public static bool Focus(string name)
        {
            IntPtr hWnd;
            foreach (Process proc in Process.GetProcesses().Where(x => x.ProcessName.Equals(name)).Where(x => x.MainWindowHandle != IntPtr.Zero))
            {
                hWnd = proc.MainWindowHandle;
                NativeMethods.ShowWindow(hWnd, 3);
                NativeMethods.SetForegroundWindow(hWnd);
                AutomationElement root = AutomationElement.FromHandle(hWnd);
                if (root.Current.Name.Contains("4p Sim"))
                {
                    return true;
                }
                Condition condition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem);
                AutomationElementCollection tabs = root.FindAll(TreeScope.Descendants, condition);
                int i = 0;
                foreach (AutomationElement tab in tabs)
                {
                    i++;
                    if (tab.Current.Name.Contains("4p Sim"))
                    {
                        break;
                    }
                }
                if (i != 0)
                {
                    SendKeys.Send("^" + i.ToString());
                    root = AutomationElement.FromHandle(hWnd);
                    if (root.Current.Name.Contains("4p Sim"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static void WaitPixelColor(int x, int y, Color color)
        {
            while (true)
            {
                Color c = GetColorAt(x, y);

                if (c.R == color.R && c.G == color.G && c.B == color.B)
                {
                    return;
                }

                Thread.Sleep(50);
            }
        }

        private static readonly Bitmap bmp = new Bitmap(1, 1);
        private static Color GetColorAt(int x, int y)
        {
            Rectangle bounds = new Rectangle(x, y, 1, 1);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);
            }

            return bmp.GetPixel(0, 0);
        }

        public static List<Point> Find(Bitmap haystack, Bitmap needle)
        {
            if (null == haystack || null == needle)
            {
                return null;
            }
            if (haystack.Width < needle.Width || haystack.Height < needle.Height)
            {
                return null;
            }

            List<Point> points = new List<Point>();

            int[][] haystackArray = GetPixelArray(haystack);
            int[][] needleArray = GetPixelArray(needle);

            foreach (Point firstLineMatchPoint in FindMatch(haystackArray.Take(haystack.Height - needle.Height), needleArray[0]))
            {
                if (IsNeedlePresentAtLocation(haystackArray, needleArray, firstLineMatchPoint, 1))
                {
                    Point newPoint = new Point(firstLineMatchPoint.X + needle.Width / 2, firstLineMatchPoint.Y + needle.Height / 2);
                    points.Add(newPoint);
                }
            }

            return points;
        }

        private static int[][] GetPixelArray(Bitmap bitmap)
        {
            int[][] result = new int[bitmap.Height][];
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            for (int y = 0; y < bitmap.Height; ++y)
            {
                result[y] = new int[bitmap.Width];
                Marshal.Copy(bitmapData.Scan0 + y * bitmapData.Stride, result[y], 0, result[y].Length);
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

        public static Point GetClosestPoint(List<Point> points, Point point)
        {
            return points.Where(p => p != point).
                           OrderBy(p => Distance(point, p)).
                           Take(1).First();
        }
        private static double Distance(Point source, Point target)
        {
            return Math.Pow(target.X - source.X, 2) + Math.Pow(target.Y - source.Y, 2);
        }
    }
}
