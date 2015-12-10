using System;
using System.Windows;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ChipSplit
{
	public partial class Window_Splitscreen : Window
	{
		private System.Windows.Forms.Panel _panel1;
		private Process _process1;

		/*
		* ADDED SHIT
		*/
		private System.Windows.Forms.Panel _panel2;
		private Process _process2;
		/*
		*
		*/

		public Window_Splitscreen()
		{
			InitializeComponent();

			window_Splitscreen.WindowState = WindowState.Maximized; // removes the taskbar
            window_Splitscreen.BringIntoView();
			//window_Splitscreen.Topmost = true;

			// (FIRST WINDOW)
			//Window_Splitscreen.SetWindowPos(Window_Splitscreen. 0, 0);
			windowsFormsHost1.Visibility = Visibility.Hidden; // hide the windows form before the user loads in the application so buttons can be displayed.
			_panel1 = new System.Windows.Forms.Panel();
			windowsFormsHost1.Child = _panel1;

			/*
			* ADDED SHIT (SECOND WINDOW)
			*/
			windowsFormsHost2.Visibility = Visibility.Hidden;
			_panel2 = new System.Windows.Forms.Panel();
			windowsFormsHost2.Child = _panel2;
			/*
			*
			*/

		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			
		}

		protected void OnClosing(Process onClosingProcess, System.ComponentModel.CancelEventArgs e)
		{
			base.OnClosing(e);
			if (onClosingProcess != null)
			{
				onClosingProcess.Refresh();
				onClosingProcess.Close();
			}
		}

		private void ResizeEmbeddedApp(Process resizableProcess, System.Windows.Forms.Panel resizablePanel)
		{
			if (resizableProcess == null)
				return;

			SetWindowPos(resizableProcess.MainWindowHandle, IntPtr.Zero, 0, 0, (int)resizablePanel.ClientSize.Width, (int)resizablePanel.ClientSize.Height, SWP_NOZORDER | SWP_NOACTIVATE);
		}

		protected Size MeasureOverride(Process measureOverrideProcess, System.Windows.Forms.Panel measureOverridePanel, Size availableSize)
		{
			Size size = base.MeasureOverride(availableSize);
			ResizeEmbeddedApp(measureOverrideProcess, measureOverridePanel);
			return size;
		}


		// =================EVENTS==================
		private void buttonX_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}


		private void window_Splitscreen_Loaded(object sender, RoutedEventArgs e)
		{
			((MainWindow)Application.Current.MainWindow).buttonStart.IsEnabled = false; //remove the button's functionality while the new window is running
			((MainWindow)Application.Current.MainWindow).buttonStop.Visibility = Visibility.Visible;
			var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
			this.Left = desktopWorkingArea.Right - this.Width;
			this.Top = desktopWorkingArea.Bottom - this.Height;



			windowsFormsHost1.Visibility = Visibility.Visible; // show the windowsformshost panel again and cover the other buttons.
															   //ProcessStartInfo psi1 = new ProcessStartInfo("C:\\Users\\cezar\\Desktop\\Halo Eldewrito\\eldorado.lnk");
			ProcessStartInfo psi1 = new ProcessStartInfo("notepad.exe");

			_process1 = Process.Start(psi1);
			psi1.WindowStyle = (System.Diagnostics.ProcessWindowStyle)WindowStyle.ToolWindow;
			_process1.WaitForInputIdle();
			SetParent(_process1.MainWindowHandle, _panel1.Handle);

			// remove control box for _process1.MainWindowHandle
			int style1 = GetWindowLong(_process1.MainWindowHandle, GWL_STYLE);
			style1 = style1 & ~WS_CAPTION & ~WS_THICKFRAME;
			SetWindowLong(_process1.MainWindowHandle, GWL_STYLE, style1);
			// resize embedded application & refresh
			ResizeEmbeddedApp(_process1, _panel1);

			/*
			* ADDED SHIT
			*/
			windowsFormsHost2.Visibility = Visibility.Visible;
			//ProcessStartInfo psi2 = new ProcessStartInfo("C:\\Users\\cezar\\Desktop\\Halo Eldewrito p2\\eldorado2.lnk");
			ProcessStartInfo psi2 = new ProcessStartInfo("notepad.exe");
			_process2 = Process.Start(psi2);
			_process2.WaitForInputIdle();
			SetParent(_process2.MainWindowHandle, _panel2.Handle);

			int style2 = GetWindowLong(_process2.MainWindowHandle, GWL_STYLE);
			style2 = style2 & ~WS_CAPTION & ~WS_THICKFRAME;
			SetWindowLong(_process2.MainWindowHandle, GWL_STYLE, style2);
			ResizeEmbeddedApp(_process2, _panel2);
			/*
			*
			*/
			//psi1.Arguments = " -launcher -multiInstance";

			this.WindowState = WindowState.Minimized; // these 3 lines bring window_Splitscreen to the front (on top of the taskbar)
			this.Show();
			this.WindowState = WindowState.Normal;
		}


		private void window_Splitscreen_Closed(object sender, EventArgs e)
		{
			try
			{
				((MainWindow)Application.Current.MainWindow).buttonStart.IsEnabled = true;
			}
			catch(Exception)
			{
			}
		}

		/*============================================================
		========================IMPORTED STUFF========================
		============================================================*/
		[DllImport("user32.dll")]
		private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

		[DllImport("user32")]
		private static extern IntPtr SetParent(IntPtr hWnd, IntPtr hWndParent);

		[DllImport("user32")]
		private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

		[DllImport("user32.dll")]
		public static extern Int32 SetForegroundWindow(int hWnd);

		private const int SWP_NOZORDER = 0x0004;
		private const int SWP_NOACTIVATE = 0x0010;
		private const int GWL_STYLE = -16;
		private const int WS_CAPTION = 0x00C00000;
		private const int WS_THICKFRAME = 0x00040000;
	}
}
