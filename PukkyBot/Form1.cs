using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows;
using System.Threading;
using WindowsInput;
using static PukkyBot.Form1;

namespace PukkyBot
{
    public partial class Form1 : Form
    {
        public static Process ragnarok = null;
        public static int width = 800;
        public static int height = 600;
        public static int middleX = 0;
        public static int middleY = 0;

        public static Rectangle window;
        public static int[] green;
        public static int[] blue;
        public static int[] red;

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern uint GetPixel(IntPtr dc, int x, int y);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hwnd, out Rectangle rectangle);
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        private const int BM_CLICK = 0x00F5;
        [DllImport("user32.dll")]
        static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);
        [DllImport("user32.dll")]
        internal static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs, int cbSize);

        //Mouse actions
        private const UInt32 MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const UInt32 MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        public static bool isItem = false;
        public static int[] itemPos = new int[2];
        public static double itemdist = 0;

        public static bool isMob = false;
        public static int[] MobPos = new int[2];
        public static double mobdist = 0;

        public static bool isDanger = false;
        public static bool isEnabled = true;
        public static int tickNotFound = 0;

        public Form1()
        {
            InitializeComponent();
            getRagnarok();
        }

        public static Process getRagnarok()
        {
            if (ragnarok == null)
            {
                Process[] processes = Process.GetProcessesByName("Notepad");

                ragnarok = processes[0];
                GetWindowRect(ragnarok.MainWindowHandle, out window);
            }
            green = new int[width*height];
            red = new int[width * height];
            blue = new int[width * height];

            middleX = 400;
            middleY = 300;

            return ragnarok;
        }

        public static Color GetColorAt(int x, int y)
        {
            
            int a = (int)GetPixel(dc, x, y);
            
            return Color.FromArgb(255, (a >> 0) & 0xff, (a >> 8) & 0xff, (a >> 16) & 0xff);
        }
        static IntPtr dc;

        private void timer1_Tick(object sender, EventArgs e)
        {
            getScreen();
            POINT lpPoint;
            GetCursorPos(out lpPoint);
            lpPoint.X = lpPoint.X - window.Left;
            lpPoint.Y = lpPoint.Y - window.Top;
            if (lpPoint.X > 0 && lpPoint.X < width && lpPoint.Y > 0 && lpPoint.Y < height)
                label3.Text = lpPoint.X+" "+ lpPoint.Y + " color: " + getRedPixel(lpPoint.X, lpPoint.Y) + " " + getGreenPixel(lpPoint.X, lpPoint.Y) + " " + getBluePixel(lpPoint.X, lpPoint.Y)+ " "+distance(lpPoint.X, lpPoint.Y);

            if (isEnabled)
            {
                if (lpPoint.X > 0 && lpPoint.X < width && lpPoint.Y > 0 && lpPoint.Y < height)
                {
                    label2.Text = lpPoint.X + " " + lpPoint.Y;
                }
                //MouseSimulator.MouseMove(486, 296);
                //Thread.Sleep(1000);
                //MouseSimulator.ClickLeftMouseButton();

            }
        }

        
        static private void ProcessUsingLockbitsAndUnsafe(Bitmap processedBitmap)
        {
          
            unsafe
            {
                BitmapData bitmapData = processedBitmap.LockBits(new Rectangle(0, 0, processedBitmap.Width, processedBitmap.Height), ImageLockMode.ReadWrite, processedBitmap.PixelFormat);
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(processedBitmap.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* ptrFirstPixel = (byte*)bitmapData.Scan0;

                for (int y = 50; y < heightInPixels-60; y++)
                {
                    byte* currentLine = ptrFirstPixel + (y * bitmapData.Stride);
                    for (int x = bytesPerPixel*30; x < widthInBytes- bytesPerPixel*50; x = x + bytesPerPixel)
                      
                    {
                      

                    }
                }
                processedBitmap.UnlockBits(bitmapData);
            }
        }

        public static double distance(int x, int y)
        {
            return Math.Sqrt((x - middleX) * (x - middleX) + (y - middleY) * (y - middleY));
        }

        static Bitmap _bitmap;
        public static void getScreen()
        {
            try {
                Bitmap screenshot = new Bitmap(width,
                                   height,
                                   PixelFormat.Format32bppArgb);

                Graphics screenGraph = Graphics.FromImage(screenshot);

                screenGraph.CopyFromScreen(window.Left,
                                           window.Top,
                                           0,
                                           0,
                                           new Size(width, height),
                                           CopyPixelOperation.SourceCopy);

                //     _bitmap = new Bitmap(width, height);
                //     using (Graphics g = Graphics.FromImage(_bitmap))
                //          g.CopyFromScreen(window.Left, window.Top, window.Left+ width, window.Top+ height, new Size(width, height));

                ProcessUsingLockbitsAndUnsafe(screenshot);
                screenshot.Dispose();
                //    this.BackgroundImage = (Image)_bitmap;
            } catch(Exception e)
            {

            }
        }

        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
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


        [Flags]
        public enum MouseEventFlags
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }


#pragma warning disable 649
    internal struct INPUT
    {
        public UInt32 Type;
        public MOUSEKEYBDHARDWAREINPUT Data;
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct MOUSEKEYBDHARDWAREINPUT
    {
        [FieldOffset(0)]
        public MOUSEINPUT Mouse;
    }

    internal struct MOUSEINPUT
    {
        public Int32 X;
        public Int32 Y;
        public UInt32 MouseData;
        public UInt32 Flags;
        public UInt32 Time;
        public IntPtr ExtraInfo;
    }

#pragma warning restore 649


        private void button2_Click(object sender, EventArgs e)
        {
            LeftClick(300, 300);
        }
        public static void ClickOnPoint(IntPtr wndHandle, Point clientPoint)
        {
            var oldPos = Cursor.Position;

            /// get screen coordinates
            ClientToScreen(wndHandle, ref clientPoint);

            /// set cursor on coords, and press mouse
            Cursor.Position = new Point(clientPoint.X, clientPoint.Y);

            var inputMouseDown = new INPUT();
            inputMouseDown.Type = 0; /// input type mouse
            inputMouseDown.Data.Mouse.Flags = 0x0002; /// left button down

            var inputMouseUp = new INPUT();
            inputMouseUp.Type = 0; /// input type mouse
            inputMouseUp.Data.Mouse.Flags = 0x0004; /// left button up

            var inputs = new INPUT[] { inputMouseDown, inputMouseUp };
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Enabled = false;
                button1.Text = "vypnuto";
            } else
            {
                timer1.Enabled = true;
                button1.Text = "zapnuto";
            }
        }

        public static Random random = new Random();

        public static void LeftClick(int x, int y)
        {
            x += window.Left + random.Next(0, 2) * 2 - 1;
            y += window.Top + random.Next(0, 2) * 2 - 1;

            LinearSmoothMove(new Point(x, y), new TimeSpan(0, 0, 0, 1));

            Cursor.Position = new System.Drawing.Point(x, y);
            //  ClickOnPoint(ragnarok.MainWindowHandle, new Point(x, y));
            mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
            Thread.Sleep(25);
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);

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

        private void Button2_Click_1(object sender, EventArgs e)
        {
            if (MouseSimulator.isRecording)
            {
                button2.Text = "Start recording";
                MouseSimulator.stopMouseRecording();
            }
            else
            {
                MouseSimulator.startMouseRecording();
                button2.Text = "Stop recording";
            }
        }

       
    }

    
}
