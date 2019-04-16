using System;
using System.Drawing;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;

namespace PukkyBot
{
    public class MouseSimulator
    {
        //Variables for low level global hook
        static int hHook = 0;
        public static Boolean isRecording = false;
        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);
        static HookProc MouseHookProcedure;
        private static Random random = new Random();
        public static List<MouseClickEvent> recordedEvents = new List<MouseClickEvent>();
        private static Stopwatch timeKeeper = new Stopwatch();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        [DllImport("user32.dll", CharSet = CharSet.Auto,
        CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode,
        IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto,
        CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        private const int BM_CLICK = 0x00F5;

        public struct MouseClickEvent
        {
            public int x;
            public int y;
            public float dTime;
            public MouseClickEvent(int x, int y, float time)
            {
                this.x = x; this.y = y; this.dTime = time;
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        struct INPUT
        {
            public SendInputEventType type;
            public MouseKeybdhardwareInputUnion mkhi;
        }
        [StructLayout(LayoutKind.Explicit)]
        struct MouseKeybdhardwareInputUnion
        {
            [FieldOffset(0)]
            public MouseInputData mi;

            [FieldOffset(0)]
            public KEYBDINPUT ki;

            [FieldOffset(0)]
            public HARDWAREINPUT hi;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct HARDWAREINPUT
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        }
        struct MouseInputData
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public MouseEventFlags dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        [Flags]
        enum MouseEventFlags : uint
        {
            MOUSEEVENTF_MOVE = 0x0001,
            MOUSEEVENTF_LEFTDOWN = 0x0002,
            MOUSEEVENTF_LEFTUP = 0x0004,
            MOUSEEVENTF_RIGHTDOWN = 0x0008,
            MOUSEEVENTF_RIGHTUP = 0x0010,
            MOUSEEVENTF_MIDDLEDOWN = 0x0020,
            MOUSEEVENTF_MIDDLEUP = 0x0040,
            MOUSEEVENTF_XDOWN = 0x0080,
            MOUSEEVENTF_XUP = 0x0100,
            MOUSEEVENTF_WHEEL = 0x0800,
            MOUSEEVENTF_VIRTUALDESK = 0x4000,
            MOUSEEVENTF_ABSOLUTE = 0x8000
        }
        enum SendInputEventType : int
        {
            InputMouse,
            InputKeyboard,
            InputHardware
        }

        public static void LeftClick(int x, int y)
        {
            x += ScreenHandler.window.Left + random.Next(0, 2) * 2 - 1;
            y += ScreenHandler.window.Top + random.Next(0, 2) * 2 - 1;

            LinearSmoothMove(new Point(x, y), new TimeSpan(0, 0, 0, 1));

            Cursor.Position = new System.Drawing.Point(x, y);
            //  ClickOnPoint(ragnarok.MainWindowHandle, new Point(x, y));
            mouse_event((int)(MouseEventFlags.MOUSEEVENTF_LEFTDOWN), 0, 0, 0, 0);
            Thread.Sleep(25);
            mouse_event((int)(MouseEventFlags.MOUSEEVENTF_LEFTUP), 0, 0, 0, 0);

        }
        public static void LinearSmoothMove(Point newPosition, TimeSpan duration)
        {
            POINT start;
            GetCursorPos(out start);

            // Find the vector between start and newPosition
            double deltaX = newPosition.X - start.X;
            double deltaY = newPosition.Y - start.Y;

            // start a timer
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            double timeFraction = 0.0;

            do
            {
                timeFraction = (double)stopwatch.Elapsed.Ticks / duration.Ticks;
                if (timeFraction > 1.0)
                    timeFraction = 1.0;

                PointF curPoint = new PointF((float)(start.X + timeFraction * deltaX),
                                             (float)(start.Y + timeFraction * deltaY));
                Cursor.Position = Point.Round(curPoint);
                Thread.Sleep(1);
            } while (timeFraction < 1.0);
            Thread.Sleep(200);
        }

        public static void startMouseRecording()
        {
            timeKeeper.Reset();
            
            recordedEvents.Clear();
            isRecording = true;
            MouseHookProcedure = new HookProc(MouseHookProc);
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
                hHook = SetWindowsHookEx(WH_MOUSE_LL,
                             MouseHookProcedure,
                             GetModuleHandle(curModule.ModuleName), 0);
        }

        public static void stopMouseRecording()
        {
            timeKeeper.Stop();
            isRecording = false;
            bool ret = UnhookWindowsHookEx(hHook);
            hHook = 0;
        }

        private const int WH_MOUSE_LL = 14;
        public const int WH_MOUSE = 7;
        [StructLayout(LayoutKind.Sequential)]
        public class MouseHookStruct
        {
            public POINT pt;
            public int hwnd;
            public int wHitTestCode;
            public int dwExtraInfo;
        }
        enum MouseMessages
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }

        public static int MouseHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            MouseHookStruct MyMouseHookStruct = (MouseHookStruct)
                Marshal.PtrToStructure(lParam, typeof(MouseHookStruct));
            
            if (nCode >= 0 && MouseMessages.WM_LBUTTONDOWN == (MouseMessages)wParam)
            {
                if (!timeKeeper.IsRunning)
                    timeKeeper.Start();

                POINT p = getCurrentPos();
                float dtime = timeKeeper.ElapsedMilliseconds - recordedEvents.Count > 0 ? recordedEvents[recordedEvents.Count-1].dTime : 0;
                recordedEvents.Add(new MouseClickEvent(p.X, p.Y, dtime));
            }
            return CallNextHookEx(hHook, nCode, wParam, lParam);
        }

        public static POINT getCurrentPos()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);
            lpPoint.X -= ScreenHandler.window.Left;
            lpPoint.Y -= ScreenHandler.window.Top;
            return lpPoint;
        }
    }
}
