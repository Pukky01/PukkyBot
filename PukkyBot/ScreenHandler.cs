using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace PukkyBot
{
    class ScreenHandler
    {
        //local variables
        static Bitmap _bitmap;
        public static Process targetProcess = null;
        public static int width = 800;
        public static int height = 600;
        public static int middleX = 0;
        public static int middleY = 0;

        public static Rectangle window;
        public static int[] green;
        public static int[] blue;
        public static int[] red;

        //DDL Loading
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hwnd, out Rectangle rectangle);
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern uint GetPixel(IntPtr dc, int x, int y);

        public static Process hookProcess(String name)
        {
            if (targetProcess == null)
            {
                Process[] processes = Process.GetProcessesByName(name);

                targetProcess = processes[0];
                GetWindowRect(targetProcess.MainWindowHandle, out window);
            }
            green = new int[width * height];
            red = new int[width * height];
            blue = new int[width * height];

            middleX = 400;
            middleY = 300;

            return targetProcess;
        }

        public static void getScreen()
        {
            try
            {
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

                ProcessUsingLockbitsAndUnsafe(screenshot);
                screenshot.Dispose();
            }
            catch (Exception e)
            {

            }
        }

        static IntPtr dc;
        public static Color GetColorAt(int x, int y)
        {
            int a = (int)GetPixel(dc, x, y);
            return Color.FromArgb(255, (a >> 0) & 0xff, (a >> 8) & 0xff, (a >> 16) & 0xff);
        }

        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
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

                for (int y = 50; y < heightInPixels - 60; y++)
                {
                    byte* currentLine = ptrFirstPixel + (y * bitmapData.Stride);
                    for (int x = bytesPerPixel * 30; x < widthInBytes - bytesPerPixel * 50; x = x + bytesPerPixel)

                    {


                    }
                }
                processedBitmap.UnlockBits(bitmapData);
            }
        }

        public static int getBluePixel(int x, int y)
        {
            return blue[x + y * width];
        }
        public static int getRedPixel(int x, int y)
        {
            return red[x + y * width];
        }
        public static int getGreenPixel(int x, int y)
        {
            return green[x + y * width];
        }

    }
}
