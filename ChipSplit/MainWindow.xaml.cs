using System.Windows;
using MahApps.Metro.Controls;

namespace ChipSplit
{

	public partial class MainWindow : MetroWindow
	{
		//public IntPtr MainWindowHandle { get; set; }
		//[DllImport("user32.dll", SetLastError = true)]
		//private static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

		public MainWindow()
		{
			InitializeComponent();

			

			//ProcessStartInfo strtInf = new ProcessStartInfo("notepad.exe");
			//strtInf.WindowStyle = ProcessWindowStyle.Maximized;
			//strtInf.WindowStyle = ProcessWindowStyle.Normal;
			//Process.Start(strtInf);

			//Process myProcess = Process.Start("notepad.exe", "");

			//label1.Content = myProcess.ProcessName+"\n"+
			//				 myProcess.MainModule;
		}

		private void buttonStart_Click(object sender, RoutedEventArgs e)
		{
			Window_Splitscreen window_Splitscreen = new Window_Splitscreen();
			window_Splitscreen.Show();
		}

		private void button_remove_process_Click(object sender, RoutedEventArgs e)
		{
			//listbox_processes.
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			flyoutRight.IsOpen = true;
		}
	}
}
