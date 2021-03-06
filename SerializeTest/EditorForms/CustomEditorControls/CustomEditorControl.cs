﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RTCV.CorruptCore.EventWarlock.WarlockEditor;
using EditorForms.UserControls;
using RTCV.CorruptCore.EventWarlock.WarlockActions;

namespace EditorForms.CustomEditorControls
{
    [WarlockEditorControl("Custom Editor")]
    public partial class CustomEditorControl : WarlockEditControl
    {
        WarlockActionCustomEditor action = null;
        public CustomEditorControl()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            webBrowserRick.Hide();
            webBrowserRick.ProgressChanged += WebBrowserRick_ProgressChanged;
        }

        private async void WebBrowserRick_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            if(e.CurrentProgress >= e.MaximumProgress)
            {
                webBrowserRick.ProgressChanged -= WebBrowserRick_ProgressChanged;
                progressBarRR.Maximum = 100;
                progressBarRR.Value = 80;
                await Task.Delay(3000);
                webBrowserRick.Show();
            }
            else
            {
                progressBarRR.Minimum = 0;
                progressBarRR.Maximum = (int)e.MaximumProgress;
                progressBarRR.Value = Math.Max(0, (int)e.CurrentProgress);
            }
        }

        public override void Apply()
        {
            Clear();
        }

        public override void Clear()
        {
            webBrowserRick.Navigate("https://www.google.com");
            Controls.Remove(webBrowserRick);
        }

        public override void LoadObject(object o, string name = null)
        {
            action = (WarlockActionCustomEditor)o;
        }
    }
}
