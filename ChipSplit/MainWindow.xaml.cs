using System.Windows;
using MahApps.Metro.Controls;
using System.Collections.ObjectModel;
using System;
using System.IO;

namespace ChipSplit
{

	public partial class MainWindow : MetroWindow
	{
		//public IntPtr MainWindowHandle { get; set; }
		//[DllImport("user32.dll", SetLastError = true)]
		//private static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
		

		public ObservableCollection<ProcessInfo> CollectionProcesses = new ObservableCollection<ProcessInfo>{};

		public ObservableCollection<MainWindowData.Display> CollectionDisplays = new ObservableCollection<MainWindowData.Display> { };

		public MainWindow()
		{
			InitializeComponent();
			CollectionDisplays.Add(new MainWindowData.Display() { DisplayResolutionX = 1920, DisplayResolutionY = 1080 });

			buttonStop.Visibility = Visibility.Hidden;

			textBoxWidth.DataContext = CollectionDisplays;
			textBoxHeight.DataContext = CollectionDisplays;

			comboBoxPosition.ItemsSource = typeof(Position).GetEnumNames();

			//comboBoxPosition.ItemsSource = Enum.GetNames(typeof(Position));
			

			comboBoxPosition.SelectedValue = listboxProcesses.SelectedIndex;
			//comboBoxPosition.SelectedIndex = 0;

			//textBoxWidth.Text = CollectionDisplays[0].DisplayResolutionX.ToString();
			//textBoxHeight.Text = CollectionDisplays[0].DisplayResolutionY.ToString();
			textBoxIP.Text = "";
			textBoxPort.Text = "";

			CollectionProcesses.Add(new ProcessInfo() { Name = "Host", Args = "", Path = "C:\\secret\\place\\", Width = CollectionDisplays[0].DisplayResolutionX / 2, Height = CollectionDisplays[0].DisplayResolutionY / 2, WindowPosition = Position.middle });
			CollectionProcesses.Add(new ProcessInfo() { Name = "Client1", Args = "-h", Path = "C:\\not\\here\\", Width = CollectionDisplays[0].DisplayResolutionX / 2, Height = CollectionDisplays[0].DisplayResolutionY / 2, WindowPosition = Position.botCenter });

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
			//CollectionProcesses.Add(new ProcessInfo() { Name = "Process", Args = "", Path = "", Width = DisplayResolutionX / 2, Height = DisplayResolutionY / 2, WindowPosition = Position.topLeft });
			CollectionProcesses.Add(new ProcessInfo() { Name = textBoxName.Text+"P"+(CollectionProcesses.Count+1).ToString(), Args = textBoxArgs.Text, Path = textBoxPath.Text, Width = CollectionDisplays[0].DisplayResolutionX / 2, Height = CollectionDisplays[0].DisplayResolutionY / 2, WindowPosition = Position.topLeft });
			textStatusBar.Text = "Added new process.";
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
			this.Close();// temporary solution to address the lingering processes after splitscreen window is closed
			//textStatusBar.Text = "Stopped processes.";
		}

		private void buttonMenuBarNew_Click(object sender, RoutedEventArgs e)
		{
			((MainWindow)Application.Current.MainWindow).Title = "Untitled";
        }

		private void buttonMenuBarOpen_Click(object sender, RoutedEventArgs e)
		{
			// Create OpenFileDialog 
			Microsoft.Win32.OpenFileDialog openDlg = new Microsoft.Win32.OpenFileDialog();

			// Set filter for file extension and default file extension 
			openDlg.DefaultExt = ".json";
			openDlg.Filter = "JSON Files (*.json)|*.json|All Files|*.*";

			// Display OpenFileDialog by calling ShowDialog method 
			Nullable<bool> result = openDlg.ShowDialog();

			// Get the selected file name and display in a TextBox 
			if (result == true)
			{
				// Open document 
				string openFilePath = openDlg.FileName;
				string openFileName = Path.GetFileName(openFilePath);
				((MainWindow)Application.Current.MainWindow).Title = openFileName;
			}
		}

		private void buttonMenuBarSave_Click(object sender, RoutedEventArgs e)
		{
			// Create SaveFileDialog 
			Microsoft.Win32.SaveFileDialog saveDlg = new Microsoft.Win32.SaveFileDialog();

			// Set filter for file extension and default file extension 
			saveDlg.DefaultExt = ".json";
			saveDlg.Filter = "JSON Files (*.json)|*.json|All Files|*.*";

			// Display SaveFileDialog by calling ShowDialog method 
			Nullable<bool> result = saveDlg.ShowDialog();

			// Get the selected file name and display in a TextBox 
			//if (result == true)
			//{
				// Save document 
				string saveFilePath = saveDlg.FileName;
				string saveFileName = Path.GetFileName(saveFilePath);
				((MainWindow)Application.Current.MainWindow).Title = saveFileName;
				//string filename = dlg.FileName;
				//((MainWindow)Application.Current.MainWindow).Title = filename;
			//}*/
		}

		private void button_shift_down_process_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (listboxProcesses.SelectedIndex < CollectionProcesses.Count - 1)
				{
					CollectionProcesses.Move(listboxProcesses.SelectedIndex, listboxProcesses.SelectedIndex + 1);
				}
			}
			catch (ArgumentOutOfRangeException)
			{
				//throw;
			}
		}

		private void button_shift_up_process_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (listboxProcesses.SelectedIndex > 0)
				{
					CollectionProcesses.Move(listboxProcesses.SelectedIndex, listboxProcesses.SelectedIndex - 1);
				}
			}
			catch (ArgumentOutOfRangeException)
			{
				//throw;
			}
		}
	}
}
