﻿using System;
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
        public static Bot activeBod = new AlbionBot();

        public static ComboBox recordedEvents;

        public Form1()
        {
            InitializeComponent();
            recordedEvents = comboBox1;
            ScreenHandler.hookProcess("Notepad");
            MouseSimulator.LoadRecording();
        }

        private void onBotUpdate(object sender, EventArgs e)
        {
            MouseSimulator.POINT lpPoint = MouseSimulator.getCurrentPos();
            label1.Text = "Pos: x:" + lpPoint.X + " y:" + lpPoint.Y;

            if (botController.Enabled)
            {
                //MouseSimulator.MouseMove(486, 296);
                //Thread.Sleep(1000);
                //MouseSimulator.ClickLeftMouseButton();

            }
        }

        private void ScreenScraper_Tick(object sender, EventArgs e)
        {
            if (botController.Enabled)
            {
                ScreenHandler.getScreen();
            }
        }

        private void TestButtonClick(object sender, EventArgs e)
        {
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (botController.Enabled)
            {
                botController.Enabled = false;
                button1.Text = "vypnuto";
            } else
            {
                botController.Enabled = true;
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ReplayButton_Click(object sender, EventArgs e)
        {
            MouseSimulator.replay();
        }

        private void SaveRecordButton_Click(object sender, EventArgs e)
        {
            MouseSimulator.SaveRecording(recordingName.Text);
        }
    }

    
}
