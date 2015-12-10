using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSplit
{
	public class MainWindowData // Contains the datatypes used in the main window
	{
		public class Display
		{
			public int DisplayResolutionX { get; set; }
			public int DisplayResolutionY { get; set; }
		}

		//public static ObservableCollection<Display> CollectionDisplays = new ObservableCollection<Display>{ };

		public MainWindowData()
		{
			//CollectionDisplays.Add( new Display() { DisplayResolutionX = 1920, DisplayResolutionY = 1080 } );
		}
	}
}
