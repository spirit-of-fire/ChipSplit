using System.Windows;
using MahApps.Metro.Controls;
using System.Collections.ObjectModel;

namespace ChipSplit
{

	public partial class MainWindow : MetroWindow
	{
		//public IntPtr MainWindowHandle { get; set; }
		//[DllImport("user32.dll", SetLastError = true)]
		//private static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

		public ObservableCollection<ProcessInfo> CollectionProcesses = new ObservableCollection<ProcessInfo>{};

		public MainWindow()
		{
			InitializeComponent();

			//foreach()
			//CollectionProcesses.Add();
			CollectionProcesses.Add(new ProcessInfo() { Name = "Junk here", Args = "whatever", Path = "C:\\hold\\on", Position = Position.middle });
			CollectionProcesses.Add(new ProcessInfo() { Name = "another thing", Args = "blank", Path = "C:\\not\\here", Position = Position.leftCenter });

			textBoxWidth.Text = "1920";
			textBoxHeight.Text = "1080";
			textBoxIP.Text = "127.0.0.1";
			textBoxPort.Text = "";

			listboxProcesses.ItemsSource = CollectionProcesses;
			textBoxName.DataContext = CollectionProcesses;
			textBoxPath.DataContext = CollectionProcesses;


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

		private void buttonOpenFlyout_Click(object sender, RoutedEventArgs e)
		{
			flyoutRight.IsOpen = true;
		}

		private void button_remove_process_Click(object sender, RoutedEventArgs e)
		{
			CollectionProcesses.Remove((ProcessInfo)listboxProcesses.SelectedItem);
		}

		private void button_add_process_Click(object sender, RoutedEventArgs e)
		{
			CollectionProcesses.Add(new ProcessInfo() { Name="Test", Args="nothing", Path="C:\\help\\me", Position=Position.botLeft });
		}

		private void textBoxName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			//CollectionProcesses.
        }
	}
}
