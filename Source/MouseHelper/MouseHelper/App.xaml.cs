using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;
using ManagedWinapi.Windows;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using ManagedWinapi;
using System.Windows;


namespace MouseHelper
{
    /// <summary>
    /// Simple application. Check the XAML for comments.
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private TaskbarIcon notifyIcon;

        private Hotkey hk;
        public Overlay Overlay { get; }
        public new MainWindow MainWindow { get; }
        public AccessHelper Helper { get; set;  }

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
  
        public App()
        {
            Overlay = new Overlay(this);
            MainWindow = new MainWindow(this);
        }

        private void hkHandler(object sender, EventArgs e)
        {
            var win = new SystemWindow(GetForegroundWindow());
            if (win.IsValid())
            { 
                Helper = new AccessHelper(win.HWnd, this);
                Overlay.Show();
                MainWindow.Show();
                MainWindow.Activate();
                MainWindow.Focus();
                MainWindow.txtIn.Focus();
                hk.Enabled = false;
            }

        }

        public void Cleanup()
        {
            Overlay.Visibility=Visibility.Hidden;
            Overlay.ClearCanvas();
            MainWindow.Visibility = Visibility.Hidden;
            try
            {
                Helper.Target.SetFocus();
            }
            catch (Exception)
            {
            }
            
            hk.Enabled = true;
        }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //create the notifyicon (it's a resource declared in NotifyIconResources.xaml
            notifyIcon = (TaskbarIcon) FindResource("NotifyIcon");
            hk = new Hotkey();
            hk.Alt = true;
            hk.Ctrl = true;
            hk.Shift = false;
            hk.WindowsKey = false;
            hk.KeyCode = System.Windows.Forms.Keys.T;

            hk.Enabled = true;
            hk.HotkeyPressed += new System.EventHandler(hkHandler);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            notifyIcon.Dispose(); //the icon would clean up automatically, but this is cleaner
            base.OnExit(e);
        }
    }
}
