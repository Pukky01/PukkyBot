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
        public static Random random = new Random();
       
        
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
            ScreenHandler.hookProcess("Notepad");
        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            ScreenHandler.getScreen();
            MouseSimulator.POINT lpPoint = MouseSimulator.getCurrentPos();
            
            if (isEnabled)
            {
               
                //MouseSimulator.MouseMove(486, 296);
                //Thread.Sleep(1000);
                //MouseSimulator.ClickLeftMouseButton();

            }
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
