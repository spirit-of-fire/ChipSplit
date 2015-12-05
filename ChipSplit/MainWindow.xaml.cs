using System.Windows;
using MahApps.Metro.Controls;
using System.Collections.ObjectModel;
using System;

namespace ChipSplit
{

	public partial class MainWindow : MetroWindow
	{
		//public IntPtr MainWindowHandle { get; set; }
		//[DllImport("user32.dll", SetLastError = true)]
		//private static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
		int DisplayResolutionX = 1920;
		int DisplayResolutionY = 1080;

		public ObservableCollection<ProcessInfo> CollectionProcesses = new ObservableCollection<ProcessInfo>{};

		public MainWindow()
		{
			InitializeComponent();
			buttonStop.Visibility = Visibility.Hidden;

			textBoxWidth.DataContext = DisplayResolutionX;
			textBoxHeight.DataContext = DisplayResolutionY;

			comboBoxPosition.ItemsSource = typeof(Position).GetEnumNames();

			//comboBoxPosition.ItemsSource = Enum.GetNames(typeof(Position));
			

			comboBoxPosition.SelectedValue = listboxProcesses.SelectedIndex;
			//comboBoxPosition.SelectedIndex = 0;

			//textBoxWidth.Text = defaultResolutionX.ToString();
			textBoxHeight.Text = DisplayResolutionY.ToString();
			textBoxIP.Text = "";
			textBoxPort.Text = "";

			CollectionProcesses.Add(new ProcessInfo() { Name = "Some process", Args = "--help", Path = "C:\\secret\\place\\", Width = DisplayResolutionX / 2, Height = DisplayResolutionY / 2, WindowPosition = Position.middle });
			CollectionProcesses.Add(new ProcessInfo() { Name = "Another process", Args = "-h", Path = "C:\\not\\here\\", Width = DisplayResolutionX / 2, Height = DisplayResolutionY / 2, WindowPosition = Position.botCenter });

			listboxProcesses.ItemsSource = CollectionProcesses;
			textBoxName.DataContext = CollectionProcesses;
			textBoxPath.DataContext = CollectionProcesses;
			textBoxArgs.DataContext = CollectionProcesses;
			textBoxWindowX.DataContext = CollectionProcesses;
			textBoxWindowY.DataContext = CollectionProcesses;
			comboBoxPosition.DataContext = CollectionProcesses;

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
			textStatusBar.Text = "Started processes.";
		}

		private void buttonOpenFlyout_Click(object sender, RoutedEventArgs e)
		{
			flyoutRight.IsOpen = true;
		}

		private void button_remove_process_Click(object sender, RoutedEventArgs e)
		{
			CollectionProcesses.Remove((ProcessInfo)listboxProcesses.SelectedItem);
			textStatusBar.Text = "Removed process.";
		}

		private void button_add_process_Click(object sender, RoutedEventArgs e)
		{
			CollectionProcesses.Add(new ProcessInfo() { Name = "Process", Args = "", Path = "", Width = DisplayResolutionX / 2, Height = DisplayResolutionY / 2, WindowPosition = Position.topLeft });
			textStatusBar.Text = "Added new process.";
		}

		private void textBoxName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			//CollectionProcesses.
        }

		private void buttonStop_Click(object sender, RoutedEventArgs e)
		{
			foreach (Window win in App.Current.Windows)
			{
				if (win != this)
				{
					win.Close();
					((MainWindow)Application.Current.MainWindow).buttonStop.Visibility = Visibility.Hidden;
				}
			}
			textStatusBar.Text = "Stopped processes.";
		}

		private void buttonMenuBarNew_Click(object sender, RoutedEventArgs e)
		{
			((MainWindow)Application.Current.MainWindow).Title = "Untitled";
        }

		private void buttonMenuBarOpen_Click(object sender, RoutedEventArgs e)
		{
			// Create OpenFileDialog 
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

			// Set filter for file extension and default file extension 
			dlg.DefaultExt = ".json";
			dlg.Filter = "JSON Files (*.json)|*.json|All Files|*.*";

			// Display OpenFileDialog by calling ShowDialog method 
			Nullable<bool> result = dlg.ShowDialog();

			// Get the selected file name and display in a TextBox 
			if (result == true)
			{
				// Open document 
				string filename = dlg.FileName;
				((MainWindow)Application.Current.MainWindow).Title = filename;
			}
		}

		private void buttonMenuBarSave_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
