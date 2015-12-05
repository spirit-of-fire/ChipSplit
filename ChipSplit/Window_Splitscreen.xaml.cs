using System;
using System.Windows;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ChipSplit
{
	/// <summary>
	/// Interaction logic for Window_Splitscreen.xaml
	/// </summary>
	public partial class Window_Splitscreen : Window
	{
		private System.Windows.Forms.Panel _panel;
		private Process _process;

		public Window_Splitscreen()
		{
			InitializeComponent();
			window_Splitscreen.WindowState = WindowState.Maximized; // removes the taskbar
            window_Splitscreen.BringIntoView();
			//Window_Splitscreen.SetWindowPos(Window_Splitscreen. 0, 0);

			windowsFormsHost1.Visibility = Visibility.Hidden; // hide the windows form before the user loads in the application so buttons can be displayed.

			_panel = new System.Windows.Forms.Panel();
			windowsFormsHost1.Child = _panel;
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			windowsFormsHost1.Visibility = Visibility.Visible; // show the windowsformshost panel again and cover the other buttons.
			//button1.Visibility = Visibility.Hidden; // replaced with button1.isEnabled = false down below where the window_Splitscreen initializes

			ProcessStartInfo psi = new ProcessStartInfo("C:\\Users\\cezar\\Desktop\\Halo\\eldorado.exe");
			_process = Process.Start(psi);
			_process.WaitForInputIdle();
			SetParent(_process.MainWindowHandle, _panel.Handle);

			// remove control box
			int style = GetWindowLong(_process.MainWindowHandle, GWL_STYLE);
			style = style & ~WS_CAPTION & ~WS_THICKFRAME;
			SetWindowLong(_process.MainWindowHandle, GWL_STYLE, style);

			// resize embedded application & refresh
			ResizeEmbeddedApp();
		}

		[DllImport("user32.dll")]
		private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

		[DllImport("user32")]
		private static extern IntPtr SetParent(IntPtr hWnd, IntPtr hWndParent);

		[DllImport("user32")]
		private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

		private const int SWP_NOZORDER = 0x0004;
		private const int SWP_NOACTIVATE = 0x0010;
		private const int GWL_STYLE = -16;
		private const int WS_CAPTION = 0x00C00000;
		private const int WS_THICKFRAME = 0x00040000;

		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			base.OnClosing(e);
			if (_process != null)
			{
				_process.Refresh();
				_process.Close();
			}
		}

		private void ResizeEmbeddedApp()
		{
			if (_process == null)
				return;

			SetWindowPos(_process.MainWindowHandle, IntPtr.Zero, 0, 0, (int)_panel.ClientSize.Width, (int)_panel.ClientSize.Height, SWP_NOZORDER | SWP_NOACTIVATE);
		}

		protected override Size MeasureOverride(Size availableSize)
		{
			Size size = base.MeasureOverride(availableSize);
			ResizeEmbeddedApp();
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
		}

		private void window_Splitscreen_Closed(object sender, EventArgs e)
		{
			try
			{
				((MainWindow)Application.Current.MainWindow).buttonStart.IsEnabled = true;
				//Taskbar.Show(); // not needed anymore
			}
			catch(Exception)
			{
			}
		}
	}
}
